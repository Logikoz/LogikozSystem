using MySql.Data.MySqlClient;
using System;

namespace LogikozSystem._Dep
{
    public class ManutencaoClass
    {
        stringControl conect = new stringControl();

        public void AtualizarManutencaoDB(UInt32 cod, string[] dados)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand att = new MySqlCommand("update clientes set maintenance_bool = @a, menssage = @b, backsystem_prev = @c where cod_cliente = @d", con))
                    {
                        att.Parameters.Add("@a", MySqlDbType.VarChar, 3).Value = dados[0];
                        att.Parameters.Add("@b", MySqlDbType.Text).Value = dados[1];
                        att.Parameters.Add("@c", MySqlDbType.TinyText).Value = dados[2];
                        att.Parameters.Add("@d", MySqlDbType.UInt32, 8).Value = cod;

                        att.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
