using API_Dependences;
using LogikozSystem._Dep;
using System.Windows.Controls;
using System.Windows.Input;

namespace LogikozSystem.Manutencao
{
    /// <summary>
    /// Interação lógica para StatusManutençao.xam
    /// </summary>
    public partial class StatusManutencao : UserControl
    {
        public StatusManutencao()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            UsuariosClass user = new UsuariosClass();
            ManutencaoGrid.ItemsSource = user.ExibirDadosTabela("select cod_cliente, cliente, maintenance_bool, menssage, backsystem_prev from clientes").DefaultView;
            ManutencaoGrid.Columns[0].Header = "Codigo";
            ManutencaoGrid.Columns[1].Header = "Cliente";
            //ManutencaoGrid.Columns[2].Header = "Ativo";
            ManutencaoGrid.Columns[3].Header = "Menssagem";
            ManutencaoGrid.Columns[3].Width = 300;
            ManutencaoGrid.Columns[4].Header = "Retorno";
        }
    }
}
