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
using System.Windows.Shapes;

namespace LogikozSystem.LoginSystem
{
    /// <summary>
    /// Lógica interna para NovaSenha.xaml
    /// </summary>
    public partial class NovaSenha : Window
    {
        public NovaSenha()
        {
            InitializeComponent();
        }

        public static string codigo = string.Empty;
        public static string email = string.Empty;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //aplicando nomes na tela
            NomePrograma_txt.Text = PegarInfos.Nome;
            SobreNomePrograma_txt.Text = PegarInfos.SobreNome;
        }

        private async void Confirm_bt_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Senha_txt.Password) || string.IsNullOrEmpty(ConfirmarSenha_txt.Password))
            {
                MessageBox.Show("Preencha ambos os campos!", "Hmmm", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if(Senha_txt.Password == ConfirmarSenha_txt.Password)
            {
                RecuperarSenha re = new RecuperarSenha();
                ExecutarComandos exe = new ExecutarComandos();
                if(await re.Codigo("http://" + PegarInfos.Url + "/renovarsenha.php?cod=" + codigo + "&email=" + email + "&newpwd=" + 
                    exe.CriptografarSenha_SHA256(exe.CriptografarSenha_MD5(Senha_txt.Password))))
                {
                    MessageBox.Show("Senha alterada com Sucesso!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("As senhas nao coincidem!", "Opps!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //codigo = string.Empty;
            //email = string.Empty;
        }
    }
}
