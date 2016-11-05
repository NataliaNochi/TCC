using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes.Cadastro
{
    static class Fase1DAO
    {
        public static void Cadastrar(Fase1VO fase1)
        {
            string sql = "insert into FASE_1 (cpf_usuario, data_f1, pontuacao_f1)" +
                         " values ('{0}' ,'{1}' ,'{2}')";

            sql = string.Format(sql, fase1.getCpf(), fase1.getData(), fase1.getPontuacao());

            MetodosBanco.ExecutaSQL(sql);
        }
    }
}
