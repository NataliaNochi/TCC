using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect;
using TCC.Classes.Base;
using TCC.Classes.Movimento;
using TCC.Classes.Movimento.Poses.Alongamento;
using TCC.Classes.Movimento.Poses.Primeira_Serie;


namespace TCC.Formularios
{
    /// <summary>
    /// Interaction logic for FrmFase2.xaml
    /// </summary>
    public partial class FrmFase2 : Window
    {
        public FrmFase2()
        {
            InicializarRastreadores();
            InitializeComponent();
     
        }

        SensorKinect sensor = new SensorKinect();
        List<IRastreadorEsqueleto> rastreadores;

        //Capturar Imagem 
        private byte[] ObterImagemSensorRGB(ColorImageFrame frame)
        {
            if (frame == null)
                return null;

            using (frame)
            {
                byte[] bytesImg = new byte[frame.PixelDataLength];
                frame.CopyPixelDataTo(bytesImg);

            
              if (ckEscalaCinza.IsChecked.HasValue && ckEscalaCinza.IsChecked.Value)
                {
                    //Percorre todos os pixels substituindo o varlos RGB pelo valor mais alto de cada
                    for (int i = 0; i < bytesImg.Length; i += frame.BytesPerPixel)
                    {
                        byte maiorValorCor = Math.Max(bytesImg[i], Math.Max(bytesImg[i + 1], bytesImg[i + 2]));
                        bytesImg[i] = maiorValorCor;
                        bytesImg[i + 1] = maiorValorCor;
                        bytesImg[i + 2] = maiorValorCor;
                    }
                }

                //strid é responsável por suavizar a imagem 
                int stride = frame.Width * frame.BytesPerPixel;
                

                //retorna largura e altura da img, quantidade de pontos por polegada, formato de cada pixel da img, paleta de cores null, stride 
                return bytesImg; // BitmapSource.Create(frame.Width, frame.Height, 96, 96, PixelFormats.Bgr32, null, bytesImg, stride);
            }
        }

        private void Distancia(DepthImageFrame frame, byte[] bytesImg, int distanciaMax)
        {
            if (frame == null || bytesImg == null)
                return;

            using (frame)
            {
                DepthImagePixel[] profundidadeImg = new DepthImagePixel[frame.PixelDataLength];
                frame.CopyDepthImagePixelDataTo(profundidadeImg);

                DepthImagePoint[] pontosProfundidadeImg = new DepthImagePoint[640 * 480];
                sensor.kinect.CoordinateMapper.MapColorFrameToDepthFrame(sensor.kinect.ColorStream.Format, sensor.kinect.DepthStream.Format, profundidadeImg, pontosProfundidadeImg);

                for (int i = 0; i < pontosProfundidadeImg.Length; i++)
                {
                    var ponto = pontosProfundidadeImg[i];
                    if (ponto.Depth < distanciaMax && KinectSensor.IsKnownPoint(ponto))
                    {
                        var pixelIndex = i * 4;
                        byte maiorValorCor = Math.Max(bytesImg[pixelIndex], Math.Max(bytesImg[pixelIndex + 1], bytesImg[pixelIndex + 2]));
                        bytesImg[pixelIndex] = maiorValorCor;
                        bytesImg[pixelIndex + 1] = maiorValorCor;
                        bytesImg[pixelIndex + 2] = maiorValorCor;
                    }
                }
            }
        }

        //A cada 1 seg 30 frames
        private void kinect_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            byte[] bytesImg = ObterImagemSensorRGB(e.OpenColorImageFrame());

            if (ckEscalaCinza.IsChecked.HasValue && ckEscalaCinza.IsChecked.Value)
                Distancia(e.OpenDepthImageFrame(), bytesImg, 2000);

            if (bytesImg != null)
                canvasKinect.Background = new ImageBrush(BitmapSource.Create(sensor.kinect.ColorStream.FrameWidth, sensor.kinect.ColorStream.FrameHeight, 96, 96, PixelFormats.Bgr32, null, bytesImg, sensor.kinect.ColorStream.FrameBytesPerPixel * sensor.kinect.ColorStream.FrameWidth));

            canvasKinect.Children.Clear();

           if (ckExibirEsqueleto.IsChecked.HasValue && ckExibirEsqueleto.IsChecked.Value)
                FuncoesEsqueleto(e.OpenSkeletonFrame());
        }
              
        private void InicializarRastreadores()
        {
            rastreadores = new List<IRastreadorEsqueleto>();

            RastreadorEsqueleto<TerceiroD_Along> teste = new RastreadorEsqueleto<TerceiroD_Along>();

            rastreadores.Add(teste);
        }

        private void PoseIdentificada(object sender, EventArgs e)
        {
            ckExibirEsqueleto.IsChecked = !ckExibirEsqueleto.IsChecked;
        }

        private void FuncoesEsqueleto(SkeletonFrame frame)
        {
            if (frame == null)
                return;

            using (frame)
            {
                Skeleton esqueleto = frame.ObterEsqueletoUsuario();

                foreach (IRastreadorEsqueleto r in rastreadores)
                {
                    r.Rastrear(esqueleto);
                    break;

                }
                if (ckExibirEsqueleto.IsChecked.HasValue && ckExibirEsqueleto.IsChecked.Value)
                    frame.DesenharEsqueleto(sensor.kinect, canvasKinect);
            }
        }
         
        //Alterar elevação do sensor
        private void slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            try
            {
                sensor.kinect.ElevationAngle = Convert.ToInt32(slider.Value);
                lb_Angulo.Content = "Ângulo Atual: " + sensor.kinect.ElevationAngle;
            }
            catch 
            {
              MessageBox.Show("Erro: Verifique se o sensor encontra-se conectado");
            }
        }
        
        private void frmFase2_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sensor.InicializarSensor();

                //A cada seg. a aplicação passará por ele com uma velocidade de 30 fps - captando o que a câmera RGB está visualizando
                sensor.kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(kinect_AllFramesReady);
                sensor.AnguloElevacao(0);
            }

            catch
            {
                MessageBox.Show("Erro: Verifique se o sensor encontra-se conectado");
            }
        }

        private void frmFase2_Closed(object sender, EventArgs e)
        {
            sensor.DesligarSensor();
        }            

    }
}
