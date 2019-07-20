using API_Dependences;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogikozSystem.Clientes
{
    /// <summary>
    /// Interação lógica para UnSuspenderCliente.xam
    /// </summary>
    public partial class UnSuspenderCliente : UserControl
    {
        stringControl conect = new stringControl();
        public UnSuspenderCliente()
        {
            InitializeComponent();
        }

        private void AlocarCampos()
        {
            ClienteClass verificar = new ClienteClass();

            string[] d = verificar.PegarDados(UInt32.Parse(Cod_Cliente_txt.Text));

            NomeCliente_txt.Text = d[2];
            EmailCliente_txt.Text = d[13];
            DiasRestantes_text.Text = d[4];
            DataCriacao_txt.Text = d[5];
        }

        private void Limpar()
        {
            Cod_Cliente_txt.Clear();
            NomeCliente_txt.Clear();
            EmailCliente_txt.Clear();
            DiasRestantes_text.Clear();
            DataCriacao_txt.Clear();

            Campos_sp.IsEnabled = false;
            UnSuspenderCliente_bt.IsEnabled = false;
        }

        private void Procurar_bt_Click(object sender, RoutedEventArgs e)
        {
            ClienteClass verificar = new ClienteClass();
            CodigosProntos codigos = new CodigosProntos();
            Verificar ver = new Verificar();

            if (Cod_Cliente_txt.Text != string.Empty && !codigos.PossuiLetras(Cod_Cliente_txt.Text))
            {
                if (ver.Suspensao(string.Empty, UInt32.Parse(Cod_Cliente_txt.Text), conect.MyBase_txt.Text))
                {
                    if (verificar.Checar(UInt32.Parse(Cod_Cliente_txt.Text)))
                    {
                        Campos_sp.IsEnabled = true;
                        UnSuspenderCliente_bt.IsEnabled = true;

                        AlocarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Este cliente nao existe, verifique se digitou corretamente!", "Not Found!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Limpar();
                    }
                }
                else
                {
                    MessageBox.Show("Este cliente nao está suspenso!\nVerifique se digitou o codigo corretamente.", string.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void UnSuspenderCliente_bt_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voce leu as menssagens acima?\nAo clicar em SIM, nao terar mais como voltar atras!", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ConfirmarSenha senha = new ConfirmarSenha();
                senha.ShowDialog();

                if (senha.Retornado)
                {
                    ClienteClass @class = new ClienteClass();
                    @class.UnSuspender(string.Empty, UInt32.Parse(Cod_Cliente_txt.Text));

                    MessageBox.Show("Cliente foi suspenso temporariamente com sucesso!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Cod_Cliente_txt_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos codigos = new CodigosProntos();
            codigos.CancelaNaoNumeros(sender, e);
        }
    }
}
