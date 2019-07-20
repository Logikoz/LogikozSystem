using LogikozSystem._Dep;
using LogikozSystem.LoginSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogikozSystem.Licencas
{
    /// <summary>
    /// Lógica interna para RemoverLicenca.xaml
    /// </summary>
    public partial class RemoverLicenca : Window
    {
        public RemoverLicenca()
        {
            InitializeComponent();
        }

        private void Fechar_janela_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        static string[] dados = new string[3];

        private void Procurar_bt_Click(object sender, RoutedEventArgs e)
        {
            Usada_txt.Clear();
            Remover_bt.IsEnabled = false;
            LicencaClass licenca = new LicencaClass();
            if (licenca.Verificar(CaixaLicenca_txt.Text))
            {
                dados = licenca.PegarDados(CaixaLicenca_txt.Text);
                Usada_txt.Text = dados[1];
                Remover_bt.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Está licença nao existe, verifique se digitou corretamente!", "Opps!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Usada_txt.Clear();
                CaixaLicenca_txt.Clear();
                Remover_bt.IsEnabled = false;
                return;
            }
        }

        private void Remover_bt_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Voce leu as menssagens acima?\nAo clicar em SIM, nao terar mais como voltar atras!", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ConfirmarSenha senha = new ConfirmarSenha();
                senha.ShowDialog();

                if (senha.Retornado)
                {
                    RemoverLicenca remover = new RemoverLicenca();
                    remover.AplicarComando();
                }
            }
        }

        public void AplicarComando()
        {
            LicencaClass licenca = new LicencaClass();
            licenca.Remover(dados[2]);

            MessageBox.Show("Concluido!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
