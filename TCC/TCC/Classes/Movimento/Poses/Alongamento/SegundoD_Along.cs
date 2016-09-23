using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;

namespace TCC.Classes.Movimento.Poses.Alongamento
{
    public class SegundoD_Along : BasePoses
    {
        //4º  lado direito - cotovelo atras da cabeça - ERRO
        public SegundoD_Along()
        {
            this.Descricao = "SegundoD_Along";
            this.FrameIdentificacao = 10;
        }

        protected override bool ValidarPosicao(Skeleton esqueletoUser)
        {
            double margemErro = 0.10;

            Joint maoE = esqueletoUser.Joints[JointType.HandLeft];
            Joint cotoveloE = esqueletoUser.Joints[JointType.ElbowLeft];
            Joint ombroE = esqueletoUser.Joints[JointType.ShoulderLeft];

            Joint maoD = esqueletoUser.Joints[JointType.HandRight];
            Joint cotoveloD = esqueletoUser.Joints[JointType.ElbowRight];
            Joint ombroD = esqueletoUser.Joints[JointType.ShoulderRight];

            Joint cabeca = esqueletoUser.Joints[JointType.Head];

            Joint punhoE = esqueletoUser.Joints[JointType.WristLeft];
            Joint punhoD = esqueletoUser.Joints[JointType.WristRight];

            //Esquerdo
            bool CotoveloDistanciaOmbroE = cotoveloE.Position.X < ombroE.Position.X;
            bool CotoveloAlturaOmbroE = cotoveloE.Position.Y > ombroE.Position.Y;

            bool maoDistanciaOmbroE = maoE.Position.X > ombroE.Position.X;
            bool maoAlturaCotoveloE = maoE.Position.Y > cotoveloE.Position.Y;

            bool maoAcimaCabecaE = maoD.Position.Y > cabeca.Position.Y; //Mão esquerda deve estar acima da cabeça

            bool maoEdistanciaCotoveloD = Util.CompararMargemErro(margemErro, cotoveloD.Position.X, maoE.Position.X); //Mão esquerda deve estar sobre o cotovelo direito
            bool maoEalturaCotoveloD = Util.CompararMargemErro(margemErro, cotoveloD.Position.Y, maoE.Position.Y); //Mão esquerda deve estar na mesma altura que o cotovelo direito
            bool maoEprofundidadeCotoveloD = Util.CompararMargemErro(margemErro, cotoveloD.Position.Z, maoE.Position.Z); //Mesma profundidade


            //Direita             
            bool cotoveloDitanciaCabeca = Util.CompararMargemErro(margemErro, cotoveloD.Position.X, cabeca.Position.X); //Cotovelo sobre a cabeça
            bool cotoveloAlturaCabeca = cotoveloD.Position.Y > cabeca.Position.Y; //Altura do cotovelo superior à cabeça

            //Mão direita sobre ombro esquerdo
            bool maoDdistanciaOmbroE = Util.CompararMargemErro(margemErro, maoD.Position.X, ombroE.Position.X);
            bool maoDalturaOmbroE = Util.CompararMargemErro(margemErro, maoD.Position.Y, ombroE.Position.Y);

            return CotoveloDistanciaOmbroE && CotoveloAlturaOmbroE &&
                   maoDistanciaOmbroE && maoAlturaCotoveloE &&
                   maoAcimaCabecaE &&
                   maoEdistanciaCotoveloD && maoEalturaCotoveloD && maoEprofundidadeCotoveloD &&
                   cotoveloDitanciaCabeca && cotoveloAlturaCabeca; //&&
                  // maoDdistanciaOmbroE && maoDalturaOmbroE;

        }
    }
}
