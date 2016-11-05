using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace TCC.Classes.Movimento.Gestos
{
        public abstract class BaseGestos : BaseMovimentos
        {
            protected LinkedList<GestoFrameChave> QuadrosChave { get; set; }
            protected LinkedListNode<GestoFrameChave> QuadroChaveAtual { get; set; }
            private int contadorEtapas;
            private EnumEstadoRastreamento novoEstado;

            public int QuantidadeEtapas { get { return QuadrosChave.Count; } }

            public int PercentualEtapasConcluidas
            {
                get
                {
                    return contadorEtapas * 100 / QuantidadeEtapas;
                }
            }

            public override EnumEstadoRastreamento Rastrear(Skeleton esqueletoUsuario)
            {
                if (esqueletoUsuario != null)
                {
                    if (ValidarPosicao(esqueletoUsuario))
                    {
                        novoEstado = EnumEstadoRastreamento.EmExecucao;
                        if (QuadroChaveAtual.Value == QuadrosChave.Last.Value)
                            novoEstado = EnumEstadoRastreamento.Identificado;
                        else
                        {
                            if (contadorFrames >= QuadroChaveAtual.Value.QuadroLimiteInferior &&
                               contadorFrames <= QuadroChaveAtual.Value.QuadroLimiteSuperior)
                                ProximoQuadroChave();
                            else
                                if (contadorFrames < QuadroChaveAtual.Value.QuadroLimiteInferior)
                                PermanecerRastreando();
                            else if (contadorFrames > QuadroChaveAtual.Value.QuadroLimiteSuperior)
                                ReiniciarRastreamento();
                        }
                    }
                    else
                        if (QuadroChaveAtual.Value.QuadroLimiteSuperior < contadorFrames)
                        ReiniciarRastreamento();
                    else
                        PermanecerRastreando();
                }
                else
                    ReiniciarRastreamento();

                return novoEstado;
            }

            private void ProximoQuadroChave()
            {
                novoEstado = EnumEstadoRastreamento.EmExecucao;
                contadorFrames = 0;
                QuadroChaveAtual = QuadroChaveAtual.Next;
                contadorEtapas++;
            }

            private void ReiniciarRastreamento()
            {
                contadorFrames = 0;
                QuadroChaveAtual = QuadrosChave.First;
                contadorEtapas = 0;
                novoEstado = EnumEstadoRastreamento.NaoIdentificado;
            }

            private void PermanecerRastreando()
            {
                contadorFrames++;
                novoEstado = EnumEstadoRastreamento.EmExecucao;
            }

        }
    }
