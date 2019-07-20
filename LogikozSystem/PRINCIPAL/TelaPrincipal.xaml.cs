using LogikozSystem._Dep;
using LogikozSystem.LoginSystem;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LogikozSystem
{
	/// <summary>
	/// Interação lógica para TelaPrincipal.xam
	/// </summary>
	public partial class TelaPrincipal : Window
	{
		//variavel static para armazenar a altura da janela.
		public static double altura_jan;
		//variavel static para armazenar a largura da janela.
		public static double largura_jan;
		//variavel static para armazanar propriedades da snackBar criada no xaml
		public static Snackbar MensagemNoCanto;
		//
		public static BitmapImage ImagemUsuario;
		//tela
		public static Grid tela;

		public TelaPrincipal()
		{
			InitializeComponent();
			MensagemNoCanto = MensagemSnack_sb;
			tela = GridPrincipal;
		}

		//Sincroniza informaçoes da tela principal!
		private void Sync_bt_Click(object sender, RoutedEventArgs e)
		{
			LoginClass login = new LoginClass();

			//atualiza imagem do usuario!
			login.PegarDados(infoLogin[1], string.Empty);
			ImagemUsuario = login.Converter(login.PegarImagem(infoLogin[1]));
		}

		//guarda as informaçoes de login
		public static string[] infoLogin = new string[5];

		//Fechar a janela principal
		private void Fechar_bt_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Deseja encerrar o programa?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				App.Current.Shutdown();
			}
		}

		//minimiza a janela principal
		private void Minimizar_bt_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		//inicializa metodos ao carregar o programa.
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			largura_jan = SystemParameters.PrimaryScreenWidth;
			altura_jan = SystemParameters.PrimaryScreenHeight;

			if (altura_jan <= 700 || largura_jan < 1024)
			{
				if (MessageBox.Show("A resoluçao da tela atual, é inferior ao considerado \"Usável\".\n" +
					"Por favor, aumente a resoluçao da tela! Recomendado (1024+ x 700+).\n" +
					"Clique em \'OK\' para encerrar o programa e trocar a resoluçao!", "Aviso", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
				{
					App.Current.Shutdown();
				}
			}

			//adicionando titulo do programa
			Title = PegarInfos.Nome + " " + PegarInfos.SobreNome + " " + PegarInfos.Versao;
			TituloTopo_txt.Text = Title;

			_ = tela.Children.Add(new LoginInicial());
		}
	}
}

