using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Windows;
using TCC.Classes.Base;


namespace TCC.Classes.Movimento.Poses.Alongamento
{
    public class QuartoD_Along : BasePoses
    {
        //6º Alogar braço na frente - ERRO
        public QuartoD_Along()
        {
            this.Descricao = "QuartoD_Along";
            this.FrameIdentificacao = 10;
        }

        protected override bool ValidarPosicao(Skeleton esqueletoUser)
        {
            double margemErro = 0.10;

            Joint maoD = esqueletoUser.Joints[JointType.HandRight];
            Joint maoE = esqueletoUser.Joints[JointType.HandLeft];
            Joint cotoveloD = esqueletoUser.Joints[JointType.ElbowRight];
            Joint cotoveloE = esqueletoUser.Joints[JointType.ElbowLeft];
            Joint ombroE = esqueletoUser.Joints[JointType.ShoulderLeft];
            Joint ombroC = esqueletoUser.Joints[JointType.ShoulderCenter];
            Joint ombroD = esqueletoUser.Joints[JointType.ShoulderRight];
                   
            //Braço esquerdo
            bool cotoveloDistanciaOmbroE = cotoveloE.Position.X > ombroE.Position.X; //Cotovelo deve estar após o ombro
            bool cotoveloAlturaOmbroE = cotoveloE.Position.Y < ombroE.Position.Y; //Cotovelo deve estar abaixo do ombro
            bool cotoveloProfundidadeOmbroE = Util.CompararMargemErro(margemErro, cotoveloE.Position.Z, ombroE.Position.Z);

            bool maoDistanciaCotoveloE = maoE.Position.X > cotoveloE.Position.X;  //mão após cotovelo
            bool maoAlturaCotoveloE = Util.CompararMargemErro(margemErro, cotoveloE.Position.Y, maoE.Position.Y); //Mão na mesma altura que o cotovelo
            bool maoProfundidadeCotoveloE = Util.CompararMargemErro(margemErro, cotoveloE.Position.Z, maoE.Position.Z);

            //Braço direito
            bool cotoveloAlturaOmbroD = cotoveloD.Position.Y < ombroD.Position.Y; //Cotovelo abaixo do ombro
            bool cotoveloDistanciaOmbroD = Util.CompararMargemErro(margemErro, cotoveloD.Position.X, ombroD.Position.X); //Ombro na mesma linha que o cotovelo
            bool cotoveloProfundidadeOmbroD = cotoveloD.Position.Z < ombroD.Position.Z; //Ombro mais profundo que o cotovelo

            bool maoDistanciaCotovelo = Util.CompararMargemErro(margemErro, cotoveloD.Position.X, maoD.Position.X); //Mão na mesma linha que o cotovelo
            bool maoAlturaCotovelo = maoD.Position.Y > cotoveloD.Position.Y;
            bool maoProfundidadeCotovelo = Util.CompararMargemErro(margemErro, maoD.Position.Z, cotoveloD.Position.Z); 

            bool cotoveloEalturaCotoveloD = cotoveloD.Position.Y < cotoveloE.Position.Y;
            bool cotoveloEdistanciaCotoveloD = cotoveloD.Position.X > cotoveloE.Position.X;
            bool cotoveloEprofundidadeCotoveloD = cotoveloD.Position.Z < cotoveloE.Position.Z;


            return cotoveloDistanciaOmbroE && cotoveloAlturaOmbroE && cotoveloProfundidadeOmbroE &&
                   maoDistanciaCotoveloE && maoAlturaCotoveloE && maoProfundidadeCotoveloE &&
                   cotoveloAlturaOmbroD && cotoveloDistanciaOmbroD && cotoveloProfundidadeOmbroD &&
                   maoDistanciaCotovelo && maoAlturaCotovelo && maoProfundidadeCotovelo &&
                   cotoveloEalturaCotoveloD && cotoveloEdistanciaCotoveloD && cotoveloEprofundidadeCotoveloD;

        }
    }
}
