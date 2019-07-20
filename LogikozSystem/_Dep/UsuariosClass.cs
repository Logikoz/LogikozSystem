using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogikozSystem._Dep
{
    class UsuariosClass
    {
        stringControl conect = new stringControl();

        private DataTable AbrirConexaoDadosTabela(string cmd)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand exibir = new MySqlCommand(cmd, con))
                    {

                        MySqlDataAdapter da = new MySqlDataAdapter
                        {
                            SelectCommand = exibir
                        };

                        DataTable table = new DataTable();
                        da.Fill(table);

                        return table;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public DataTable ExibirDadosTabela(string cmd)
        {
            try
            {
                DataTable table = new DataTable();

                table = AbrirConexaoDadosTabela(cmd);

                return table;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
