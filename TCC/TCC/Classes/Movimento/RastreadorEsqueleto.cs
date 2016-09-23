using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Windows;
using System.Threading;

namespace TCC.Classes.Movimento
{
    
    //Rastrear apenas objetos que herdem da classe base para Movimentos (BaseMovimentos) e que sejam objetos concretos
    public class RastreadorEsqueleto<T> : IRastreadorEsqueleto where T : BaseMovimentos, new()
    {
        private T movimento;
        public EnumEstadoRastreamento EstadoAtual { get; private set; }
        public event EventHandler MovimentoIdentificado;
        public event EventHandler MovimentoEmProgresso;

        public RastreadorEsqueleto()
        {
            EstadoAtual = EnumEstadoRastreamento.NaoIdentificado;
            movimento = Activator.CreateInstance<T>();
        }
        public static bool primeiraVez = true;

        public void Rastrear(Skeleton esqueletoUser)
        {
            EnumEstadoRastreamento EstadoNovo = movimento.Rastrear(esqueletoUser);

            if (EstadoNovo == EnumEstadoRastreamento.Identificado && EstadoAtual != EnumEstadoRastreamento.Identificado)
            {
                Evento(MovimentoIdentificado);
                MessageBox.Show("Identificado");
            }
            if (EstadoNovo == EnumEstadoRastreamento.EmExecucao && (EstadoAtual == EnumEstadoRastreamento.EmExecucao || EstadoAtual == EnumEstadoRastreamento.NaoIdentificado))
                Evento(MovimentoEmProgresso);

            EstadoAtual = EstadoNovo;
        }

        private void Evento(EventHandler evento)
        {
            if (evento != null)
                evento(movimento, new EventArgs());
            
        }

    }
}
