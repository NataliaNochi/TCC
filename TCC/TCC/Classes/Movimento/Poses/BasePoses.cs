using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Windows;

namespace TCC.Classes.Movimento.Poses
{
    //Classe base para todas as poses
    public abstract class BasePoses : BaseMovimentos
    {
        //Para que uma pose tenha validade é necessário que ela esteja sendo feita por um período de tempo.
        //Marcar em qual frame a pose é reconhecida

        protected int FrameIdentificacao { get; set; }

        public override EnumEstadoRastreamento Rastrear(Skeleton esqueletoUser)
        {
            EnumEstadoRastreamento estadoNovo;
            if (esqueletoUser != null && ValidarPosicao(esqueletoUser)) //Se o usuário estiver em uma posição valida
            {
                if (FrameIdentificacao == contadorFrames) //Valida também o número de quadros em que ele está nessa posição, se for igual ao número definido para validação
                    estadoNovo = EnumEstadoRastreamento.Identificado; //O estado do rastreamento é alterado para identificado

                else //Senão o rastreamento está em execução
                {
                    estadoNovo = EnumEstadoRastreamento.EmExecucao;
                    contadorFrames++;
                }
            }
            else //Se a posição não for válida o rastreamento não é identificado e o contador de quadros é zerado
            {
                estadoNovo = EnumEstadoRastreamento.NaoIdentificado;
                contadorFrames = 0;
            }
            return estadoNovo;
        }


        //Progresso da pose
        public int PercentualProgresso
        {
            get { return contadorFrames * 100 / FrameIdentificacao; }
        }


    }
}
