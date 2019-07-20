using API_Dependences;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogikozSystem._Dep
{
    public class ClienteClass
    {
        stringControl conect = new stringControl();

        const Byte MaxTam = 15; //Tamanho maximo do vetor.

        private readonly string[] dadosCliente = new string[MaxTam];

        public Boolean Checar(UInt32 cod)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand ve = new MySqlCommand("select count(*) from clientes where cod_cliente = @a", con))
                    {
                        ve.Parameters.Add("@a", MySqlDbType.UInt32, 8).Value = cod;

                        Byte v = Byte.Parse(ve.ExecuteScalar().ToString());

                        if (v == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        public string[] PegarDados(UInt32 cod)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand pe = new MySqlCommand("select * from clientes where cod_cliente = @a", con))
                    {
                        pe.Parameters.Add("@a", MySqlDbType.UInt32, 8).Value = cod;

                        using (MySqlDataReader ler_pegar = pe.ExecuteReader())
                        {
                            ler_pegar.Read();

                            for (Byte i = 0; i < MaxTam; i++)
                            {
                                if (!string.IsNullOrEmpty(ler_pegar.GetString(i)))
                                {
                                    dadosCliente[i] = ler_pegar.GetString(i);
                                }
                                else
                                {
                                    dadosCliente[i] = "";
                                }
                            }

                            return dadosCliente;
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

                    return dadosCliente;
                }
            }
        }

        public void Adicionar(string[] info)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
                try
                {
                    con.Open();

                    using (MySqlCommand add = new MySqlCommand("insert into clientes (id, cod_cliente, cliente, licence, days, data_criacao, data_remocao, suspenso_bool, maintenance_bool, menssage, backsystem_prev, atualization_bool, link_att, email_cliente, contato, passconfig, suspenso_msg, produto_desc) values(null, @a, @b, @c, @d, @e, @f, @g, @h, @i, @j, @k, @l, @m, @n, @o, @p, @q)", con))
                    {
                        add.Parameters.Add(new MySqlParameter("@a", info[1]));
                        add.Parameters.Add(new MySqlParameter("@b", info[2]));
                        add.Parameters.Add(new MySqlParameter("@c", info[3]));
                        add.Parameters.Add(new MySqlParameter("@d", info[4]));
                        add.Parameters.Add(new MySqlParameter("@e", info[5]));
                        add.Parameters.Add(new MySqlParameter("@f", info[6]));
                        add.Parameters.Add(new MySqlParameter("@g", info[7]));
                        add.Parameters.Add(new MySqlParameter("@h", info[8]));
                        add.Parameters.Add(new MySqlParameter("@i", info[9]));
                        add.Parameters.Add(new MySqlParameter("@j", info[10]));
                        add.Parameters.Add(new MySqlParameter("@k", info[11]));
                        add.Parameters.Add(new MySqlParameter("@l", info[12]));
                        add.Parameters.Add(new MySqlParameter("@m", info[13]));
                        add.Parameters.Add(new MySqlParameter("@n", info[14]));
                        add.Parameters.Add(new MySqlParameter("@o", info[15]));
                        add.Parameters.Add(new MySqlParameter("@p", info[16]));
                        add.Parameters.Add(new MySqlParameter("@q", info[17]));


                        add.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    con.Close();
                }
        }

        public UInt32 GerarNumeroAleatorio(long min, long max)
        {
            UInt32 numeroGerado;

            ClienteClass verificacoes = new ClienteClass();

            do
            {
                Random aleatorio = new Random();
                numeroGerado = (UInt32)aleatorio.Next((int)min, (int)max);

            } while (verificacoes.Checar(numeroGerado));

            return numeroGerado;
        }

        public void Remover(UInt32 cod)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
                try
                {
                    con.Open();

                    using (MySqlCommand re = new MySqlCommand("delete from clientes where cod_cliente = @a", con))
                    {
                        re.Parameters.Add("@a", MySqlDbType.UInt32, 8).Value = cod;

                        re.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    con.Close();
                }
        }

        public void Suspender(string lic, UInt32 cod)
        {
            using(MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand sus = new MySqlCommand("update clientes set suspenso_bool = 'ON' where binary licence = @a || cod_cliente = @b", con))
                    {
                        sus.Parameters.Add("@a", MySqlDbType.VarChar, 30).Value = lic;
                        sus.Parameters.Add("@b", MySqlDbType.UInt32, 8).Value = cod;

                        sus.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                
            }
        }

        public void UnSuspender (string lic, UInt32 cod)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand unSus = new MySqlCommand("update clientes set suspenso_bool = 'OFF' where licence = @a || cod_cliente = @b", con))
                    {
                        unSus.Parameters.Add("@a", MySqlDbType.VarChar, 30).Value = lic;
                        unSus.Parameters.Add("@b", MySqlDbType.UInt32, 8).Value = cod;

                        unSus.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void AtualizarDados(string[] dados)
        {
            using(MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using(MySqlCommand att = new MySqlCommand("update clientes set id = @a, cod_cliente = @b, cliente = @c, licence = @d, days = @e, data_criacao = @f, data_remocao = @g, suspenso_bool = @h, maintenance_bool = @i, menssage = @j, backsystem_prev = @k, atualization_bool = @l, link_att = @m, email_cliente = @n, contato = @o where cod_cliente = @b", con))
                    {
                        att.Parameters.Add("@a", MySqlDbType.UInt16, 3).Value = dados[0];
                        att.Parameters.Add("@b", MySqlDbType.UInt32, 8).Value = dados[1];
                        att.Parameters.Add("@c", MySqlDbType.VarChar, 30).Value = dados[2];
                        att.Parameters.Add("@d", MySqlDbType.VarChar, 35).Value = dados[3];
                        att.Parameters.Add("@e", MySqlDbType.UByte, 2).Value = dados[4];
                        att.Parameters.Add("@f", MySqlDbType.VarChar, 10).Value = dados[5];
                        att.Parameters.Add("@g", MySqlDbType.VarChar, 10).Value = dados[6];
                        att.Parameters.Add("@h", MySqlDbType.VarChar, 3).Value = dados[7];
                        att.Parameters.Add("@i", MySqlDbType.VarChar, 3).Value = dados[8];
                        att.Parameters.Add("@j", MySqlDbType.VarChar, 500).Value = dados[9];
                        att.Parameters.Add("@k", MySqlDbType.VarChar, 50).Value = dados[10];
                        att.Parameters.Add("@l", MySqlDbType.VarChar, 3).Value = dados[11];
                        att.Parameters.Add("@m", MySqlDbType.VarChar, 50).Value = dados[12];
                        att.Parameters.Add("@n", MySqlDbType.VarChar, 50).Value = dados[13];
                        att.Parameters.Add("@o", MySqlDbType.VarChar, 20).Value = dados[14];

                        att.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public string PegarSenhaConfig(UInt32 cod)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using(MySqlCommand get = new MySqlCommand("select passconfig from clientes where binary cod_cliente = @a", con))
                    {
                        get.Parameters.Add(new MySqlParameter("@a", cod));

                        using(MySqlDataReader gravar = get.ExecuteReader())
                        {
                            gravar.Read();

                            return gravar.GetString(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public string AtualizarSenhaConfig(UInt32 cod)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using(MySqlCommand att = new MySqlCommand("update clientes set passconfig = @a where cod_cliente = @b", con))
                    {
                        ExecutarComandos exe = new ExecutarComandos();
                        string newPass = exe.SenhaCrip(25, 3);

                        att.Parameters.Add(new MySqlParameter("@a", exe.CriptografarSenha_MD5(newPass)));
                        att.Parameters.Add(new MySqlParameter("@b", cod));

                        att.ExecuteNonQuery();

                        return newPass;
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
