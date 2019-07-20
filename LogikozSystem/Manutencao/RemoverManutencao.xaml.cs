using API_Dependences;
using LogikozSystem._Dep;
using LogikozSystem.LoginSystem;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LogikozSystem.Manutencao
{
    /// <summary>
    /// Interação lógica para RemoverManutencao.xam
    /// </summary>
    public partial class RemoverManutencao : UserControl
    {
        stringControl conect = new stringControl();

        public RemoverManutencao()
        {
            InitializeComponent();
        }

        private void CodCliente_txt_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos verificacoes = new CodigosProntos();
            verificacoes.CancelaNaoNumeros(sender, e);
        }

        private void LimparCampos()
        {
            CodCliente_txt.Clear();
            NomeCliente_txt.Clear();
            Campos_sp.IsEnabled = false;
        }

        private void ProcurarCliente_txt_Click(object sender, RoutedEventArgs e)
        {
            CodigosProntos codigos = new CodigosProntos();

            if (codigos.PossuiLetras(CodCliente_txt.Text)) return;

            ClienteClass cliente = new ClienteClass();

            if(CodCliente_txt.Text != string.Empty)
            {
                Verificar verificar = new Verificar();

                if (cliente.Checar(UInt32.Parse(CodCliente_txt.Text)) && verificar.Manutencao(UInt32.Parse(CodCliente_txt.Text), conect.MyBase_txt.Text))
                {
                    string[] dados = cliente.PegarDados(UInt32.Parse(CodCliente_txt.Text));

                    NomeCliente_txt.Text = dados[2];

                    Campos_sp.IsEnabled = true;
                    DesativarManutencao_bt.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Usuario nao existe ou nao está com software em Manutençao!", "Opps", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    LimparCampos();
                    return;
                }
            }
        }

        private void DesativarManutencao_bt_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Voce deve confirmar sua senha", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                ConfirmarSenha confirmar = new ConfirmarSenha();
                confirmar.ShowDialog();

                if (confirmar.Retornado)
                {
                    ManutencaoClass manutencao = new ManutencaoClass();

                    string[] d = new string[3];
                    d[0] = "OFF";

                    manutencao.AtualizarManutencaoDB(UInt32.Parse(CodCliente_txt.Text), d);

                    MessageBox.Show("Este software agora nao está mais em Manutençao!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Cancelado!");
                }
            }
        }
    }
}
