using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Classes.Movimento.Poses;
using Microsoft.Kinect;
using TCC.Classes.Base;

namespace TCC.Classes.Movimento.Gestos.Aceno
{
    public class AcenoMaoSobreCotovelo : BasePoses
    {
        protected override bool ValidarPosicao(Skeleton esqueletoUsuario)
        {
            double margemErro = 0.30;
            Joint maoDireita = esqueletoUsuario.Joints[JointType.HandRight];
            Joint cotoveloDireito = esqueletoUsuario.Joints[JointType.ElbowRight];

            bool maoDireitaAntesCotovelo = maoDireita.Position.X > cotoveloDireito.Position.X;
            bool maoDireitaSobreCotovelo = Util.CompararMargemErro(margemErro, maoDireita.Position.Y, cotoveloDireito.Position.Y);

            return maoDireitaSobreCotovelo && maoDireitaAntesCotovelo;
        }
    }
}
