using LogikozSystem.Licencas;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogikozSystem._Dep
{
    class LicencaClass
    {
        stringControl conect = new stringControl();

        public void Adicionar(string lic)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand add = new MySqlCommand("insert into licences values (null, 'NO', @a)", con))
                    {
                        add.Parameters.Add("@a", MySqlDbType.VarChar, 40).Value = lic;

                        add.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public Boolean Verificar(string lic)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();
                    using (MySqlCommand ver = new MySqlCommand("select count(*) from licences where binary licence = @a", con))
                    {
                        ver.Parameters.Add("@a", MySqlDbType.VarChar, 40).Value = lic;

                        Byte v = Byte.Parse(ver.ExecuteScalar().ToString());

                        if (v == 1) return true;

                        else return false;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void Remover(string lic)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand remove = new MySqlCommand("delete from licences where binary licence = @a", con))
                    {
                        remove.Parameters.Add("@a", MySqlDbType.VarChar, 30).Value = lic;

                        remove.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public string[] PegarDados(string lic)
        {
            string[] dados = new string[3];

            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand pe = new MySqlCommand("select * from licences where binary licence = @a", con))
                    {
                        pe.Parameters.Add("@a", MySqlDbType.VarChar, 30).Value = lic;

                        using (MySqlDataReader infos = pe.ExecuteReader())
                        {
                            infos.Read();

                            for (Byte i = 0; i < 3; i++)
                            {
                                dados[i] = infos.GetString(i);
                            }

                            return dados;
                        }
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
