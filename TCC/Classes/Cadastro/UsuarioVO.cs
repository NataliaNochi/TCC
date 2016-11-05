using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace TCC.Classes.Cadastro
{
    public class UsuarioVO
    {
        string nome;
        string cpf;
        string email;
        string senha;

        public void SetNome(string Nome)
        {
           
                if (Nome.Trim().Length < 8 || Nome.Trim().IndexOf(" ") == -1)
                    throw new Exception("Nome inválido");
                else
                    nome = Nome;
            
          
        }

        public string GetNome()
        {
            return nome;
        }


        public void SetEmail(string Email)
        {
            if (Email.Trim().Length == 0 || Email.Length < 4 || Email.Trim().IndexOf('@') == -1 || Email.Trim().IndexOf('.') == -1
               || Email[0] == '.' || Email[0] == '@' || Email[Email.Length - 1] == '.' || Email[Email.Length - 1] == '@')
                throw new Exception("E-mail incorreto, por favor verificar.");

            else
                email = Email;
        }

        public string GetEmail()
        {
            return email;
        }


        public void SetSenha(string Senha)
        {
               if (Senha.Trim().Length < 6)
                    throw new Exception("Informe uma senha de pelo menos 6 dígitos");

                else
                    senha = Senha;            
        }
        
            public string GetSenha()
            {
                return senha; 
            }

        public void SetCPF(string CPF)
        {
           
            if (Validar(CPF) == false)
                    throw new Exception("CPF incorreto, por favor verificar");

                else
                    cpf = CPF;
            
        }

        public string GetCPF()
        {
            return cpf;
        }


        #region Validar CPF

        static bool Validar(string cpf)
        {
            string digitado;
            string calculado;
            int soma = 0;
            int resto;
            int d1;
            int d2;

            int[] mult1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] digito = new int[11];

            cpf = cpf.Replace(".", "").Replace("-", "").Trim();

            if (cpf.Length != 11)
                return false;

            switch (cpf)
            {
                case "11111111111":
                    return false;
                case "22222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
            }

            // Atribui a cada posição do vetor digito a posição correspondente na string cpf convertida para int
            try
            {
                digito[0] = Convert.ToInt32(cpf.Substring(0, 1));
                digito[1] = Convert.ToInt32(cpf.Substring(1, 1));
                digito[2] = Convert.ToInt32(cpf.Substring(2, 1));
                digito[3] = Convert.ToInt32(cpf.Substring(3, 1));
                digito[4] = Convert.ToInt32(cpf.Substring(4, 1));
                digito[5] = Convert.ToInt32(cpf.Substring(5, 1));
                digito[6] = Convert.ToInt32(cpf.Substring(6, 1));
                digito[7] = Convert.ToInt32(cpf.Substring(7, 1));
                digito[8] = Convert.ToInt32(cpf.Substring(8, 1));
                digito[9] = Convert.ToInt32(cpf.Substring(9, 1));
                digito[10] = Convert.ToInt32(cpf.Substring(10, 1));
            }
            catch
            {
                return false;
            }


            //Calcula 1º dig
            for (int n = 0; n <= mult1.GetUpperBound(0); n++)
                soma = soma + (mult1[n] * Convert.ToInt32(digito[n]));

            resto = soma % 11;

            if (resto < 2)
                d1 = 0;

            else
                d1 = 11 - resto;


            //Calcula 2º dig
            soma = 0;

            for (int n = 0; n <= mult2.GetUpperBound(0); n++)
                soma = soma + (mult2[n] * Convert.ToInt32(digito[n]));

            resto = soma % 11;

            if (resto < 2)
                d2 = 0;

            else
                d2 = 11 - resto;

            calculado = d1.ToString() + d2.ToString();
            digitado = digito[9].ToString() + digito[10].ToString();

            if (calculado == digitado)
                return (true);

            else
                return (false);

        }

        #endregion

    }
}
