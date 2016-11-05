using Microsoft.Kinect;
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
using TCC.Classes.Base;
using TCC.Classes.Movimento;
using TCC.Classes.Movimento.Poses;
using Microsoft.Kinect.Toolkit;
using System.Threading;
using TCC.Classes.Cadastro;

namespace TCC.Formularios
{
    /// <summary>
    /// Interaction logic for FrmFase1.xaml
    /// </summary>
    public partial class FrmFase1 : Window
    {
        #region variáveis

        int pontuacao = 0, paresFeitos = 0, segundoClickCerto = 0, segundoClickErrado = 0, NumeroDeVidas = 3;
        const int tempoEspera = 500;
        UsuarioVO usuario;
        List<IRastreadorEsqueleto> rastreadores;
        List<BitmapImage> listaDeVidas = new List<BitmapImage>();


        SensorKinect sensor = new SensorKinect();

        System.Windows.Media.ImageSource carregaImagem = null;
        Image imgDesseleciona = null, imgDesseleciona2 = null;
        KinectToggleButton btnDesseleciona = null, btnDesseleciona2 = null;
        KinectToggleButton posturaSelecionadaCarregarPrimeiro = null;


        #endregion

        public FrmFase1()
        {
            InicializarRastreadores();
            InitializeComponent();
        }
        public FrmFase1(UsuarioVO user)
        {
            InicializarRastreadores();
            InitializeComponent();
            usuario = user;
            escolheQualComeca();

            mudarCorBotoesDePosturaCorretaEIncorreta();
            if (kinectRegion.KinectSensor == null)
                kinectRegion.KinectSensor = sensor.kinect;
            
        }
        
        private void mudarCorBotoesDePosturaCorretaEIncorreta()
        {
            if (posturaSelecionadaCarregarPrimeiro == btnPosturaIncorreta)
                btnPosturaIncorreta.Background = Brushes.DarkRed;
            else
                btnPosturaCorreta.Background = Brushes.DarkGreen;
        }

        #region setar kinect
        private byte[] ObterImagemSensorRGB(ColorImageFrame frame)
        {
            if (frame == null)
                return null;

            using (frame)
            {
                byte[] bytesImg = new byte[frame.PixelDataLength];
                frame.CopyPixelDataTo(bytesImg);

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

            if (bytesImg != null)
                stkKinect.Background = new ImageBrush(BitmapSource.Create(sensor.kinect.ColorStream.FrameWidth, sensor.kinect.ColorStream.FrameHeight, 96, 96, PixelFormats.Bgr32, null, bytesImg, sensor.kinect.ColorStream.FrameBytesPerPixel * sensor.kinect.ColorStream.FrameWidth));

            stkKinect.Children.Clear();

            //if (ckExibirEsqueleto.IsChecked && ckExibirEsqueleto.IsChecked)
            FuncoesEsqueleto(e.OpenSkeletonFrame());
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

            }
        }
        #endregion

        #region rastreador e pose
        private void InicializarRastreadores()
        {
            rastreadores = new List<IRastreadorEsqueleto>(); //inicializa a lista de rastreadores

            RastreadorEsqueleto<PoseT> poseT = new RastreadorEsqueleto<PoseT>(); //cria novo rastreador do tipo pose T
            poseT.MovimentoIdentificado += PoseTIdentificada;

            rastreadores.Add(poseT); //adiciona o PoseT na lista de rastreadores

        } //Rastreadores e detector da pose T para iniciar o rastreamento da mão

        private void PoseTIdentificada(object sender, EventArgs e)
        {
            if (kinectRegion.KinectSensor == null)
                kinectRegion.KinectSensor = sensor.kinect; //Inicializa o kinect region para rastreamento mais forte da mão utilizando o toolkit
            

        }
        #endregion 

        #region ações do form

        private void frmFase1_Loaded(object sender, RoutedEventArgs e)
        {

            listaDeVidas.Add(new BitmapImage(new Uri(@"/TCC;component/Imagens/Fase1/morto2.png", UriKind.Relative)));
            listaDeVidas.Add(new BitmapImage(new Uri(@"/TCC;component/Imagens/Fase1/morto.png", UriKind.Relative)));
            listaDeVidas.Add(new BitmapImage(new Uri(@"/TCC;component/Imagens/Fase1/feliz2.png", UriKind.Relative)));
            listaDeVidas.Add(new BitmapImage(new Uri(@"/TCC;component/Imagens/Fase1/feliz.png", UriKind.Relative)));
            
            imgVida.Source = listaDeVidas[NumeroDeVidas];
         
            sensor.InicializarSensor();

            if (sensor.kinect != null)
            {
             
                //A cada seg. a aplicação passará por ele com uma velocidade de 30 fps - captando o que a câmera RGB está visualizando
                sensor.kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(kinect_AllFramesReady);
                sensor.AnguloElevacao(0);
                lbPontos.Content = pontuacao;
            }
                       
        }
        #endregion

        #region métodos
        private void ganhaVida()
        {
            if (pontuacao % 3 == 0 && NumeroDeVidas >= 0 && NumeroDeVidas < 3)
            {
                NumeroDeVidas++;
                imgVida.Source = listaDeVidas[NumeroDeVidas];
            }
        }

        private void escolheQualComeca()
        {
            if ((DateTime.Now.Millisecond & 1) == 1)
                posturaSelecionadaCarregarPrimeiro = btnPosturaCorreta;
 
            else
                posturaSelecionadaCarregarPrimeiro = btnPosturaIncorreta;

            mudarCorBotoesDePosturaCorretaEIncorreta();

        }

        private void reiniciaJogo()
        {
            btnPostura1.Visibility = System.Windows.Visibility.Visible;
            btnPostura2.Visibility = System.Windows.Visibility.Visible;
            btnPostura3.Visibility = System.Windows.Visibility.Visible;
            btnPostura4.Visibility = System.Windows.Visibility.Visible;
            btnPostura5.Visibility = System.Windows.Visibility.Visible;
            btnPostura6.Visibility = System.Windows.Visibility.Visible;
            btnPostura7.Visibility = System.Windows.Visibility.Visible;
            btnPostura8.Visibility = System.Windows.Visibility.Visible;
            btnPostura9.Visibility = System.Windows.Visibility.Visible;
            btnPostura10.Visibility = System.Windows.Visibility.Visible;
            btnPostura11.Visibility = System.Windows.Visibility.Visible;
            btnPostura12.Visibility = System.Windows.Visibility.Visible;

            img1Certo.Opacity = 1;
            img2Certo.Opacity = 1;
            img3Certo.Opacity = 1;
            img4Certo.Opacity = 1;
            img5Certo.Opacity = 1;
            img6Certo.Opacity = 1;

            img1Errado.Opacity = 1;
            img2Errado.Opacity = 1;
            img3Errado.Opacity = 1;
            img4Errado.Opacity = 1;
            img5Errado.Opacity = 1;
            img6Errado.Opacity = 1;

            btnPostura1.IsChecked = false;
            btnPostura2.IsChecked = false;
            btnPostura3.IsChecked = false;
            btnPostura4.IsChecked = false;
            btnPostura5.IsChecked = false;
            btnPostura6.IsChecked = false;
            btnPostura7.IsChecked = false;
            btnPostura8.IsChecked = false;
            btnPostura9.IsChecked = false;
            btnPostura10.IsChecked = false;
            btnPostura11.IsChecked = false;
            btnPostura12.IsChecked = false;

            btnPosturaCorreta.Background = Brushes.DarkGray;
            btnPosturaIncorreta.Background = Brushes.DarkGray;
            
            escolheQualComeca();
            pontuacao = 0;
            lbPontos.Content = pontuacao;
            imgVida.Source = listaDeVidas[3];
        }

        private void parCerto(KinectToggleButton btn1, KinectToggleButton btn2)
        {
            paresFeitos++;
            pontuacao++;
            lbPontos.Content = pontuacao;
            btn1.Visibility = Visibility.Hidden; //img1certo
            btn2.Visibility = Visibility.Hidden; //img1errado
            ganhaVida();
        }
        
        async private void testaPosturas() // método para testar se o par escolhido está certo. Método assíncrono apenas para ter delay sem travar o sistema
        {
            
                
                if (btnPosturaIncorreta.IsChecked && btnPosturaCorreta.IsChecked)
                {
                    await Task.Delay(500); //espera meio segundo para executar o resto. Await previne o sistema de travar e só pode ser utilizado com métodos assíncronos
                    if (imgErrado.Source == img1Errado.Source && imgCerto.Source == img1Certo.Source)
                        parCerto(btnPostura1, btnPostura6);

                    else if (imgErrado.Source == img2Errado.Source && imgCerto.Source == img2Certo.Source)
                        parCerto(btnPostura2, btnPostura4);

                    else if (imgErrado.Source == img3Errado.Source && imgCerto.Source == img3Certo.Source)
                        parCerto(btnPostura5, btnPostura3);

                    else if (imgErrado.Source == img4Errado.Source && imgCerto.Source == img4Certo.Source)
                        parCerto(btnPostura8, btnPostura7);

                    else if (imgErrado.Source == img5Errado.Source && imgCerto.Source == img5Certo.Source)
                        parCerto(btnPostura11, btnPostura9);

                    else if (imgErrado.Source == img6Errado.Source && imgCerto.Source == img6Certo.Source)
                        parCerto(btnPostura12, btnPostura10);

                    else
                    {
                        imgDesseleciona.Opacity = 1;
                        imgDesseleciona2.Opacity = 1;

                        btnDesseleciona.IsChecked = false;
                        btnDesseleciona2.IsChecked = false;

                    if (NumeroDeVidas == 0)
                    {
                        MessageBox.Show("Você perdeu");
                        salvaDados();
                    }
                    else
                    {
                        NumeroDeVidas--;
                        imgVida.Source = listaDeVidas[NumeroDeVidas];
                    }

                    }

                    imgDesseleciona = null;
                    imgDesseleciona2 = null;
                    btnDesseleciona = null;
                    btnDesseleciona2 = null;
                    imgErrado.Source = null;
                    imgCerto.Source = null;
                    if (paresFeitos == 6)
                    {
                        MessageBox.Show("Parabéns");
                        salvaDados();
                        reiniciaJogo();
                        
                        
                    }
                    btnPosturaCorreta.IsChecked = false;
                    btnPosturaIncorreta.IsChecked = false;
                }
            
        }

        private void salvaDados()
        {
            try
            {
                Fase1VO fase1Dados = new Fase1VO();
                fase1Dados.setCpf(usuario.GetCPF());
                fase1Dados.setData(DateTime.Today);
                fase1Dados.setPontuacao(pontuacao);
                Fase1DAO.Cadastrar(fase1Dados);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        private void preencheCorretoEErrado(Image img)
        {
            if (posturaSelecionadaCarregarPrimeiro == btnPosturaCorreta && imgCerto.Source == null)
            {
                imgCerto.Source = img.Source;
                btnPosturaCorreta.IsChecked = true;
                btnPosturaIncorreta.Background = Brushes.DarkRed;
            }
            else if (posturaSelecionadaCarregarPrimeiro == btnPosturaCorreta && imgCerto.Source != null)
            {
                imgErrado.Source = img.Source;
                btnPosturaIncorreta.IsChecked = true;
            }

            else if (posturaSelecionadaCarregarPrimeiro != btnPosturaCorreta && imgErrado.Source == null)
            {
                imgErrado.Source = img.Source;
                btnPosturaIncorreta.IsChecked = true;
                btnPosturaCorreta.Background = Brushes.DarkGreen;
            }
            else if (posturaSelecionadaCarregarPrimeiro != btnPosturaCorreta && imgErrado.Source != null)
            {
                imgCerto.Source = img.Source;
                btnPosturaCorreta.IsChecked = true;
            }

            if (btnPosturaCorreta.IsChecked && btnPosturaIncorreta.IsChecked)
            {
                testaPosturas();
                btnPosturaCorreta.IsChecked = !btnPosturaCorreta.IsChecked;
                btnPosturaIncorreta.IsChecked = !btnPosturaIncorreta.IsChecked;
                btnPosturaCorreta.Background = Brushes.DarkGray;
                btnPosturaIncorreta.Background = Brushes.DarkGray;
                escolheQualComeca();
                
            }
        }
        private void selecaoDeBotao(KinectToggleButton btn, Image img) //mostra que o botão foi selecionado
        {
            if (!btn.IsChecked)
            {
                if (imgCerto == null && imgErrado == null)
                {
                    img.Opacity = 1;
                    carregaImagem = null;
                    imgDesseleciona = null;
                    btnDesseleciona = null;
                    imgDesseleciona2 = null;
                    btnDesseleciona2 = null;
                }
                else
                    btn.IsChecked = !btn.IsChecked;

            }
            else
            {
                if (posturaSelecionadaCarregarPrimeiro != null)
                {
                    preencheCorretoEErrado(img);
                   
                    img.Opacity = 0.2;
                    if (imgDesseleciona == null) //se for a primeira imagem selecionada
                    {
                        imgDesseleciona = img;
                        btnDesseleciona = btn;
                    }
                    else //se for a segunda
                    {
                        imgDesseleciona2 = img;
                        btnDesseleciona2 = btn;
                    }
                }
                else
                    btn.IsChecked = false;

            }
        }

        private void carregaImagensBotoesDePostura(ref KinectToggleButton btn, ref Image img, ref int segundoClick)
        {
            if (carregaImagem == null)
            {
                if (segundoClick == 1)
                    segundoClick++;
                if (!btn.IsChecked && segundoClick == 2)
                {
                    if (img.Source == imgDesseleciona.Source)
                    {
                        imgDesseleciona.Opacity = 1;
                        btnDesseleciona.IsChecked = false;
                    }
                    else if (img.Source == imgDesseleciona2.Source)
                    {
                        imgDesseleciona2.Opacity = 1;
                        btnDesseleciona2.IsChecked = false;
                    }

                    segundoClick = 0;

                }
                btn.IsChecked = false;
                img.Source = null;
            }
            else
            {
                if (img.Source == null)
                {
                    segundoClick++;
                    img.Source = carregaImagem;
                    carregaImagem = null;
                    testaPosturas();
                }
                else
                    btn.IsChecked = !btn.IsChecked;
            }
        }

        #endregion

        #region botoes de certo e errado
        private void btnPosturaIncorreta_Click(object sender, RoutedEventArgs e)
        {
            carregaImagensBotoesDePostura(ref btnPosturaIncorreta, ref imgErrado, ref segundoClickErrado);
        }

        private void btnPosturaCorreta_Click(object sender, RoutedEventArgs e)
        {
            carregaImagensBotoesDePostura(ref btnPosturaCorreta, ref imgCerto, ref segundoClickCerto);
        }

        #endregion

        #region botoes de postura
        private void btnPostura1_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura1, img1Certo);

        }

        private void btnPostura2_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura2, img2Certo);
        }

        private void btnPostura3_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura3, img3Errado);
        }

        private void btnPostura4_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura4, img2Errado);

        }

        private void btnPostura5_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura5, img3Certo);
        }

        private void btnPostura6_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura6, img1Errado);

        }

        private void btnPostura7_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura7, img4Errado);
        }

        private void btnPostura8_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura8, img4Certo);

        }

        private void btnPostura9_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura9, img5Errado);

        }

        private void btnPostura11_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura11, img5Certo);

        }

        private void btnPostura10_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura10, img6Errado);

        }

        private void btnPostura12_Click(object sender, RoutedEventArgs e)
        {
            selecaoDeBotao(btnPostura12, img6Certo);

        }

        #endregion
        //www.herniadedisco.com.br/espaco-dr-coluna/cartilha-do-itc-vertebral/guia-de-postura-no-escritorio/
    }
}
