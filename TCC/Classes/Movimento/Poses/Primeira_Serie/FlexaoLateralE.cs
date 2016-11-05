using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;

namespace TCC.Classes.Movimento.Poses.Primeira_Serie
{
    public class FlexaoLateralE : BasePoses    //Página 44 - Tudo sobre pilates
    {
        public FlexaoLateralE()
        {
            this.Descricao = "FlexaoLateralE";
            this.FrameIdentificacao = 10;
        }


        protected override bool ValidarPosicao(Skeleton esqueletoUser)
        {
            double margemErro = 0.10;

            Joint maoE = esqueletoUser.Joints[JointType.HandLeft];
            Joint cotoveloE = esqueletoUser.Joints[JointType.ElbowLeft];
            Joint ombroE = esqueletoUser.Joints[JointType.ShoulderLeft];
            Joint quadrilE = esqueletoUser.Joints[JointType.HipLeft];
            Joint joelhoE = esqueletoUser.Joints[JointType.KneeLeft];
            Joint peE = esqueletoUser.Joints[JointType.FootLeft];

            Joint espinha = esqueletoUser.Joints[JointType.Spine];
            Joint cabeca = esqueletoUser.Joints[JointType.Head];

            Joint maoD = esqueletoUser.Joints[JointType.HandRight];
            Joint cotoveloD = esqueletoUser.Joints[JointType.ElbowRight];
            Joint ombroD = esqueletoUser.Joints[JointType.ShoulderRight];
            Joint quadrilD = esqueletoUser.Joints[JointType.HipRight];
            Joint joelhoD = esqueletoUser.Joints[JointType.KneeRight];
            Joint peD = esqueletoUser.Joints[JointType.FootRight];

            //Lado Direito  
            bool ombroDistanciaCotoveloD = ombroD.Position.X > cotoveloD.Position.X;
            bool ombroAlturaCotoveloD = ombroD.Position.Y < cotoveloD.Position.Y;
            bool ombroProfundidadeCotoveloD = Util.CompararMargemErro(margemErro, ombroD.Position.Z, cotoveloD.Position.Z);

            bool cotoveloDistanciaMaoD = cotoveloD.Position.X > maoD.Position.X;
            bool cotoveloProfundidadeMaoD = Util.CompararMargemErro(margemErro, cotoveloD.Position.Z, maoD.Position.Z);

            bool maoAlturaCabecaD = maoD.Position.Y > cabeca.Position.Y;
            bool maoProfundidadeCabecaD = Util.CompararMargemErro(margemErro, cabeca.Position.Z, maoD.Position.Z);

            bool cabecaDistanciaEspinha = cabeca.Position.X < espinha.Position.X;
            bool cabecaProfundidadeEspinha = Util.CompararMargemErro(margemErro, cabeca.Position.Z, espinha.Position.Z);

            bool joelhoDistanciaQuadrilD = joelhoD.Position.X > quadrilD.Position.X;
            bool joelhoAlturaQuadrilD = joelhoD.Position.Y < quadrilD.Position.Y;
            bool joelhoProfundidadeQuadrilD = joelhoD.Position.Z < quadrilD.Position.Z;

            bool peDistanciaJoelhoD = peD.Position.X < joelhoD.Position.X;

            //Lado Esquerdo

            bool ombroAlturaCotoveloE = ombroE.Position.Y > cotoveloE.Position.Y;
            bool ombroProfundidadeCotoveloE = Util.CompararMargemErro(margemErro, ombroE.Position.Z, cotoveloE.Position.Z);

            bool joelhoDistanciaQuadrilE = joelhoE.Position.X < quadrilE.Position.X;
            bool joelhoAlturaQuadrilE = joelhoE.Position.Y < quadrilE.Position.Y;
            bool joelhoProfundidadeQuadrilE = joelhoE.Position.Z < quadrilE.Position.Z;

            bool peDistanciaJoelhoE = peE.Position.X > joelhoE.Position.X;

            return ombroDistanciaCotoveloD && ombroAlturaCotoveloD && ombroProfundidadeCotoveloD
                   && cotoveloDistanciaMaoD && cotoveloProfundidadeMaoD
                   && maoAlturaCabecaD && maoProfundidadeCabecaD
                   && cabecaDistanciaEspinha && cabecaProfundidadeEspinha
                   && joelhoDistanciaQuadrilD && joelhoAlturaQuadrilD && joelhoProfundidadeQuadrilD
                   && ombroAlturaCotoveloE && ombroProfundidadeCotoveloE
                   && joelhoDistanciaQuadrilE && joelhoAlturaQuadrilE && joelhoProfundidadeQuadrilE;
                   //&& peDistanciaJoelhoD && peDistanciaJoelhoE;                                  


        }
    }
}
