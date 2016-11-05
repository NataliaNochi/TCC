using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace TCC.Classes.Movimento
{
    public interface IRastreadorEsqueleto
    {
        void Rastrear(Skeleton esqueletoUser);
        EnumEstadoRastreamento EstadoAtual { get; }
    }

}
