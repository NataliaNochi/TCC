using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TCC.Classes.Cadastro
{
    static class MetodosBanco
    {
       
        public static DataTable ExecutaSelect(string sql)
        {
            SqlConnection connect = ConexaoBD.GetConexao();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connect);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                return tabela;
            }
            finally
            {
                connect.Close();
            }
        }

        public static void ExecutaSQL(string sql)
        {
            SqlConnection connect = ConexaoBD.GetConexao();

            try
            {
                SqlCommand comando = new SqlCommand(sql, connect);
                comando.ExecuteNonQuery();
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
