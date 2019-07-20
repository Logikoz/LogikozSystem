using LogikozSystem._Dep;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogikozSystem.Pessoal
{
    public class LevelSystem
    {
        stringControl conect = new stringControl();

        public void AtualizarLevel(string usuario, UInt32 newLevel)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using(MySqlCommand att = new MySqlCommand("update usuarios set nivel_atual_user = @a where login_user = @b", con))
                    {
                        att.Parameters.Add(new MySqlParameter("@a", newLevel));
                        att.Parameters.Add(new MySqlParameter("@b", usuario));

                        att.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void AtualizarXP(string usuario, double XP)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand att = new MySqlCommand("update usuarios set xp_atual_user = @a where login_user = @b", con))
                    {
                        att.Parameters.Add(new MySqlParameter("@a", XP));
                        att.Parameters.Add(new MySqlParameter("@b", usuario));

                        att.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void AtualizarMinutosOnline(string usuario, UInt32 minutos)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand att = new MySqlCommand("update usuarios set minutos_registrados_user = @a where login_user = @b", con))
                    {
                        att.Parameters.Add(new MySqlParameter("@a", minutos));
                        att.Parameters.Add(new MySqlParameter("@b", usuario));

                        att.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public double PegarXP(string usuario)
        {
            using(MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using(MySqlCommand ver = new MySqlCommand("select xp_atual_user from usuarios where login_user = @a", con))
                    {
                        ver.Parameters.Add(new MySqlParameter("@a", usuario));

                        using(MySqlDataReader dReader = ver.ExecuteReader())
                        {
                            dReader.Read();

                            return dReader.GetUInt32(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public UInt32 PegarLevel(string usuario)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand ver = new MySqlCommand("select nivel_atual_user from usuarios where login_user = @a", con))
                    {
                        ver.Parameters.Add(new MySqlParameter("@a", usuario));

                        using (MySqlDataReader dReader = ver.ExecuteReader())
                        {
                            dReader.Read();

                            return dReader.GetUInt32(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public UInt32 PegarMinutos(string usuario)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand ver = new MySqlCommand("select minutos_registrados_user from usuarios where login_user = @a", con))
                    {
                        ver.Parameters.Add(new MySqlParameter("@a", usuario));

                        using (MySqlDataReader dReader = ver.ExecuteReader())
                        {
                            dReader.Read();

                            return dReader.GetUInt32(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public UInt32 PegarLevelAntigo(string usuario)
        {
            using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
            {
                try
                {
                    con.Open();

                    using (MySqlCommand ver = new MySqlCommand("select nivel_atual_user from usuarios where login_user = @a", con))
                    {
                        ver.Parameters.Add(new MySqlParameter("@a", usuario));

                        using (MySqlDataReader dReader = ver.ExecuteReader())
                        {
                            dReader.Read();

                            if(dReader.GetUInt32(0) == 0) return 0;

                            else return (dReader.GetUInt32(0) - 1);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public Boolean VerificarXp_Is_Objetivo(double XP, double Objetivo)
        {
            if (XP >= Objetivo) return true;

            else return false;
        }

        public double CalcularXp_Maior_Objetivo(/*UInt32 XP,*/ UInt32 Level)
        {
            if(Level > 0)
            {
                return (Level * 2.25) * 100;
            }

            return 100;
        }
    }
}
