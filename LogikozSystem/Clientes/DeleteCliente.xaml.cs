using LogikozSystem._Dep;
using API_Dependences;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogikozSystem.LoginSystem;

namespace LogikozSystem.Clientes
{
    /// <summary>
    /// Interação lógica para deleteCliente.xam
    /// </summary>
    public partial class DeleteCliente : UserControl
    {
        public DeleteCliente()
        {
            InitializeComponent();
        }

        private void AlocarDados()
        {
            ClienteClass verificar = new ClienteClass();
            string[] dadosDB = verificar.PegarDados(UInt32.Parse(cod_Cliente_txt.Text));

            Cliente_txt.Text = dadosDB[2];
            licence_txt.Text = dadosDB[3];
            days_txt.Text = dadosDB[4];
            criacao_txt.Text = dadosDB[5];
            suspenso_txt.Text = dadosDB[7];
        }

        private void LimparCampos()
        {
            Cliente_txt.Clear();
            licence_txt.Clear();
            days_txt.Clear();
            criacao_txt.Clear();
            suspenso_txt.Clear();

            botoes_stack.IsEnabled = false;
            campos_stack.IsEnabled = false;
            procurar_stack.IsEnabled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            main_grid.Height = TelaPrincipal.altura_jan - 170;
        }

        private void Procurar_bt_Click(object sender, RoutedEventArgs e)
        {
            CodigosProntos verific = new CodigosProntos();

            if (!string.IsNullOrEmpty(cod_Cliente_txt.Text))
            {
                ClienteClass ve = new ClienteClass();

                if (ve.Checar(UInt32.Parse(cod_Cliente_txt.Text)))
                {
                    procurar_stack.IsEnabled = false;
                    botoes_stack.IsEnabled = true;
                    campos_stack.IsEnabled = true;
                    
                    AlocarDados();
                }
                else
                {
                    MessageBox.Show("Cliente informado nao existe\nVerifique se digitou corretamente e tente novamente!", "Opss!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void Remover_bt_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voce realmente deseja deletar esse cliente do banco de dados?\nVoce pode apenas Suspender ele ou colocar em menutençao\n\nOBS: nao terá como recuperar os dados futuramente!", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ClienteClass d = new ClienteClass();
                ConfirmarSenha conf = new ConfirmarSenha();
                conf.ShowDialog();

                if (conf.Retornado)
                {
                    d.Remover(UInt32.Parse(cod_Cliente_txt.Text));
                    MessageBox.Show("Dados Removidos com sucesso!");
                    LimparCampos();
                }
            }
        }

        private void Limpar_bt_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Confirmar Clearning!", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                LimparCampos();
            }
        }

        //verifica se oque o cara digitou é diferente de um numero, caso, retorna.
        private void Cod_Cliente_txt_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos verificacoes = new CodigosProntos();
            verificacoes.CancelaNaoNumeros(sender, e);
        }
    }
}
