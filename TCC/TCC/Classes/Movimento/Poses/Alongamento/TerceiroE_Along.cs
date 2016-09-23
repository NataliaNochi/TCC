using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;
                           
namespace TCC.Classes.Movimento.Poses.Alongamento
{                          
    public class TerceiroE_Along : BasePoses
    {
        //5º Alogar pescoço lado esquerdo 
        public TerceiroE_Along()
        {
            this.Descricao = "TerceiroE_Along";
            this.FrameIdentificacao = 10;
        }

        protected override bool ValidarPosicao(Skeleton esqueletoUser)
        {
            double margemErro = 0.10;

            Joint maoE = esqueletoUser.Joints[JointType.HandLeft];
            Joint cotoveloE = esqueletoUser.Joints[JointType.ElbowLeft];
            Joint ombroE = esqueletoUser.Joints[JointType.ShoulderLeft];
       
            Joint cabeca = esqueletoUser.Joints[JointType.Head];
            Joint ombroC = esqueletoUser.Joints[JointType.ShoulderCenter];


            bool cotoveloDistanciaOmbroE = cotoveloE.Position.X < ombroE.Position.X; //Cotovelo deve estar antes do ponto central do ombro
            bool cotoveloAlturaOmbroE = cotoveloE.Position.Y > ombroE.Position.Y; //A altura do cotovelo deve ser superior ao ombro
            bool cotoveloProfundidadeOmbroE = Util.CompararMargemErro(margemErro, cotoveloE.Position.Z, ombroC.Position.Z); //Profundidade do ombro e cotovelo devem ser as mesmas

            bool maoAlturaCabeca = maoE.Position.Y > cabeca.Position.Y;
            bool maoDistanciaCabeca = Util.CompararMargemErro(margemErro, cabeca.Position.X, maoE.Position.X);
            bool cabecaDistanciaOmbro = cabeca.Position.X < ombroC.Position.X; //Cabeça deve estar tombada para a direita 

                               
            return cotoveloDistanciaOmbroE  && cotoveloAlturaOmbroE && cotoveloProfundidadeOmbroE                     
                && maoDistanciaCabeca && cabecaDistanciaOmbro && maoAlturaCabeca;
        }
    }
}
