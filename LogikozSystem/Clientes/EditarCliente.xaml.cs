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
    /// Interação lógica para EditarCliente.xam
    /// </summary>
    public partial class EditarCliente : UserControl
    {
        public EditarCliente()
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
            CodCliente_txt.Clear();
        }

        private void AlocarCampos()
        {
            cliente_txt.Text = valores[2];
            licenca_txt.Text = valores[3];
            dias_txt.Text = valores[4];
            email_txt.Text = valores[13];
            Contato.Text = valores[14];
        }

        string[] valores = new string[15];

        private void GuardarValoresDosCampos()
        {
            CodigosProntos prontos = new CodigosProntos();
            
            valores[2] = cliente_txt.Text;
            valores[3] = licenca_txt.Text;
            valores[4] = dias_txt.Text;
            valores[5] = prontos.PegarDataAtual();
            


            valores[13] = email_txt.Text;
            valores[14] = Contato.Text;

            ClienteClass cClass = new ClienteClass();
            ConfirmarSenha confirmar = new ConfirmarSenha();
            confirmar.ShowDialog();

            if (confirmar.Retornado)
            {
                cClass.AtualizarDados(valores);

                MessageBox.Show("Dados Atualizados com sucesso!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ProcurarCliente_bt_Click(object sender, RoutedEventArgs e)
        {
            ClienteClass verificar = new ClienteClass();
            CodigosProntos prontos = new CodigosProntos();

            if (CodCliente_txt.Text != string.Empty && !prontos.PossuiLetras(CodCliente_txt.Text))
            {
                if (verificar.Checar(UInt32.Parse(CodCliente_txt.Text)))
                {
                    ClienteClass @class = new ClienteClass();
                    valores = @class.PegarDados(UInt32.Parse(CodCliente_txt.Text));

                    AlocarCampos();
                }
                else
                {
                    MessageBox.Show("O codigo informado nao coincide com nenhum cliente cadastrado!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }
        }

        private void Dias_txt_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos verificacoes = new CodigosProntos();

            verificacoes.CancelaNaoNumeros(sender, e);
        }

        private void CodCliente_txt_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos verificacoes = new CodigosProntos();

            verificacoes.CancelaNaoNumeros(sender, e);
        }

        private void AtualizarCliente_bt_Click(object sender, RoutedEventArgs e)
        {
            if (cliente_txt.Text == string.Empty || licenca_txt.Text == string.Empty || dias_txt.Text == string.Empty || email_txt.Text == string.Empty || Contato.Text == string.Empty)
            {
                MessageBox.Show("Voce precisa preencher todos os campos!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (MessageBox.Show("Voce tem certeza que quer atualizar os dados deste cliente?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                GuardarValoresDosCampos();
            }
        }

        private void Limpar_bt_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirmar Clearning!", "...", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimparCampos();
            }
        }

        private void Contato_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos codigos = new CodigosProntos();
            codigos.CancelaNaoNumeros(sender, e);
        }
    }
}
