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
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Interaction;
using TCC.Classes.Base;
using TCC.Classes.Movimento;
using TCC.Classes.Movimento.Poses;
using TCC.Classes.Movimento.Poses.Alongamento;
using TCC.Classes.Movimento.Poses.Primeira_Serie;
using TCC.Classes.Movimento.Gestos.Aceno;
using System.Windows.Threading;
using System.Diagnostics;


namespace TCC.Formularios
{
    /// <summary>
    /// Interaction logic for FrmFase2.xaml
    /// </summary>
    public partial class FrmFase2 : Window
    {

        int pontuacao = 0;
        object atual = null;
        DispatcherTimer _timer;
        TimeSpan _time;
        
        public FrmFase2()
        {
            
            InitializeComponent();

            _time = TimeSpan.FromSeconds(0);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tb_timer.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    if (pontuacao == 0)
                        pontuacao = 0;
                    else
                    {
                        pontuacao--;
                        lb_Pontos.Content = pontuacao;
                    }
                    _time = TimeSpan.FromSeconds(29);
                    

                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            InicializarRastreadores();
            
           
        }
        Stopwatch cronometro = new Stopwatch();
        
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


                if (btnEscalaCinza.IsChecked && btnEscalaCinza.IsChecked)
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

                //stride é responsável por suavizar a imagem 
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

            if (btnEscalaCinza.IsChecked && btnEscalaCinza.IsChecked)
                Distancia(e.OpenDepthImageFrame(), bytesImg, 2000);

            if (bytesImg != null)
                canvasKinect.Background = new ImageBrush(BitmapSource.Create(sensor.kinect.ColorStream.FrameWidth, sensor.kinect.ColorStream.FrameHeight, 96, 96, PixelFormats.Bgr32, null, bytesImg, sensor.kinect.ColorStream.FrameBytesPerPixel * sensor.kinect.ColorStream.FrameWidth));

            canvasKinect.Children.Clear();

            //if (ckExibirEsqueleto.IsChecked && ckExibirEsqueleto.IsChecked)
            FuncoesEsqueleto(e.OpenSkeletonFrame());
        }

        private void InicializarRastreadores()
        {
            rastreadores = new List<IRastreadorEsqueleto>();

            RastreadorEsqueleto<PoseT> poseT = new RastreadorEsqueleto<PoseT>();
            poseT.MovimentoIdentificado += PoseTIdentificada;

            RastreadorEsqueleto<Primeiro_Along> PrimAlong = new RastreadorEsqueleto<Primeiro_Along>();
            PrimAlong.MovimentoIdentificado += PrimAlongIdentificado;

            RastreadorEsqueleto<FortalecimentoOmbrosA> FortOmbrosA = new RastreadorEsqueleto<FortalecimentoOmbrosA>();
            FortOmbrosA.MovimentoIdentificado += FortOmbrosAIdentificado;

            RastreadorEsqueleto<FortalecimentoOmbrosB> FortOmbrosB = new RastreadorEsqueleto<FortalecimentoOmbrosB>();
            FortOmbrosB.MovimentoIdentificado += FortOmbrosBIdentificado;

            RastreadorEsqueleto<FlexaoLateralD> flexaoD = new RastreadorEsqueleto<FlexaoLateralD>();
            flexaoD.MovimentoIdentificado += PoseFlexaoDIdentificada;

            RastreadorEsqueleto<FlexaoLateralE> flexaoE = new RastreadorEsqueleto<FlexaoLateralE>();
            flexaoE.MovimentoIdentificado += PoseFlexaoEIdentificada;

            RastreadorEsqueleto<VariacaoFortalecimentoA> VariacaoFortOmbroA = new RastreadorEsqueleto<VariacaoFortalecimentoA>();
            VariacaoFortOmbroA.MovimentoIdentificado += VariacaoFortOmbroAIdenditificado;

            RastreadorEsqueleto<VariacaoFortalecimentoB> VariacaoFortOmbroB = new RastreadorEsqueleto<VariacaoFortalecimentoB>();
            VariacaoFortOmbroB.MovimentoIdentificado += VariacaoFortOmbroBIdenditificado;

            rastreadores.Add(poseT);
            
            rastreadores.Add(PrimAlong);
 
            rastreadores.Add(FortOmbrosA); 
            rastreadores.Add(FortOmbrosB); 
        
            rastreadores.Add(flexaoD);
            rastreadores.Add(flexaoE);

            rastreadores.Add(VariacaoFortOmbroA);
            rastreadores.Add(VariacaoFortOmbroB);
        
            atual = rastreadores[0];

          
        }

        private void PoseTIdentificada(object sender, EventArgs e)
        {

            /*if (kinectRegion.KinectSensor == null)
                kinectRegion.KinectSensor = sensor.kinect;*/
            if (atual == rastreadores[0])
            {
                atual = rastreadores[1];
                _timer.Start();
                InicializarRastreadores();
            }
      

            //if (atual != null)
              //  Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " " + atual.GetType().FullName);
              //MessageBox.Show("PoseT");
            
        }

        

        private void PrimAlongIdentificado(object sender, EventArgs e)
        {
            _timer.Start();
            if (atual == rastreadores[1])
            {
                atual = rastreadores[2];
                pontuacao++;
                lb_Pontos.Content = pontuacao;
                
                pose.Source = new BitmapImage(new Uri(@"/TCC;component/Imagens/Fase1/morto.png", UriKind.Relative));
            }
        }

        private void FortOmbrosAIdentificado(object sender, EventArgs e)
        {
            _timer.Start();
            if (atual == rastreadores[2])
            {
                atual = rastreadores[3];
                pontuacao++;
                lb_Pontos.Content = pontuacao;

                pose.Source = new BitmapImage(new Uri(@"/TCC;component/Imagens/Fase1/feliz.png", UriKind.Relative));

            }
        }

        private void FortOmbrosBIdentificado(object sender, EventArgs e)
        {
            _timer.Start();
            if (atual == rastreadores[3])
            {
                atual = rastreadores[4];
                pontuacao++;
                lb_Pontos.Content = pontuacao;

                pose.Source = new BitmapImage(new Uri(@"/TCC;component/Imagens/Fase1/morto2.png", UriKind.Relative));

            }
        }


        private void PoseFlexaoDIdentificada(object sender, EventArgs e)
        {
            _timer.Start();
            if (atual == rastreadores[4])
            {
                atual = rastreadores[5];
                pontuacao++;
                lb_Pontos.Content = pontuacao;

                pose.Source = new BitmapImage(new Uri(@"/TCC;component/Imagens/Fase1/feliz2.png", UriKind.Relative));

            }
        }

        private void PoseFlexaoEIdentificada(object sender, EventArgs e)
        {
            _timer.Start();
            if (atual == rastreadores[5])
            {
                atual = rastreadores[6];
                pontuacao++;
                lb_Pontos.Content = pontuacao;

                pose.Source = new BitmapImage(new Uri(@"/TCC;component/Imagens/Fase1/img1_certo.png", UriKind.Relative));

            }

        }
        
        private void VariacaoFortOmbroAIdenditificado(object sender, EventArgs e)
        {
            _timer.Start();
            if (atual == rastreadores[6])
            {
                atual = rastreadores[7];
                pontuacao++;
                lb_Pontos.Content = pontuacao;
            }

        }

        private void VariacaoFortOmbroBIdenditificado(object sender, EventArgs e)
        {
            _timer.Start();
            if (atual == rastreadores[7])
            {
                atual = rastreadores[8];
                pontuacao++;
                lb_Pontos.Content = pontuacao;
            }
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
                    

                }
                // if (ckExibirEsqueleto.IsChecked && ckExibirEsqueleto.IsChecked)
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
                MessageBox.Show("Verifique se o sensor encontra-se conectado", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void frmFase2_Loaded(object sender, RoutedEventArgs e)
        {
           sensor.InicializarSensor();

            if(sensor.kinect != null)
            {
                //A cada seg. a aplicação passará por ele com uma velocidade de 30 fps - captando o que a câmera RGB está visualizando
                sensor.kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(kinect_AllFramesReady);
                sensor.AnguloElevacao(0);
            }
            _timer.Start();
        }

        private void frmFase2_Closed(object sender, EventArgs e)
        {
            sensor.DesligarSensor();
        }

        private void btnEscalaCinza_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("teste");
        }

        private void btnEsqueletoUsuario_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
