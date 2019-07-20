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
    /// Interação lógica para AtivarManutencao.xam
    /// </summary>
    public partial class AtivarManutencao : UserControl
    {
        stringControl conect = new stringControl();

        public AtivarManutencao()
        {
            InitializeComponent();
        }

        private void CodCliente_txt_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos codigos = new CodigosProntos();
            codigos.CancelaNaoNumeros(sender, e);
        }

        private void LimparCampos()
        {
            CodCliente_txt.Clear();
            HorarioSelecionado_tp.Text = string.Empty;
            DataSelecionada_dp.Text = string.Empty;
            
        }

        private void ProcurarCliente_txt_Click(object sender, RoutedEventArgs e)
        {
            CodigosProntos codigos = new CodigosProntos();

            if (codigos.PossuiLetras(CodCliente_txt.Text)) return;

            Verificar ver = new Verificar();

            if(CodCliente_txt.Text != string.Empty)
            {
                ClienteClass cliente = new ClienteClass();
                if(!ver.Manutencao(UInt32.Parse(CodCliente_txt.Text), conect.MyBase_txt.Text) && cliente.Checar(UInt32.Parse(CodCliente_txt.Text)))
                {
                    string[] d = cliente.PegarDados(UInt32.Parse(CodCliente_txt.Text));

                    NomeCliente_txt.Text = d[2];
                    mensagemManutenance_txt.Text = d[9];


                    Campos_sp.IsEnabled = true;
                    Manutençao.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Cliente informado nao existe, ou ja está com a manutençao ativa!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private Boolean VerificarCamposIsEmpty()
        {
            if (mensagemManutenance_txt.Text == string.Empty || DataSelecionada_dp.Text == string.Empty || HorarioSelecionado_tp.Text == string.Empty) return true;

            return false;
        }

        private void Manutençao_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarCamposIsEmpty())
            {
                MessageBox.Show("Por favor preencha todos os campos!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ConfirmarSenha confirm = new ConfirmarSenha();

            if (MessageBox.Show("Confirme sua senha para continuar!", string.Empty, MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                confirm.ShowDialog();

                string[] dadosInfo = new string[3];
                dadosInfo[0] = "ON";
                dadosInfo[1] = mensagemManutenance_txt.Text;

                CodigosProntos codigos = new CodigosProntos();
                dadosInfo[2] = DataSelecionada_dp.Text + " as " + HorarioSelecionado_tp.Text;

                if (confirm.Retornado)
                {

                    ManutencaoClass maintenance = new ManutencaoClass();
                    maintenance.AtualizarManutencaoDB(UInt32.Parse(CodCliente_txt.Text), dadosInfo);

                    MessageBox.Show("Esse cliente agora possui seu software em manutençao!\nPrevisao de Retorno: " + dadosInfo[2], "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Cancelado!");
                    return;
                }
            }
        }
    }
}
