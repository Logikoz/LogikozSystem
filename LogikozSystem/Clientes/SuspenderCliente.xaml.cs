using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using API_Dependences;
using LogikozSystem._Dep;
using LogikozSystem.LoginSystem;

namespace LogikozSystem.Clientes
{
    /// <summary>
    /// Interação lógica para SuspenderCliente.xam
    /// </summary>
    public partial class SuspenderCliente : UserControl
    {
        stringControl conect = new stringControl();
        public SuspenderCliente()
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
            SuspenderCliente_bt.IsEnabled = false;
        }

        private void Procurar_bt_Click(object sender, RoutedEventArgs e)
        {
            ClienteClass verificar = new ClienteClass();
            CodigosProntos codigos = new CodigosProntos();
            Verificar ver = new Verificar();

            if (Cod_Cliente_txt.Text != string.Empty && !codigos.PossuiLetras(Cod_Cliente_txt.Text))
            {
                if(!ver.Suspensao(string.Empty, UInt32.Parse(Cod_Cliente_txt.Text), conect.MyBase_txt.Text))
                {
                    if (verificar.Checar(UInt32.Parse(Cod_Cliente_txt.Text)))
                    {
                        Campos_sp.IsEnabled = true;
                        SuspenderCliente_bt.IsEnabled = true;

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
                    MessageBox.Show("Este cliente ja está suspenso!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void SuspenderCliente_bt_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voce leu as menssagens acima?\nAo clicar em SIM, nao terar mais como voltar atras!", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ConfirmarSenha senha = new ConfirmarSenha();
                senha.ShowDialog();
                
                if (senha.Retornado)
                {
                    ClienteClass @class = new ClienteClass();
                    @class.Suspender(string.Empty, UInt32.Parse(Cod_Cliente_txt.Text));

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
