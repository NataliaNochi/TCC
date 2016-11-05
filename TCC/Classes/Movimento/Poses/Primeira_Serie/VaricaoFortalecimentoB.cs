using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;

namespace TCC.Classes.Movimento.Poses.Primeira_Serie
{
    public class VariacaoFortalecimentoB : BasePoses
    {
         public VariacaoFortalecimentoB()
         {
             this.Descricao = " VariacaoFortalecimentoB";
             this.FrameIdentificacao = 10;
         }

         protected override bool ValidarPosicao(Skeleton esqueletoUser)
         {
             double margemErro = 0.05;

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

             //Lado direito
             bool cotoveloDistanciaOmbroD = cotoveloD.Position.X > ombroD.Position.X;
             bool cotoveloAlturaOmbroD = Util.CompararMargemErro(margemErro, cotoveloD.Position.Y, ombroD.Position.Y);
             bool cotoveloProfundidadeOmbroD = cotoveloD.Position.Z > ombroD.Position.Z;

             bool maoDistanciaCotoveloD = Util.CompararMargemErro(margemErro, maoD.Position.X, cotoveloD.Position.X);
             bool maoAlturaCotoveloD = Util.CompararMargemErro(margemErro, maoD.Position.Y, cotoveloD.Position.Y);
             bool maoProfundidadeOmbroD = maoD.Position.Z < ombroD.Position.Z;

             bool cabecaDistanciaEspinha = Util.CompararMargemErro(margemErro, cabeca.Position.X, espinha.Position.X);
             bool cabecaProfundidadeEspinha = Util.CompararMargemErro(margemErro, cabeca.Position.Z, espinha.Position.Z);

             bool joelhoDistanciaQuadrilD = joelhoD.Position.X > quadrilD.Position.X;
             bool joelhoAlturaQuadrilD = joelhoD.Position.Y < quadrilD.Position.Y;
             bool joelhoProfundidadeQuadrilD = joelhoD.Position.Z < quadrilD.Position.Z;

             bool peDistanciaJoelhoD = peD.Position.X < joelhoD.Position.X;


             //Lado esquerdo
             bool cotoveloDistanciaOmbroE = cotoveloE.Position.X < ombroE.Position.X;
             bool cotoveloAlturaOmbroE = Util.CompararMargemErro(margemErro, cotoveloE.Position.Y, ombroE.Position.Y);
             bool cotoveloProfundidadeOmbroE = cotoveloE.Position.Z > ombroE.Position.Z;

             bool maoDistanciaCotoveloE = Util.CompararMargemErro(margemErro, maoE.Position.X, cotoveloE.Position.X);
             bool maoAlturaCotoveloE = Util.CompararMargemErro(margemErro, maoE.Position.Y, cotoveloE.Position.Y);
             bool maoProfundidadeOmbroE = maoE.Position.Z < ombroE.Position.Z;

             bool joelhoDistanciaQuadrilE = joelhoE.Position.X < quadrilE.Position.X;
             bool joelhoAlturaQuadrilE = joelhoE.Position.Y < quadrilE.Position.Y;
             bool joelhoProfundidadeQuadrilE = joelhoE.Position.Z < quadrilE.Position.Z;

             bool peDistanciaJoelhoE = peE.Position.X > joelhoE.Position.X;

            return cotoveloDistanciaOmbroD && cotoveloAlturaOmbroD && cotoveloProfundidadeOmbroD
                 && maoDistanciaCotoveloD && maoAlturaCotoveloD && maoProfundidadeOmbroD
                 && joelhoDistanciaQuadrilD && joelhoAlturaQuadrilD && joelhoProfundidadeQuadrilD
                 && cotoveloDistanciaOmbroE && cotoveloAlturaOmbroE && cotoveloProfundidadeOmbroE
                 && maoDistanciaCotoveloE && maoAlturaCotoveloE && maoProfundidadeOmbroE
                 && joelhoDistanciaQuadrilE && joelhoAlturaQuadrilE && joelhoProfundidadeQuadrilE
                 && cabecaDistanciaEspinha && cabecaProfundidadeEspinha; //&& peDistanciaJoelhoD && peDistanciaJoelhoE; 

         }  
    }       
}