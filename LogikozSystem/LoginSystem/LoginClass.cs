using API_Dependences;
using LogikozSystem._Dep;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LogikozSystem.LoginSystem
{
	class LoginClass : ExecutarComandos
	{
		private readonly stringControl conect = new stringControl();

		public void Criar(string l, string s, string em)
		{
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					using (MySqlCommand add = new MySqlCommand("insert into usuarios values (null, @a, @b, @c)", con))
					{
						add.Parameters.Add(new MySqlParameter("@a", l));
						add.Parameters.Add(new MySqlParameter("@b", s));
						add.Parameters.Add(new MySqlParameter("@c", em));

						_ = add.ExecuteNonQuery();
					}
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
				}
			}
		}

		public void Remover(string l, string s)
		{
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					using (MySqlCommand add = new MySqlCommand("delete from usuarios where binary login_user = @a and binary senha_user = @b", con))
					{
						add.Parameters.Add("@a", MySqlDbType.VarChar, 20).Value = l;
						add.Parameters.Add("@b", MySqlDbType.VarChar, 30).Value = s;

						add.ExecuteNonQuery();
					}
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
				}
			}
		}

		public Boolean Verificar(string l, string s)
		{
			s = CriptografarSenha_SHA256(CriptografarSenha_MD5(s));
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					using (MySqlCommand add = new MySqlCommand("select count(*) from usuarios where binary login_user = @a && binary senha_user = @b", con))
					{
						add.Parameters.Add(new MySqlParameter("@a", l));
						add.Parameters.Add(new MySqlParameter("@b", s));

						Byte v = Byte.Parse(add.ExecuteScalar().ToString());

						if (v == 1) return true;

						else return false;
					}
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
					return false;
				}
			}
		}

		public async Task<bool> VerificarLoginPHP(string l, string s)
		{
			Uri link = new Uri("http://" + PegarInfos.Url + "/validarlogin.php?user=" + l + "&pwd=" + CriptografarSenha_SHA256(CriptografarSenha_MD5(s)));

			using (WebClient w = new WebClient())
			{
				try
				{
					string result = await Task.Run(() => w.DownloadString(link));

					if (!string.IsNullOrEmpty(result) && result != "YES")
					{
						return false;
					}
					return true;
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
					return false;
				}
			}
		}

		public string[] PegarDados(string l, string em)
		{
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					using (MySqlCommand add = new MySqlCommand("select * from usuarios where binary login_user = @a || email_user = @c", con))
					{
						add.Parameters.Add(new MySqlParameter("@a", l));
						add.Parameters.Add(new MySqlParameter("@c", em));

						using (MySqlDataReader dados = add.ExecuteReader())
						{
							dados.Read();

							const Byte mTam = 18;
							string[] d = new string[mTam];

							for (Byte i = 0; i < mTam; i++)
							{
								d[i] = dados.GetString(i);
							}

							return d;
						}

					}
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
					return null;
				}
			}
		}

		public Byte[] PegarImagem(string l)
		{
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					using (MySqlCommand get = new MySqlCommand("select foto_user from usuarios where binary login_user = @a", con))
					{
						get.Parameters.Add(new MySqlParameter("@a", l));

						using (MySqlDataReader p = get.ExecuteReader())
						{
							Byte[] a = null;
							if (p.Read())
							{
								a = (Byte[])(p["foto_user"]);
							}
							return a;
						}
					}
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
					return null;
				}
			}
		}

		public void AtualizarDados(Byte[] foto, string[] dados)
		{
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					using (MySqlCommand att = new MySqlCommand("update usuarios set id = @a, nivel_acesso_user = @b, login_user = @c, senha_user = @d, nome_user = @e, cargo_user = @f, email_user = @g, data_nascimento_user = @h, nivel_atual_user = @i, numero_clientes_user = @j, minutos_registrados_user = @k, xp_atual_user = @l, recado_user = @m, logradouro_user = @n, numero_casa_user = @o, cidade_user = @p, estado_user = @q, pais_user = @r, foto_user = @s where login_user = @c", con))
					{
						_ = att.Parameters.Add(new MySqlParameter("@a", dados[0]));
						_ = att.Parameters.Add(new MySqlParameter("@b", dados[1]));
						_ = att.Parameters.Add(new MySqlParameter("@c", dados[2]));
						_ = att.Parameters.Add(new MySqlParameter("@d", dados[3]));
						_ = att.Parameters.Add(new MySqlParameter("@e", dados[4]));
						_ = att.Parameters.Add(new MySqlParameter("@f", dados[5]));
						_ = att.Parameters.Add(new MySqlParameter("@g", dados[6]));
						_ = att.Parameters.Add(new MySqlParameter("@h", dados[7]));
						_ = att.Parameters.Add(new MySqlParameter("@i", dados[8]));
						_ = att.Parameters.Add(new MySqlParameter("@j", dados[9]));
						_ = att.Parameters.Add(new MySqlParameter("@k", dados[10]));
						_ = att.Parameters.Add(new MySqlParameter("@l", dados[11]));
						_ = att.Parameters.Add(new MySqlParameter("@m", dados[12]));
						_ = att.Parameters.Add(new MySqlParameter("@n", dados[13]));
						_ = att.Parameters.Add(new MySqlParameter("@o", dados[14]));
						_ = att.Parameters.Add(new MySqlParameter("@p", dados[15]));
						_ = att.Parameters.Add(new MySqlParameter("@q", dados[16]));
						_ = att.Parameters.Add(new MySqlParameter("@r", dados[17]));
						_ = att.Parameters.Add(new MySqlParameter("@s", foto));


						MySqlDataReader dReader = att.ExecuteReader();
						dReader.Dispose();
					}
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
				}
			}
		}

		public BitmapImage Converter(Byte[] img)
		{
			//guardando array de Byte na memoria
			using (MemoryStream mStream = new MemoryStream(img))
			{
				//carregando uma nova imagem
				BitmapImage BmI = new BitmapImage();
				//iniciando o bitmap
				BmI.BeginInit();

				//armazenando a imagem na durante o carregamento
				BmI.CacheOption = BitmapCacheOption.OnLoad;
				//
				BmI.StreamSource = mStream;
				//finalizando o bitmap
				BmI.EndInit();

				//retornando a BitmapImage pronta!
				return BmI;
			}
		}

		public ushort[] PegarData(string l)
		{
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					ushort[] dados = new ushort[3];

					using (MySqlCommand getY = new MySqlCommand("select year(str_to_date(data_nascimento_user, '%Y-%m-%d')) from usuarios where login_user = @a", con))
					{
						getY.Parameters.Add(new MySqlParameter("@a", l));

						using (MySqlDataReader dReader = getY.ExecuteReader())
						{
							if (dReader.Read())
							{
								dados[0] = ushort.Parse(dReader.GetString(0));
							}
						}
					}

					using (MySqlCommand getM = new MySqlCommand("select month(str_to_date(data_nascimento_user, '%Y-%m-%d')) from usuarios where login_user = @b", con))
					{
						getM.Parameters.Add(new MySqlParameter("@b", l));

						using (MySqlDataReader dReader = getM.ExecuteReader())
						{
							dReader.Read();

							dados[1] = UInt16.Parse(dReader.GetString(0));
						}
					}

					using (MySqlCommand getD = new MySqlCommand("select day(str_to_date(data_nascimento_user, '%Y-%m-%d')) from usuarios where login_user = @c", con))
					{
						getD.Parameters.Add(new MySqlParameter("@c", l));

						using (MySqlDataReader dReader = getD.ExecuteReader())
						{
							dReader.Read();

							dados[2] = UInt16.Parse(dReader.GetString(0));
						}
					}

					return dados;
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
					return null;
				}
			}
		}

		public byte IdadeUsuario(ushort[] dados)
		{
			byte dia = byte.Parse(dados[2].ToString());
			byte mes = byte.Parse(dados[1].ToString());
			ushort ano = ushort.Parse(dados[0].ToString());

			byte dia_this = byte.Parse(DateTime.Now.Day.ToString());
			byte mes_this = byte.Parse(DateTime.Now.Month.ToString());
			ushort ano_this = ushort.Parse(DateTime.Now.Year.ToString());

			byte Idade = (byte)(ano_this - ano);

			if (ano_this - ano >= 0)
			{
				if (mes < mes_this) return Idade;

				if (mes == mes_this)
				{
					if (dia <= dia_this) return Idade;

					else return --Idade;
				}
				else return --Idade;
			}

			return --Idade;
		}

		public void AtualizarFotoPerfil(byte[] foto, string User)
		{
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					using (MySqlCommand att = new MySqlCommand("update usuarios set foto_user = @a where login_user = @b", con))
					{
						att.Parameters.Add(new MySqlParameter("@a", foto));
						att.Parameters.Add(new MySqlParameter("@b", User));

						MySqlDataReader dReader = att.ExecuteReader();
						dReader.Dispose();
					}
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
				}
			}
		}

		public void AtualizarNomePerfil(string Nome, string User)
		{
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					using (MySqlCommand att = new MySqlCommand("update usuarios set nome_user = @a where login_user = @b", con))
					{
						att.Parameters.Add(new MySqlParameter("@a", Nome));
						att.Parameters.Add(new MySqlParameter("@b", User));

						MySqlDataReader dReader = att.ExecuteReader();
						dReader.Dispose();
					}
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
				}
			}
		}

		public void AtualizarRecadoPerfil(string Recado, string User)
		{
			using (MySqlConnection con = new MySqlConnection(conect.MyBase_txt.Text))
			{
				try
				{
					con.Open();

					using (MySqlCommand att = new MySqlCommand("update usuarios set recado_user = @a where login_user = @b", con))
					{
						att.Parameters.Add(new MySqlParameter("@a", Recado));
						att.Parameters.Add(new MySqlParameter("@b", User));

						MySqlDataReader dReader = att.ExecuteReader();
						dReader.Dispose();
					}
				}
				catch (Exception e)
				{
					MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nErro: " + e.Message);
				}
			}
		}
	}
}
