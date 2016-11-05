using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;

namespace TCC.Classes.Movimento.Poses.Primeira_Serie
{
    public class FortalecimentoOmbrosA : BasePoses
    {
        public FortalecimentoOmbrosA()
        {
            this.Descricao = "FortalecimentoOmbrosA";
            this.FrameIdentificacao = 15;
        }

        protected override bool ValidarPosicao(Skeleton esqueletoUser)
        {
            double margemErro = 0.05;

            Joint maoE = esqueletoUser.Joints[JointType.WristLeft];
            Joint cotoveloE = esqueletoUser.Joints[JointType.ElbowLeft];
            Joint ombroE = esqueletoUser.Joints[JointType.ShoulderLeft];
            Joint quadrilE = esqueletoUser.Joints[JointType.HipLeft];
            Joint joelhoE = esqueletoUser.Joints[JointType.KneeLeft];
            Joint peE = esqueletoUser.Joints[JointType.FootLeft];

            Joint espinha = esqueletoUser.Joints[JointType.Spine];
            Joint cabeca = esqueletoUser.Joints[JointType.Head];

            Joint maoD = esqueletoUser.Joints[JointType.WristRight];
            Joint cotoveloD = esqueletoUser.Joints[JointType.ElbowRight];
            Joint ombroD = esqueletoUser.Joints[JointType.ShoulderRight];
            Joint quadrilD = esqueletoUser.Joints[JointType.HipRight];
            Joint joelhoD = esqueletoUser.Joints[JointType.KneeRight];
            Joint peD = esqueletoUser.Joints[JointType.FootRight];

            //Lado Direito
            bool cotoveloDistanciaOmbroD = cotoveloD.Position.X > ombroD.Position.X;
            bool cotoveloAlturaOmbroD = cotoveloD.Position.Y < ombroD.Position.Y;
            bool cotoveloProfundidadeOmbroD = cotoveloD.Position.Z < ombroD.Position.Z;

            bool maoDistanciaCotoveloD = maoD.Position.X > cotoveloD.Position.X;
            bool maoAlturaOmbroD = Util.CompararMargemErro(margemErro, maoD.Position.Y, ombroD.Position.Y);
            bool maoProfundidadeCotoveloD = Util.CompararMargemErro(margemErro, maoD.Position.Z, cotoveloD.Position.Z);

            bool cotoveloAlturaEspinhaD = cotoveloD.Position.Y > espinha.Position.Y;

            bool joelhoDistanciaQuadrilD = joelhoD.Position.X > quadrilD.Position.X;
            bool joelhoAlturaQuadrilD = joelhoD.Position.Y < quadrilD.Position.Y;
            bool joelhoProfundidadeQuadrilD = joelhoD.Position.Z < quadrilD.Position.Z;

            bool peDistanciaJoelhoD = peD.Position.X < joelhoD.Position.X;

            bool cabecaDistanciaEspinha = Util.CompararMargemErro(margemErro, cabeca.Position.X, espinha.Position.X);
            bool cabecaProfundidadeEspinha = Util.CompararMargemErro(margemErro, cabeca.Position.Z, espinha.Position.Z);


            //Lado Esquerdo
            bool cotoveloDistanciaOmbroE = cotoveloE.Position.X < ombroE.Position.X;
            bool cotoveloAlturaOmbroE = cotoveloE.Position.Y < ombroE.Position.Y;
            bool cotoveloProfundidadeOmbroE = cotoveloE.Position.Z < ombroE.Position.Z;

            bool maoDistanciaCotoveloE = maoE.Position.X < cotoveloE.Position.X;
            bool maoAlturaOmbroE = Util.CompararMargemErro(margemErro, maoE.Position.Y, ombroE.Position.Y);
            bool maoProfundidadeCotoveloE = Util.CompararMargemErro(margemErro, maoE.Position.Z, cotoveloE.Position.Z);

            bool cotoveloAlturaEspinhaE = cotoveloE.Position.Y > espinha.Position.Y;

            bool joelhoDistanciaQuadrilE = joelhoE.Position.X < quadrilE.Position.X;
            bool joelhoAlturaQuadrilE = joelhoE.Position.Y < quadrilE.Position.Y;
            bool joelhoProfundidadeQuadrilE = joelhoE.Position.Z < quadrilE.Position.Z;

            bool peDistanciaJoelhoE = peE.Position.X > joelhoE.Position.X;

            return cotoveloDistanciaOmbroD && cotoveloAlturaOmbroD 
                && maoDistanciaCotoveloD && maoAlturaOmbroD
                && cotoveloDistanciaOmbroE && cotoveloAlturaOmbroE  
                && maoDistanciaCotoveloE && maoAlturaOmbroE &&
                joelhoDistanciaQuadrilD && joelhoAlturaQuadrilD && joelhoProfundidadeQuadrilD
                && joelhoDistanciaQuadrilE && joelhoAlturaQuadrilE && joelhoProfundidadeQuadrilE;
           /*
                && 
                && cotoveloAlturaEspinhaD && cotoveloAlturaEspinhaD && peDistanciaJoelhoD && peDistanciaJoelhoE
                && cabecaDistanciaEspinha && cabecaProfundidadeEspinha && cotoveloProfundidadeOmbroE && cotoveloProfundidadeOmbroD;*/

        }
    }
}

