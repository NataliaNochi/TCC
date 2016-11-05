using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes.Cadastro
{
    class Fase1VO
    {
        DateTime data;
        int pontuacao;
        string cpf;

        public void setData(DateTime dt)
        {
            if (DateTime.Today.Day > dt.Day || DateTime.Today.Day < dt.Day)
            {
                throw new Exception("Dia não pode ser anterior ou posterior ao atual.");
            }
            else
            {
                data = dt;
            }
        }

        public DateTime getData()
        {
            return data;
        }

        public void setPontuacao(int pt)
        {
            pontuacao = pt;
        }

        public int getPontuacao()
        {
            return pontuacao;
        }

        public void setCpf(string CPF)
        {
            if (CPF.Length > 0)
            {
                cpf = CPF;
            }
            else
            {
                throw new Exception("Insira o CPF corretamente");
            }
        }

        public string getCpf()
        {
            return cpf;
        }
    }
}
