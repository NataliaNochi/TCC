using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes.Cadastro
{
    static class UsuarioDAO
    {
        public static void Cadastrar(UsuarioVO user)
        {
            string sql = "insert into CADASTRO_USUARIO (nome_usuario, cpf_usuario, email_usuario, " +
                         "senha_usuario) values ('{0}' ,'{1}' ,'{2}' ,'{3}')";

            sql = string.Format(sql, user.GetNome(), user.GetCPF(), user.GetEmail(), user.GetSenha());

            MetodosBanco.ExecutaSQL(sql);
        }

        public static UsuarioVO Login(string email, string senha)
        {
            string sql = "select * from CADASTRO_USUARIO where email_usuario = '" + email + "' and senha_usuario = '" + senha + "'";
            DataTable tabela = MetodosBanco.ExecutaSelect(sql);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaVO(tabela.Rows[0]);
        }
               

        private static UsuarioVO MontaVO(DataRow registro)
        {
            UsuarioVO user = new UsuarioVO();
            user.SetCPF(registro["cpf_usuario"].ToString());
            user.SetNome(registro["nome_usuario"].ToString());
            user.SetEmail(registro["email_usuario"].ToString());
            user.SetSenha(registro["senha_usuario"].ToString());
            return user;
        }
   
    }
}
