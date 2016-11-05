using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;

namespace TCC.Classes.Movimento.Poses.Alongamento
{
    public class TerceiroD_Along : BasePoses
    {
        //5º Alogar pescoço para o lado direito
        public TerceiroD_Along()
        {
            this.Descricao = "TerceiroD_Along";
            this.FrameIdentificacao = 10;
        }

        protected override bool ValidarPosicao(Skeleton esqueletoUser)
        {
            double margemErro = 0.10;

            Joint maoD = esqueletoUser.Joints[JointType.HandRight];
            Joint cotoveloD = esqueletoUser.Joints[JointType.ElbowRight];

            Joint cabeca = esqueletoUser.Joints[JointType.Head];
            Joint ombroC = esqueletoUser.Joints[JointType.ShoulderCenter];

            bool cotoveloDistanciaOmbroD = cotoveloD.Position.X > ombroC.Position.X; //Cotovelo deve estar após o ponto central do ombro
            bool cotoveloAlturaOmbroD = cotoveloD.Position.Y > ombroC.Position.Y; //A altura do cotovelo deve ser superior ao ombro
            bool cotoveloProfundidadeOmbroD = Util.CompararMargemErro(margemErro, cotoveloD.Position.Z, ombroC.Position.Z); //Profundidade do ombro e cotovelo devem ser as mesmas



            bool maoAlturaCabeca = maoD.Position.Y > cabeca.Position.Y;
            bool maoDistanciaCabeca = Util.CompararMargemErro(margemErro, cabeca.Position.X, maoD.Position.X);
            bool cabecaDistanciaOmbro = cabeca.Position.X > ombroC.Position.X; //Cabeça deve estar tombada para a direita 
                                                                                                                                                                                                
            return cotoveloDistanciaOmbroD && cotoveloDistanciaOmbroD && maoDistanciaCabeca
                   && cabecaDistanciaOmbro && cotoveloProfundidadeOmbroD && maoAlturaCabeca;
        }
    }
}