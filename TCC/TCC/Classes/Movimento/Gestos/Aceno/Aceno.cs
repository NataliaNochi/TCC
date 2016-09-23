using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Classes.Movimento;
using Microsoft.Kinect;
using TCC.Classes.Base;


namespace TCC.Classes.Movimento.Gestos.Aceno
{
   /* public class Aceno : BaseGesto 
    {
        public Aceno()
        {
            InicializaQuadrosChave();
            Descricao = "Aceno";
            contadorFrames = 0;
            QuadroChaveAtual = QuadrosChave.First;
        }

        
        private void InicializaQuadrosChave()
        {
        QuadrosChave = new LinkedList<GestoFrameChave>();
        QuadrosChave.AddFirst(new GestoFrameChave(new AcenoCotoveloMao(), 0, 0));
        QuadrosChave.AddLast(new GestoFrameChave(new AcenoMaoSobreCotovelo(), 1, 25));
        QuadrosChave.AddLast(new GestoFrameChave(new AcenoMaoCotovelo(), 1, 25));

        QuadrosChave.AddLast(new GestoFrameChave(new AcenoMaoSobreCotovelo(), 1, 25));
        QuadrosChave.AddLast(new GestoFrameChave(new AcenoCotoveloMao(), 1, 25));
        QuadrosChave.AddLast(new GestoFrameChave(new AcenoMaoSobreCotovelo(), 1, 25));
        QuadrosChave.AddLast(new GestoFrameChave(new AcenoMaoCotovelo(), 1, 25));
        }
        protected override bool ValidarPosicao(Skeleton esqueletoUsuario)
        {
            EnumEstadoRastreamento estado = QuadroChaveAtual.Value.PoseChave.Rastrear(esqueletoUsuario);
            return estado == EnumEstadoRastreamento.Identificado;
        } 
    }*/

    
}