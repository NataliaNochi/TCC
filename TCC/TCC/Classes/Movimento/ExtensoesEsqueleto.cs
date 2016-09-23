using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using TCC.Classes.Base;
using System.Windows.Controls;

namespace TCC.Classes.Movimento
{
    public static class ExtensoesEsqueleto
    {
        public static void DesenharEsqueleto(this SkeletonFrame frame, KinectSensor kinectSensor, Canvas desenhar)
        {
            if (kinectSensor == null)
                throw new ArgumentNullException("Erro: kinectSensor");

            if (desenhar == null)
                throw new ArgumentNullException("Erro: canvasParaDesenhar");

            Skeleton esqueleto = ObterEsqueletoUsuario(frame);

            if (esqueleto != null)
            {
                CriarEsqueleto eUser = new CriarEsqueleto(kinectSensor);

                foreach (BoneOrientation osso in esqueleto.BoneOrientations)
                {
                    eUser.DesenharOsso(esqueleto.Joints[osso.StartJoint], esqueleto.Joints[osso.EndJoint], desenhar);
                    eUser.DesenharArticulacao(esqueleto.Joints[osso.EndJoint], desenhar);
                }
            }
        }

        
        public static Skeleton ObterEsqueletoUsuario(this SkeletonFrame frame)
        {
            Skeleton eUser = null;
            Skeleton[] esqueletos = new Skeleton[frame.SkeletonArrayLength];
            frame.CopySkeletonDataTo(esqueletos);
            IEnumerable<Skeleton> esqueletosRastreados = esqueletos.Where(esqueleto => esqueleto.TrackingState == SkeletonTrackingState.Tracked);

            if (esqueletosRastreados.Count() > 0)
                eUser = esqueletosRastreados.First();

            return eUser;
        }
    }
}
