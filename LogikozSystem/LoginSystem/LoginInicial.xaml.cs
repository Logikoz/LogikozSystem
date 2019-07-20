using API_Dependences;
using LogikozSystem._Dep;
using LogikozSystem.LoginSystem;
using LogikozSystem.PRINCIPAL.Contents;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogikozSystem.LoginSystem
{
	/// <summary>
	/// Lógica interna para LoginInicial.xaml
	/// </summary>
	public partial class LoginInicial : UserControl
	{
		public LoginInicial()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			IniciarVars();

			if (VerificaArquivoExiste())
			{
				using (StreamReader file = new StreamReader(pasta_local + arquivo_local))
				{
					string armazenar;
					int c = 0;
					while ((armazenar = file.ReadLine()) != null)
					{
						Usuario_txt.Items.Insert(c, armazenar);
						c++;
					}
				}
			}

			//adicionando titulo do grid
			NomePrograma_txt.Text = PegarInfos.Nome;
			SobreNomePrograma_txt.Text = PegarInfos.SobreNome;
		}

		private void IniciarVars()
		{
			Usuario_txt.Text = "";
			Senha_txt.Clear();
			lembrar_dados_tb.IsChecked = false;
		}

		public static string pasta_local = @".\dep\";
		public static string arquivo_local = @"usuarios.txt";

		public static Boolean VerificaArquivoExiste()
		{
			if (!File.Exists(pasta_local + arquivo_local))
			{
				return false;
			}
			return true;
		}

		private void AdicionarUsadosRelembrar()
		{
			if (!VerificaArquivoExiste())
			{
				if (!Directory.Exists(pasta_local))
				{
					Directory.CreateDirectory(pasta_local);
				}

				StreamWriter escrever = new StreamWriter(pasta_local + arquivo_local, true, Encoding.Default);
				escrever.WriteLine(Usuario_txt.Text);
				escrever.Dispose();

			}
			else
			{
				StreamReader ler = new StreamReader(pasta_local + arquivo_local);
				int retornado = -1;
				string linha;
				string itemProcurado = Usuario_txt.Text;

				while ((linha = ler.ReadLine()) != null)
				{
					if (linha == itemProcurado)
					{
						retornado = linha.IndexOf(itemProcurado, 0);
					}
				}
				ler.Dispose();

				if (retornado == -1)
				{
					StreamWriter escrever = new StreamWriter(pasta_local + arquivo_local, true, Encoding.Default);
					escrever.WriteLine(Usuario_txt.Text);
					escrever.Dispose();
				}
			}
		}

		private async void Logar_bt_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(Usuario_txt.Text) && !string.IsNullOrEmpty(Senha_txt.Password))
			{
				if (await new LoginClass().VerificarLoginPHP(Usuario_txt.Text, Senha_txt.Password))
				{
					//guarda info no array
					TelaPrincipal.infoLogin[1] = Usuario_txt.Text;

					//abre o principal
					TelaPrincipal.tela.Children.Clear();
					TelaPrincipal.tela.Children.Add(new TelaInicio());
					//p.GridPrincipal.Children.Clear();
					//_ = p.GridPrincipal.Children.Add(new TelaInicio());
				}
				else
				{
					MessageBox.Show("Usuario ou senha informados nao existe!\nVerifique se digitou corretamente.", "Opss!", MessageBoxButton.OK, MessageBoxImage.Warning);
					IniciarVars();
				}

				if (lembrar_dados_tb.IsChecked == true)
				{
					AdicionarUsadosRelembrar();
				}
			}
		}

		private void Fechar_janela_confirmLogin_bt_Click(object sender, RoutedEventArgs e)
		{
			App.Current.Shutdown();
		}

		private async void EnviarEmail_bt_Click(object sender, RoutedEventArgs e)
		{
			RecuperarSenha recover = new RecuperarSenha();

			UInt16 codigo = recover.CodigoRecuperacao();

			if (await recover.Codigo("http://" + PegarInfos.Url + "/armazenarcodigo.php?cod=" + codigo + "&email=" + EmailUsuario_txt.Text))
			{
				recover.EnviarEmail(EmailUsuario_txt.Text, codigo.ToString());
				EmailUsuario_txt.IsReadOnly = true;
			}
		}

		//variavel que guardará se o existe um email digitado, e se ele é valido
		Boolean EmailValido = false;

		private async void ConfirmarCodigo_bt_Click(object sender, RoutedEventArgs e)
		{
			RecuperarSenha rec = new RecuperarSenha();

			if (await rec.Codigo("http://" + PegarInfos.Url + "/validarcodigo.php?cod=" + CodigoDigitado_txt.Text + "&email=" + EmailUsuario_txt.Text))
			{
				NovaSenha ns = new NovaSenha();
				NovaSenha.codigo = CodigoDigitado_txt.Text;
				NovaSenha.email = EmailUsuario_txt.Text;
				ns.ShowDialog();
			}

			//limpando codigo que foi digitado
			CodigoDigitado_txt.Clear();
		}

		private void EmailUsuario_txt_TextChanged(object sender, TextChangedEventArgs e)
		{
			FrameworkElement fe = new FrameworkElement();
			Regex reg = new Regex(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})");
			if (!reg.IsMatch(EmailUsuario_txt.Text))
			{
				EmailUsuario_txt.Foreground = NovaCorFonte(fe, "Accent400");
				EmailValido = false;
			}
			else
			{
				EmailUsuario_txt.Foreground = NovaCorFonte(fe, "Primary400");
				EmailValido = true;
			}
		}

		private static SolidColorBrush NovaCorFonte(FrameworkElement fe, object resource)
		{
			return new SolidColorBrush() { Color = (Color)fe.FindResource(resource) };
		}

		private void TenhoOCodigo_bt_Click(object sender, RoutedEventArgs e)
		{
			if (!EmailValido)
			{
				MessageBox.Show("Voce precisa informar um email antes\nE precisa ser o mesmo que foi enviado o codigo!", "Haha", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
		}

		private void CodigoDigitado_txt_KeyDown(object sender, KeyEventArgs e)
		{
			CodigosProntos cod = new CodigosProntos();
			if (cod.CancelaNaoNumeros(sender, e))
			{
				e.Handled = false;
			}
			else e.Handled = true;
		}
	}
}
