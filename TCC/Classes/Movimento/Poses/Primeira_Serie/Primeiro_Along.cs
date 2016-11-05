using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Windows;
using TCC.Classes.Base;

namespace TCC.Classes.Movimento.Poses.Primeira_Serie
{
    public class Primeiro_Along : BasePoses
    {
        //1º Braços esticados para cima
        public Primeiro_Along()
        {
            this.Descricao = "Primeiro_Along";
            this.FrameIdentificacao = 10;
        }

        protected override bool ValidarPosicao(Skeleton esqueletoUser)
        {
            double margemErro = 0.10;

            Joint maoD = esqueletoUser.Joints[JointType.HandRight];
            Joint punhoD = esqueletoUser.Joints[JointType.WristRight];
            Joint cotoveloD = esqueletoUser.Joints[JointType.ElbowRight];
            Joint ombroD = esqueletoUser.Joints[JointType.ShoulderRight];

            Joint maoE = esqueletoUser.Joints[JointType.HandLeft];
            Joint punhoE = esqueletoUser.Joints[JointType.WristLeft];
            Joint cotoveloE = esqueletoUser.Joints[JointType.ElbowLeft];
            Joint ombroE = esqueletoUser.Joints[JointType.ShoulderLeft];

            Joint cabeca = esqueletoUser.Joints[JointType.Head];

            //Direito
            bool cotoveloAlturaOmbroD = cotoveloD.Position.Y > ombroD.Position.Y; //A altura do cotovelo deve ser superior a altura do ombro
            bool cotoveloDistanciaOmbroD = Util.CompararMargemErro(margemErro, cotoveloD.Position.X, ombroD.Position.X); //Cotovelo deve estar na linha do ombro
            bool cotoveloProfundidadeOmbroD = Util.CompararMargemErro(margemErro, cotoveloD.Position.Z, ombroD.Position.Z); //Profundidade do ombro e cotovelo devem ser as mesmas

            bool cotoveloAlturaPunhoD = cotoveloD.Position.Y < punhoD.Position.Y; //A altura do cotovelo deve ser inferior a altura do ombro 
            bool cotoveloDistanciaPunhoD = Util.CompararMargemErro(margemErro, cotoveloD.Position.X, punhoD.Position.X); //cotovelo deve estrar na linha do punho
            bool cotoveloProfundidadePunhoD = Util.CompararMargemErro(margemErro, cotoveloD.Position.Z, punhoD.Position.Z); //Profundidade do ombro e cotovelo devem ser as mesmas

            bool maoAlturaCabecaD = maoD.Position.Y > cabeca.Position.Y; //Mãos devem estar acima da cabeça
            bool maoDistanciaCabecaD = Util.CompararMargemErro(margemErro, maoD.Position.X, cabeca.Position.X); //Mãos devem estar alinhadas com a cabeça
            bool maoProfundidadeCabecaD = Util.CompararMargemErro(margemErro, maoD.Position.Z, cabeca.Position.Z); //Mesma profundidade

            //Esquerdo
            bool cotoveloAlturaOmbroE = cotoveloE.Position.Y > ombroE.Position.Y; //A altura do cotovelo deve ser superior a altura do ombro
            bool cotoveloDistanciaOmbroE = Util.CompararMargemErro(margemErro, cotoveloE.Position.X, ombroE.Position.X); //Cotovelo deve estar na linha do ombro
            bool cotoveloProfundidadeOmbroE = Util.CompararMargemErro(margemErro, cotoveloE.Position.Z, ombroE.Position.Z); //Profundidade do ombro e cotovelo devem ser as mesmas

            bool cotoveloAlturaPunhoE = cotoveloE.Position.Y < punhoE.Position.Y; //A altura do cotovelo deve ser inferior a altura do ombro 
            bool cotoveloDistanciaPunhoE = Util.CompararMargemErro(margemErro, cotoveloE.Position.X, punhoE.Position.X); //cotovelo deve estrar na linha do punho
            bool cotoveloProfundidadePunhoE = Util.CompararMargemErro(margemErro, cotoveloE.Position.Z, punhoE.Position.Z); //Profundidade do ombro e cotovelo devem ser as mesmas

            bool maoAlturaCabecaE = maoE.Position.Y > cabeca.Position.Y; //Mãos devem estar acima da cabeça
            bool maoDistanciaCabecaE = Util.CompararMargemErro(margemErro, maoE.Position.X, cabeca.Position.X); //Mãos devem estar alinhadas com a cabeça
            bool maoProfundidadeCabecaE = Util.CompararMargemErro(margemErro, maoE.Position.Z, cabeca.Position.Z);

            /*  //Mãos unidas - Não há como validar
            bool maosDistancia = Util.CompararMargemErro(margemErro, maoD.Position.X, maoE.Position.X); //Mão esquerda e direita unidas
            bool maosAltura = Util.CompararMargemErro(margemErro, maoD.Position.Y, maoE.Position.Y); //Mãos na mesma altura
            bool maosProfundidade = Util.CompararMargemErro(margemErro, maoD.Position.Z, maoE.Position.Z); //Mesma profundidade
            */

            return cotoveloAlturaOmbroD && cotoveloDistanciaOmbroD && cotoveloProfundidadeOmbroD &&
                   cotoveloAlturaPunhoD && cotoveloDistanciaPunhoD && cotoveloProfundidadePunhoD &&
                   maoAlturaCabecaD && maoDistanciaCabecaD && maoProfundidadeCabecaD &&
                   cotoveloAlturaOmbroE && cotoveloDistanciaOmbroE && cotoveloProfundidadeOmbroE &&
                   cotoveloAlturaPunhoE && cotoveloDistanciaPunhoE && cotoveloProfundidadePunhoE &&
                   maoAlturaCabecaE && maoDistanciaCabecaE && maoProfundidadeCabecaE;
                   /*&& maosDistancia && maosAltura && maosProfundidade;*/




        }
    }
}
