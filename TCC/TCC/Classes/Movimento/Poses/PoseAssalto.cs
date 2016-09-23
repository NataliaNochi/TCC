using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;

namespace TCC.Classes.Movimento.Poses
{
    public class PoseAssalto : BasePoses
    {

        public PoseAssalto()
        {
            this.Descricao = "PoseAssalto";
            this.FrameIdentificacao = 10;
        }


        protected override bool ValidarPosicao(Skeleton esqueletoUser)
        {
            double margemErro = 0.10;

            Joint maoD = esqueletoUser.Joints[JointType.HandRight];
            Joint cotoveloD = esqueletoUser.Joints[JointType.ElbowRight];
            Joint ombroD = esqueletoUser.Joints[JointType.ShoulderRight];

            Joint maoE = esqueletoUser.Joints[JointType.HandLeft];
            Joint cotoveloE = esqueletoUser.Joints[JointType.ElbowLeft];
            Joint ombroE = esqueletoUser.Joints[JointType.ShoulderLeft];

            Joint ombroC = esqueletoUser.Joints[JointType.ShoulderCenter];

            //Direito
            bool maoDistanciaCotoveloD = Util.CompararMargemErro(margemErro, maoD.Position.X, cotoveloD.Position.X); //Mão deve estar alinhada ao cotovelo
            bool maoProfundidadeCotoveloD = Util.CompararMargemErro(margemErro, maoD.Position.Z, cotoveloD.Position.Z); //Mão e cotovelo devem estar na mesma profundidade
            bool maoAlturaCotoveloD = maoD.Position.Y > cotoveloD.Position.Y; //Altura da mão deve ser superior ao cotovelo

            bool ombroAlturaCotoveloD = Util.CompararMargemErro(margemErro, ombroD.Position.Y, cotoveloD.Position.Y); //Cotovelo deve estar na mesma altura do ombro
            bool ombroDistanciaCotoveloD = ombroC.Position.X < cotoveloD.Position.X; //Cotovelo Direito deve estar depois do ponto central do ombro

            //Esquerdo
            bool maoDistanciaCotoveloE = Util.CompararMargemErro(margemErro, maoE.Position.X, cotoveloE.Position.X); //Mão deve estar alinhada ao cotovelo
            bool maoProfundidadeCotoveloE = Util.CompararMargemErro(margemErro, maoE.Position.Z, cotoveloE.Position.Z); //Mão e cotovelo devem estar na mesma profundidade
            bool maoAlturaCotoveloE = maoE.Position.Y > cotoveloD.Position.Y;  //Altura da mão deve ser superior ao cotovelo         

            bool ombroAlturaCotoveloE = Util.CompararMargemErro(margemErro, ombroE.Position.Y, cotoveloE.Position.Y);  //Cotovelo deve estar na mesma altura do ombro
            bool ombroDistanciaCotoveloE = ombroC.Position.X > cotoveloE.Position.X; //Cotovelo Esquerdo deve estar antes do ponto central do ombro

            return maoDistanciaCotoveloD && maoAlturaCotoveloD && maoDistanciaCotoveloE && maoAlturaCotoveloE &&
                   maoProfundidadeCotoveloE && maoProfundidadeCotoveloD &&
                   ombroAlturaCotoveloD && ombroAlturaCotoveloE && ombroDistanciaCotoveloD && ombroDistanciaCotoveloE;


        }
    }
}
