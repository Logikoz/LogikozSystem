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
    /// Interação lógica para Cliente_add.xam
    /// </summary>
    public partial class Cliente_add : UserControl
    {
        public Cliente_add()
        {
            InitializeComponent();
        }

        private void LimparCampos()
        {
            cliente_txt.Clear();
            email_txt.Clear();
            licenca_txt.Clear();
            dias_txt.Clear();
            Contato.Clear();

            AtivarOpcoes.IsChecked = false;
            manutencao_txt.Text = null;
            suspensao_txt.Text = null;
            mensagemManutenance_txt.Clear();
            DataSelecionada_dp.Text = null;
            HorarioSelecionado_tp.Text = null;
        }

        private Boolean VerificarCamposVazios()
        {
            if(cliente_txt.Text == string.Empty || licenca_txt.Text == string.Empty || dias_txt.Text == string.Empty || email_txt.Text == string.Empty || Contato.Text == string.Empty)
            {
                MessageBox.Show("Voce precisa preencher todos os campos!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return true;
            }

            if (AtivarOpcoes.IsChecked == true)
            {
                if(manutencao_txt.Text == string.Empty || suspensao_txt.Text == string.Empty || mensagemManutenance_txt.Text == string.Empty || DataSelecionada_dp.Text == string.Empty || HorarioSelecionado_tp.Text == string.Empty)
                {
                    MessageBox.Show("Todos os campos Opcionais devem ser preenchidos quando o \"Habilitar\" estiver Ativo", string.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return true;
                }
            }
            return false;
        }

        public string[] valores = new string[18];

        private void GuardarValoresDosCampos()
        {
            CodigosProntos prontos = new CodigosProntos();
            ExecutarComandos exe = new ExecutarComandos();
            ClienteClass adicionar = new ClienteClass();

            valores[1] = adicionar.GerarNumeroAleatorio(10000000, 99999999).ToString();
            valores[2] = cliente_txt.Text;
            valores[3] = licenca_txt.Text;
            valores[4] = dias_txt.Text;
            valores[5] = prontos.PegarDataAtual();
            valores[6] = prontos.PegarDataAtual_Incrementada(Byte.Parse(dias_txt.Text));

            if (AtivarOpcoes.IsChecked == true)
            {
                valores[7] = suspensao_txt.Text;
                valores[8] = manutencao_txt.Text;
                valores[9] = mensagemManutenance_txt.Text;
                valores[10] = DataSelecionada_dp.Text + " as " + HorarioSelecionado_tp.Text;
            }
            else
            {
                valores[7] = "OFF";
                valores[8] = "OFF";
                valores[9] = "";
                valores[10] = "";
            }


            valores[13] = /*(string.IsNullOrEmpty(email_txt.Text)) ? "" : */email_txt.Text;
            valores[14] = Contato.Text;
            valores[15] = exe.SenhaCrip(30, 4);
            valores[16] = "";
            valores[17] = "";

            for(Byte i = 0; i < 15; i++)
            {
                if(valores[i] == null)
                {
                    valores[i] = "";
                }
            }
        }
        
        private void AtivarCamposOptions()
        {
            manutencao_txt.IsEnabled = true;
            suspensao_txt.IsEnabled = true;
            DataHora_sp.IsEnabled = true;
            mensagemManutenance_txt.IsEnabled = true;
        }

        private void DesativarCamposOptions()
        {
            manutencao_txt.IsEnabled = false;
            suspensao_txt.IsEnabled = false;
            DataHora_sp.IsEnabled = false;
            mensagemManutenance_txt.IsEnabled = false;
        }

        private void AtivarOpcoes_Checked(object sender, RoutedEventArgs e)
        {
            AtivarCamposOptions();
        }

        private void AtivarOpcoes_Unchecked(object sender, RoutedEventArgs e)
        {
            DesativarCamposOptions();
        }

        private void Limpar_bt_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirmar Clearning!", "...", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimparCampos();
            }
        }

        private void Dias_txt_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos verificacoes = new CodigosProntos();

            verificacoes.CancelaNaoNumeros(sender, e);
        }

        private void Gerar_cliente_bt_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificarCamposVazios())
            {
                GuardarValoresDosCampos();

                ClienteClass clienteClass = new ClienteClass();
                ConfirmarSenha conf = new ConfirmarSenha();
                
                if (!clienteClass.Checar(UInt32.Parse(valores[1])))
                {
                    conf.ShowDialog();

                    if (conf.Retornado)
                    {
                        clienteClass.Adicionar(valores);

                        MessageBox.Show("Usuario Adicionado com sucesso!");
                        LimparCampos();
                    }
                }
                else
                {
                    MessageBox.Show("Ops! ouve um erro, parece que o codigo gerado para esse cliente ja existe!\nClique no botao novamente!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void Contato_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos codigos = new CodigosProntos();
            codigos.CancelaNaoNumeros(sender, e);
        }
    }
}
