using LogikozSystem._Dep;
using System.Windows;
using System.Windows.Controls;

namespace LogikozSystem.Licencas
{
	/// <summary>
	/// Interação lógica para ListarLicencas.xam
	/// </summary>
	public partial class ListarLicencas : UserControl
	{
		public ListarLicencas()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			UsuariosClass usuarios = new UsuariosClass();
			LicencaGrid.ItemsSource = usuarios.ExibirDadosTabela("select use_bool, licence from licences").DefaultView;

			LicencaGrid.Columns[0].Header = "Em Uso";
			LicencaGrid.Columns[0].Width = 100;
			LicencaGrid.Columns[1].Header = "Licenças";
			LicencaGrid.Columns[1].Width = 300;
		}
	}
}
