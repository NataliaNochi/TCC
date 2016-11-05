using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;


namespace TCC.Classes.Movimento.Poses.Primeira_Serie
{
    public class FlexaoLateralD : BasePoses    //Página 44 - Tudo sobre pilates
    {
        public FlexaoLateralD()
        {
            this.Descricao = "FlexaoLateralD";
            this.FrameIdentificacao = 5;
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

            //Lado Esquerdo  
            bool ombroDistanciaCotoveloE = ombroE.Position.X < cotoveloE.Position.X;
            bool ombroAlturaCotoveloE = ombroE.Position.Y < cotoveloE.Position.Y;
            bool ombroProfundidadeCotoveloE = Util.CompararMargemErro(margemErro, ombroE.Position.Z, cotoveloE.Position.Z);

            bool cotoveloDistanciaMaoE = cotoveloE.Position.X < maoE.Position.X;
            bool cotoveloProfundidadeMaoE = Util.CompararMargemErro(margemErro, cotoveloE.Position.Z, maoE.Position.Z);

            bool maoAlturaCabecaE = maoE.Position.Y > cabeca.Position.Y;
            bool maoProfundidadeCabecaE = Util.CompararMargemErro(margemErro, cabeca.Position.Z, maoE.Position.Z);

            bool cabecaDistanciaEspinha = cabeca.Position.X > espinha.Position.X;
            bool cabecaProfundidadeEspinha = Util.CompararMargemErro(margemErro, cabeca.Position.Z, espinha.Position.Z);

            bool joelhoDistanciaQuadrilE = joelhoE.Position.X < quadrilE.Position.X;
            bool joelhoAlturaQuadrilE = joelhoE.Position.Y < quadrilE.Position.Y;
            bool joelhoProfundidadeQuadrilE = joelhoE.Position.Z < quadrilE.Position.Z;

            bool peDistanciaJoelhoE = peE.Position.X > joelhoE.Position.X;

            //Lado Direito

            bool ombroAlturaCotoveloD = ombroD.Position.Y > cotoveloD.Position.Y;
            bool ombroProfundidadeCotoveloD = Util.CompararMargemErro(margemErro, ombroD.Position.Z, cotoveloD.Position.Z);

            bool joelhoDistanciaQuadrilD = joelhoD.Position.X > quadrilD.Position.X;
            bool joelhoAlturaQuadrilD = joelhoD.Position.Y < quadrilD.Position.Y;
            bool joelhoProfundidadeQuadrilD = joelhoD.Position.Z < quadrilD.Position.Z;

            bool peDistanciaJoelhoD = peD.Position.X < joelhoD.Position.X;

            return ombroDistanciaCotoveloE && ombroAlturaCotoveloE && ombroProfundidadeCotoveloE
                   && cotoveloDistanciaMaoE && cotoveloProfundidadeMaoE
                   && maoAlturaCabecaE && maoProfundidadeCabecaE
                   && cabecaDistanciaEspinha && cabecaProfundidadeEspinha
                   && joelhoDistanciaQuadrilE && joelhoAlturaQuadrilE && joelhoProfundidadeQuadrilE
                   && ombroAlturaCotoveloD && ombroProfundidadeCotoveloD
                   && joelhoDistanciaQuadrilD && joelhoAlturaQuadrilD && joelhoProfundidadeQuadrilD;
                   //&& peDistanciaJoelhoD && peDistanciaJoelhoE;


        }
    }
}
