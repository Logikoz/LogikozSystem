using API_Dependences;
using LogikozSystem._Dep;
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
    /// Interação lógica para PegarSenhaConfig.xam
    /// </summary>
    public partial class PegarSenhaConfig : UserControl
    {
        public PegarSenhaConfig()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            SenhaUsuarioConfig_txt.Clear();
            SenhaUsuarioConfig_txt.IsEnabled = false;
            GerarNovaSenha_bt.IsEnabled = false;
            CodCliente_txt.Clear();
        }

        private void IniciarCampos()
        {
            SenhaUsuarioConfig_txt.IsEnabled = true;
            GerarNovaSenha_bt.IsEnabled = true;
        }

        private void CodCliente_txt_KeyDown(object sender, KeyEventArgs e)
        {
            CodigosProntos prontos = new CodigosProntos();
            prontos.CancelaNaoNumeros(sender, e);
        }

        private void ProcurarCliente_bt_Click(object sender, RoutedEventArgs e)
        {
            ClienteClass cliente = new ClienteClass();
            CodigosProntos codigos = new CodigosProntos();
            if (!string.IsNullOrEmpty(CodCliente_txt.Text) && !codigos.PossuiLetras(CodCliente_txt.Text))
            {
                if (cliente.Checar(Convert.ToUInt32(CodCliente_txt.Text)))
                {
                    IniciarCampos();
                    //SenhaUsuarioConfig_txt.Text = cliente.PegarSenhaConfig(Convert.ToUInt32(CodCliente_txt.Text));
                }
                else
                {
                    MessageBox.Show("Usuario informado nao existe\nVerifique se digitou corretamente e tente novamente!", "Ops!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    LimparCampos();
                    return;
                }
            }
            else
            {
                LimparCampos();
            }
        }

        private void GerarNovaSenha_bt_Click(object sender, RoutedEventArgs e)
        {
            ClienteClass cliente = new ClienteClass();
            SenhaUsuarioConfig_txt.Text = cliente.AtualizarSenhaConfig(UInt32.Parse(CodCliente_txt.Text));
        }
    }
}
