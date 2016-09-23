using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Classes.Movimento;
using Microsoft.Kinect;

namespace TCC.Classes.Movimento.Gestos
{
    public abstract class BaseGesto : BaseMovimentos
    {
        protected LinkedList<GestoFrameChave> QuadrosChave { get; set; }
        protected LinkedListNode<GestoFrameChave> QuadroChaveAtual { get; set; }
        private int contadorEtapas;

        public int QuantidadeEtapas
        {
            get
            {
                return QuadrosChave.Count;
            }
        }

        public int PercentualEtapasConcluidas
        {
            get
            {
                return contadorEtapas * 100 / QuantidadeEtapas;
            }
        }

      /*  private void ReiniciarRastreamento()
        {
            ContadorFrames = 0;
            QuadroChaveAtual = QuadrosChave.First;
            contadorEtapas = 0;
            novoEstado = EnumEstadoRastreamento.NaoIdentificado;
        }


        
    public override  EnumEstadoRastreamento Rastrear(Skeleton esqueletoUsuario)
    {
        return; //alterar
    }
          */
    }
}
