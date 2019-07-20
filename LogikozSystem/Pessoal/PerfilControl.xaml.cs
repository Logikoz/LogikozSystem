using LogikozSystem.LoginSystem;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogikozSystem.Pessoal
{
	/// <summary>
	/// Interação lógica para PerfilControl.xam
	/// </summary>
	public partial class PerfilControl : UserControl
	{
		public PerfilControl()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			LoginClass login = new LoginClass();

			login.PegarDados(TelaPrincipal.infoLogin[1], string.Empty);
			ImagemPerfilEditar_img.ImageSource = login.Converter(login.PegarImagem(TelaPrincipal.infoLogin[1]));

			//Adiciona informaçoes nos campos
			AlocandoValoresCampos();
		}

		private void AlocandoValoresCampos()
		{
			LoginClass login = new LoginClass();
			string[] dds = login.PegarDados(TelaPrincipal.infoLogin[1], string.Empty);

			//editaveis.
			//--------------------------------------
			NomePerfil_txt.Text = dds[4];
			RecadoPerfil_txt.Text = dds[12];
			LodradouroNumeroPerfil_txt.Text = dds[13] + ", " + dds[14];
			CidadeEstadoPerfil_txt.Text = dds[15] + " - " + dds[16];
			//---------------------------------------

			PaisPerfil_txt.Text = dds[17];
			CargoPerfil_txt.Text = dds[5].ToUpper();
			EmailPerfil_txt.Text = dds[6];
			IdadePerfil_txt.Text = login.IdadeUsuario(login.PegarData(dds[2])).ToString();
			NivelPerfil_txt.Text = dds[8];
			ClientesPerfil_txt.Text = dds[9];
			HorasRegistradasPerfil_txt.Text = string.Format("{0:0.##}", (float.Parse(dds[10]) / 60));


			XpAtualPerfil_pb.Text = dds[11];
			LevelSystem level = new LevelSystem();

			double xpAtual = level.PegarXP(dds[2]);
			double xpObj = level.CalcularXp_Maior_Objetivo(level.PegarLevel(dds[2]));
			double levelAntigoXP = level.CalcularXp_Maior_Objetivo(level.PegarLevelAntigo(dds[2]));
			if (levelAntigoXP == 100) levelAntigoXP = 0;
			ProgressoHorasPerfil_pb.Value = xpAtual;
			ProgressoHorasPerfil_pb.Maximum = xpObj;
			ProgressoHorasPerfil_pb.Minimum = levelAntigoXP;
			XpRequisitoPerfil_pb.Text = xpObj.ToString();
		}

		private void EditarRecadoPerfil_pi_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (EditarRecadoPerfil_pi.Kind == PackIconKind.Edit)
			{
				EditarRecadoPerfil_pi.Kind = PackIconKind.ContentSave;
				RecadoPerfil_txt.IsReadOnly = false;
				EditarRecadoPerfil_pi.ToolTip = "Salvar Alteraçoes";
				EditarRecadoPerfil_pi.Foreground = System.Windows.Media.Brushes.ForestGreen;
			}
			else if (EditarRecadoPerfil_pi.Kind == PackIconKind.ContentSave)
			{
				if (!string.IsNullOrEmpty(RecadoPerfil_txt.Text))
				{
					LoginClass login = new LoginClass();
					login.AtualizarRecadoPerfil(RecadoPerfil_txt.Text, TelaPrincipal.infoLogin[1]);

					EditarRecadoPerfil_pi.Kind = PackIconKind.Edit;
					RecadoPerfil_txt.IsReadOnly = true;
					EditarRecadoPerfil_pi.ToolTip = "Editar Recado";
					EditarRecadoPerfil_pi.Foreground = System.Windows.Media.Brushes.CornflowerBlue;
				}
			}
		}
		private void EditarNomePerfil_pi_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (EditarNomePerfil_pi.Kind == PackIconKind.Edit)
			{
				EditarNomePerfil_pi.Kind = PackIconKind.ContentSave;
				NomePerfil_txt.IsReadOnly = false;
				EditarNomePerfil_pi.ToolTip = "Salvar Alteraçoes";
				EditarNomePerfil_pi.Foreground = System.Windows.Media.Brushes.ForestGreen;
			}
			else if (EditarNomePerfil_pi.Kind == PackIconKind.ContentSave)
			{
				if (!string.IsNullOrEmpty(NomePerfil_txt.Text))
				{
					LoginClass login = new LoginClass();
					login.AtualizarNomePerfil(NomePerfil_txt.Text, TelaPrincipal.infoLogin[1]);

					EditarNomePerfil_pi.Kind = PackIconKind.Edit;
					NomePerfil_txt.IsReadOnly = true;
					EditarNomePerfil_pi.ToolTip = "Editar Nome";
					EditarNomePerfil_pi.Foreground = System.Windows.Media.Brushes.CornflowerBlue;
				}
			}
		}

		private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
		{
			string camFoto;

			OpenFileDialog procurarFoto = new OpenFileDialog
			{
				Title = "Procurar imagem para Perfil",
				Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png"
			};

			if (procurarFoto.ShowDialog() == true)
			{
				if (new FileInfo(procurarFoto.FileName).Length / 1000 > 2000)
				{
					_ = MessageBox.Show("A imagem escolhida é muito grande!\nEscolha uma imagem com até 2Mb", "Atençao", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return;
				}

				Bitmap imagem = new Bitmap(procurarFoto.FileName);
				if (imagem.Width != imagem.Height)
				{
					_ = MessageBox.Show("O ideal é uma imagem quadrada!\nEx: 400x400 px (recomendado)", "Atençao", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}

				camFoto = procurarFoto.FileName;
				ImagemPerfilEditar_img.ImageSource = new BitmapImage(new Uri(camFoto));

				//salva imagem
				if (!string.IsNullOrEmpty(camFoto))
				{
					FileStream stream = new FileStream(camFoto, FileMode.Open, FileAccess.Read);

					using (BinaryReader reader = new BinaryReader(stream))
					{
						byte[] dadosParaBanco = reader.ReadBytes((int)stream.Length);
						new LoginClass().AtualizarFotoPerfil(dadosParaBanco, TelaPrincipal.infoLogin[1]);

						_ = MessageBox.Show("Imagem Alterada com Sucesso!", "Oh Yeah", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
		}
		private void Ellipse_MouseDown_1(object sender, MouseButtonEventArgs e)
		{
			new ExibirImagem().ShowDialog();
		}
	}
}
