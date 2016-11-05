using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace TCC.Classes.Movimento
{
    //Classe base para todos os movimentos
    public abstract class BaseMovimentos
    {
        //Contador de quadros  para saber em qual o movimento está em execução
        protected int contadorFrames { get; set; }
        public string Descricao { get; set; }

        public abstract EnumEstadoRastreamento Rastrear(Skeleton esqueletoUser);

        //Valida se a posição do usuário está de acordo com o movimento executado.
        protected abstract bool ValidarPosicao(Skeleton esqueletoUser);
        


    }
}
