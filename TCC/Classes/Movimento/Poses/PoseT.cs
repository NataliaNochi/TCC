using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;

namespace TCC.Classes.Movimento.Poses
{
    //Pose para reconhecimento do esqueleto
    public class PoseT : BasePoses
    {
        public PoseT()
        {
            this.Descricao = "PoseT";
            this.FrameIdentificacao = 30;
        }


        //Compara a altura(eixo Y)  e a distância(eixo Z) das mãos e cotovelos em relação ao centro dos ombros
        //e se o eixo X das mãos estão mais distantes de seu respectivo cotovelo
        protected override bool ValidarPosicao(Skeleton esqueletoUser)
        {
            Joint centroOmbros = esqueletoUser.Joints[JointType.ShoulderCenter];
            Joint maoD = esqueletoUser.Joints[JointType.HandRight];
            Joint maoE = esqueletoUser.Joints[JointType.HandLeft];
            Joint cotoveloD = esqueletoUser.Joints[JointType.ElbowRight];
            Joint cotoveloE = esqueletoUser.Joints[JointType.ElbowLeft];

            double margemErro = 0.30;

            //Mão Direita
            bool AlturaCorretaMaoD = Util.CompararMargemErro(margemErro, maoD.Position.Y, centroOmbros.Position.Y);
            bool DistanciaCorretaMaoD = Util.CompararMargemErro(margemErro, maoD.Position.Z, centroOmbros.Position.Z);
            bool MaoDAposCotovelo = maoD.Position.X > cotoveloD.Position.X;

            //Mão Esquerda
            bool AlturaCorretaMaoE = Util.CompararMargemErro(margemErro, maoE.Position.Y, centroOmbros.Position.Y);
            bool DistanciaCorretaMaoE = Util.CompararMargemErro(margemErro, maoE.Position.Z, centroOmbros.Position.Z);
            bool MaoEAposCotovelo = maoE.Position.X < cotoveloE.Position.X;

            //Altura do cotovelo Direito e Esquerdo
            bool AlturaCorretaCotoveloD = Util.CompararMargemErro(margemErro, cotoveloD.Position.Y, centroOmbros.Position.Y);
            bool AlturaCorretaCotoveloE = Util.CompararMargemErro(margemErro, cotoveloE.Position.Y, centroOmbros.Position.Y);

            return AlturaCorretaCotoveloD && AlturaCorretaCotoveloE && DistanciaCorretaMaoD && DistanciaCorretaMaoE
                   && MaoDAposCotovelo && MaoEAposCotovelo && AlturaCorretaCotoveloD && AlturaCorretaCotoveloE;
        }
    }
}
