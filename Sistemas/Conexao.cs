using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLoja
{
    class Conexao
    {
        //CONEXÃO COM O BANCO DE DADOS LOCAL
        string conec = "SERVER=localhost; DATABASE=sistemaloja; UID=root; PWD=; PORT=;";
        public MySqlConnection con = null;

        public void AbrirCon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Open();
            }
            catch (Exception ex)
            {

            }
        }

        public void FecharCon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
