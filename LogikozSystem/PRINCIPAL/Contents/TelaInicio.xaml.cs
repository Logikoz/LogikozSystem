using LogikozSystem._Dep;
using LogikozSystem.Clientes;
using LogikozSystem.Licencas;
using LogikozSystem.LoginSystem;
using LogikozSystem.Manutencao;
using LogikozSystem.Pessoal;
using LogikozSystem.Usuarios;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace LogikozSystem.PRINCIPAL.Contents
{
	/// <summary>
	/// Interaction logic for TelaInicio.xaml
	/// </summary>
	public partial class TelaInicio : UserControl
	{
		public TelaInicio()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			//seta o home
			Principal_gd.Children.Clear();
			_ = Principal_gd.Children.Add(new Home_Default());

			//timer para verificar tempo online
			DispatcherTimer AdicionarMaisTempo_TempoOnline = new DispatcherTimer
			{
				Interval = TimeSpan.FromMinutes(3)
			};
			AdicionarMaisTempo_TempoOnline.Tick += AdicionarMaisTempo_TempoOnline_Tick;
			AdicionarMaisTempo_TempoOnline.Start();


			//seta informaçoes do usuario
			LoginClass perfil = new LoginClass();

			_ = perfil.PegarDados(TelaPrincipal.infoLogin[1], string.Empty);
			ImagemUsuario_img.ImageSource = perfil.Converter(perfil.PegarImagem(TelaPrincipal.infoLogin[1]));
			//seta nome do usuario no topo
			UsuarioTop_txt.Text = TelaPrincipal.infoLogin[1];

			//adicionar valores nos campos (nome/sobre)
			NomePrograma_txt.Text = PegarInfos.Nome;
			SobreNomePrograma_txt.Text = PegarInfos.SobreNome;
		}

		//xp que será dado a cada determinado tempo...
		//metoto -> AdicionarMaisTempo_TempoOnline_Tick
		private const ushort XP_quantidade = 3;

		//metodo do timer de level.
		private void AdicionarMaisTempo_TempoOnline_Tick(object sender, EventArgs e)
		{

			LevelSystem level = new LevelSystem();
			string Usuario = TelaPrincipal.infoLogin[1];

			//pega o xp atual do usuario
			double xpUser = level.PegarXP(Usuario);
			//pega o nivel atual do usuario
			UInt32 nivelUser = level.PegarLevel(Usuario);
			//calcula a quantidade de xp que o usuario precisa para upar de nivel
			double xpObjetivo;
			//minutos online
			UInt32 minOnline = level.PegarMinutos(Usuario);

			//adiciona mais 3 no xp do usuario
			level.AtualizarXP(Usuario, xpUser += XP_quantidade);
			level.AtualizarMinutosOnline(Usuario, minOnline += XP_quantidade);

			//string[] dados = new string[] {xpUser.Tostring(), nivelUser.Tostring(), xpObjetivo.Tostring()};
			do
			{
				//atualiza novamente para evitar loop infinito
				xpUser = level.PegarXP(Usuario);
				xpObjetivo = level.CalcularXp_Maior_Objetivo(nivelUser);
				//da um levelUP no nivel do usuario
				level.AtualizarLevel(Usuario, ++nivelUser);
			}
			while (level.VerificarXp_Is_Objetivo(xpUser, xpObjetivo));
		}

		#region UsersControl

		private void HomeBack_bt_Click(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new Home_Default());
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new Cliente_add());
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new DeleteCliente());
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			GerarLicenca a = new GerarLicenca();
			a.ShowDialog();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			RemoverLicenca b = new RemoverLicenca();
			b.ShowDialog();
		}

		private void Button_Click_4(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new SuspenderCliente());
		}

		private void Button_Click_5(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new UnSuspenderCliente());
		}

		private void Button_Click_6(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new EditarCliente());
		}

		private void Button_Click_7(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new AtivarManutencao());
		}

		private void Button_Click_8(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new RemoverManutencao());
		}

		private void Button_Click_9(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new StatusManutencao());
		}
		private void PerfilUsuario_bt_Click(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new PerfilControl());
		}

		private void Button_Click_10(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new PegarSenhaConfig());
		}

		private void Button_Click_11(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new ListarLicencas());
		}
		private void Button_Click_12(object sender, RoutedEventArgs e)
		{
			Principal_gd.Children.Clear();
			Principal_gd.Children.Add(new NovoUsuario());
		}
		#endregion

		#region Expander Conta
		private void SairConta_bt_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("encerrar a sessao?", "Confirmar", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
			{
				App.Current.Shutdown();
				System.Windows.Forms.Application.Restart();
			}
		}
		#endregion

		private void NotificacaoUser_bt_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MudarNotificacao_ev.Kind = PackIconKind.Notifications;
			MudarNotificacao_ev.Foreground = Brushes.WhiteSmoke;
			MessageBox.Show("foi");
		}
	}
}
