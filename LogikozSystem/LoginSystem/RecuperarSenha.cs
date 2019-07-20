using LogikozSystem._Dep;
using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LogikozSystem.LoginSystem
{
	class RecuperarSenha
	{
		public ushort CodigoRecuperacao()
		{
			return ushort.Parse(new Random().Next(1000, 9999).ToString());
		}

		public async Task<bool> Codigo(string arc)
		{
			using (WebClient req = new WebClient())
			{
				try
				{
					string result = await req.DownloadStringTaskAsync(new Uri(arc));

					if (!string.IsNullOrEmpty(result))
					{
						if (result == "SUCESSO")
						{
							return true;
						}
						else if (result == "EMAIL_NAO_EXISTE")
						{
							MessageBox.Show("Email informado nao existe\nVerifique se digitou corretamente.", "Problema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						}
						else if (result == "CODIGO_NAO_EXISTE")
						{
							MessageBox.Show("Codigo informado nao existe!\nVerifique se digitou corretamente.", "Problema", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						}
						else
						{
							MessageBox.Show("ERROR: " + result);
						}
					}
				}
				catch (Exception erro)
				{
					MessageBox.Show("Tipo: " + erro.GetType().FullName + "\n\nErro: " + erro.Message);
				}
			}
			return false;
		}

		public async void EnviarEmail(string emailTO, string cod)
		{
			using (SmtpClient EmailProtocolo = new SmtpClient())
			{
				EmailProtocolo.Host = "smtp.gmail.com";
				EmailProtocolo.Port = 587;
				EmailProtocolo.EnableSsl = true;
				EmailProtocolo.DeliveryMethod = SmtpDeliveryMethod.Network;
				EmailProtocolo.UseDefaultCredentials = false;

				#region credenciais
				string fromTo = "ruancarlos14clash@gmail.com";
				EmailProtocolo.Credentials = new NetworkCredential(fromTo, "ruancarlosc.s");
				#endregion

				using (MailMessage InformacoesMensagens = new MailMessage())
				{
					InformacoesMensagens.From = new MailAddress(fromTo);
					InformacoesMensagens.Subject = "Logikoz System - Recuperar Senha";
					InformacoesMensagens.IsBodyHtml = true;
					InformacoesMensagens.BodyEncoding = System.Text.Encoding.UTF8;

					string body = GetBody();

					body = body.Replace("#CODIGO#", cod.ToString());

					InformacoesMensagens.Body = body;

					if (!string.IsNullOrEmpty(emailTO))
					{
						try
						{
							Regex reg = new Regex(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})");
							if (reg.IsMatch(emailTO))
							{
								InformacoesMensagens.To.Add(emailTO);

								//enviando a msg
								await EmailProtocolo.SendMailAsync(InformacoesMensagens);

								TelaPrincipal.MensagemNoCanto.MessageQueue.Enqueue("Email de recuperaçao Enviado!");
							}
						}
						catch (Exception e)
						{
							MessageBox.Show("Tipo: " + e.GetType().FullName + "\n\nERRO: " + e.Message);
						}
					}
				}
			}
		}

		private static string GetBody()
		{
			return System.IO.File.ReadAllText(PegarInfos.CaminhoEmailTemplate);
		}
	}
}
