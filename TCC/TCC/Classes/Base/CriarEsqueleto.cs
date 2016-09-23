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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace TCC.Classes.Base
{
    public class CriarEsqueleto
    {
         private KinectSensor kinect;

        public CriarEsqueleto(KinectSensor kinect)
        {
            this.kinect = kinect;
        }

        #region Ajustar Articulação
        public ColorImagePoint ConverterCoordenadasArticulacoes(Joint joint, double larguraC, double alturaC)
        {
            //Posição da Articulação
            ColorImagePoint posicao = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(joint.Position, kinect.ColorStream.Format);

            //Regra de 3 para ajustar a posição das articulações ao tamanho da pessoa
            posicao.X = (int)(posicao.X * larguraC) / kinect.ColorStream.FrameWidth; //Largura
            posicao.Y = (int)(posicao.Y * alturaC) / kinect.ColorStream.FrameHeight; //Altura

            return posicao;
        }
        #endregion

        #region Configuração da Ellipse
        private Ellipse DadosDesenhoArticulacao(int diametro, int largura, Brush cor)
        {
            Ellipse desenhoArticulacao = new Ellipse();
            desenhoArticulacao.Height = diametro;
            desenhoArticulacao.Width = diametro;
            desenhoArticulacao.StrokeThickness = largura;
            desenhoArticulacao.Stroke = cor;
            return desenhoArticulacao;
        }
        #endregion

        #region  Desenhando as Articulações nas posições corretas
        public void DesenharArticulacao(Joint joint, Canvas desenho)
        {
            int diametro = joint.JointType == JointType.Head ? 50 : 10;
            int largura = 4;
            Brush cor = Brushes.Black;

            Ellipse desenhoArticulacao = DadosDesenhoArticulacao(diametro, largura, cor);

            ColorImagePoint posicaoArticulacao = ConverterCoordenadasArticulacoes(joint, desenho.ActualWidth, desenho.ActualHeight);

            double deslocamentoH = posicaoArticulacao.X - desenhoArticulacao.Width / 2;
            double deslocamentoV = posicaoArticulacao.Y - desenhoArticulacao.Height / 2;

            if (deslocamentoV >= 0 && deslocamentoV < desenho.ActualHeight && deslocamentoH >= 0 && deslocamentoH < desenho.ActualWidth)
            {
                Canvas.SetLeft(desenhoArticulacao, deslocamentoH);
                Canvas.SetTop(desenhoArticulacao, deslocamentoV);
                Canvas.SetZIndex(desenhoArticulacao, 100);

                desenho.Children.Add(desenhoArticulacao);
            }
        }
        #endregion

        #region Desenhar Ossos
        public void DesenharOsso(Joint origem, Joint destino, Canvas desenhar)
        {
            int largura = 4;
            Brush cor = Brushes.Gold;
            ColorImagePoint posicaoOrigem = ConverterCoordenadasArticulacoes(origem, desenhar.ActualWidth, desenhar.ActualHeight);
            ColorImagePoint posicaoDestino = ConverterCoordenadasArticulacoes(destino, desenhar.ActualWidth, desenhar.ActualHeight);
            Line tracarOsso = ComponenteVisualOsso(largura, cor, posicaoOrigem.X, posicaoOrigem.Y, posicaoDestino.X, posicaoDestino.Y);

            if (Math.Max(tracarOsso.X1, tracarOsso.X2) < desenhar.ActualWidth && Math.Min(tracarOsso.X1, tracarOsso.X2) > 0 &&
               Math.Max(tracarOsso.Y1, tracarOsso.Y2) < desenhar.ActualHeight && Math.Min(tracarOsso.Y1, tracarOsso.Y2) > 0)
            {
                desenhar.Children.Add(tracarOsso);
            }
        }
        
        private Line ComponenteVisualOsso(int largura, Brush cor, double origemX, double origemY, double destinoX, double destinoY)
        {
            Line tracarOsso = new Line();
            tracarOsso.StrokeThickness = largura;
            tracarOsso.Stroke = cor;
            tracarOsso.X1 = origemX;
            tracarOsso.X2 = destinoX;
            tracarOsso.Y1 = origemY;
            tracarOsso.Y2 = destinoY;
            return tracarOsso;
        }
        #endregion

    }
}
