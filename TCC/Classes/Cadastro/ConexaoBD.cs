using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes.Cadastro
{
    static class ConexaoBD
    {
        public static SqlConnection GetConexao()
        {
            string conexao = "user id=sa;password=123456;initial Catalog=TCC;data Source=.\\SQLEXPRESS";
            SqlConnection connect = new SqlConnection(conexao);
            connect.Open();
            return connect;
        }
    }
}
