using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Classes.Movimento.Poses;
using Microsoft.Kinect;

namespace TCC.Classes.Movimento.Gestos.Aceno
{
    public class AcenoMaoAntesCotovelo : BasePoses
    {
        protected override bool ValidarPosicao(Skeleton esqueletoUsuario)
        {
            Joint maoDireita = esqueletoUsuario.Joints[JointType.HandRight];
            Joint cotoveloDireito = esqueletoUsuario.Joints[JointType.ElbowRight];

            bool maoDireitaAntesCotovelo = maoDireita.Position.X < cotoveloDireito.Position.X;
            bool maoDireitaSobreCotovelo = maoDireita.Position.Y > cotoveloDireito.Position.Y;

            return maoDireitaSobreCotovelo && maoDireitaAntesCotovelo;
        }
    }
}
