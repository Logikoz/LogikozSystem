using LogikozSystem._Dep;
using LogikozSystem.Licencas;
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
    /// Lógica interna para ConfirmarSenha.xaml
    /// </summary>
    public partial class ConfirmarSenha : Window
    {
        public ConfirmarSenha()
        {
            InitializeComponent();
        }

        LoginClass login = new LoginClass();

        public Boolean Retornado;
        

        private void Fechar_janela_confirmLogin_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = TelaPrincipal.largura_jan;
            this.Height = TelaPrincipal.altura_jan;

            Usuario_txt.Text = TelaPrincipal.infoLogin[1];

            //aplicando nomes na tela
            NomePrograma_txt.Text = PegarInfos.Nome;
            SobreNomePrograma_txt.Text = PegarInfos.SobreNome;
        }

        Byte i = 0;

        private void Confirm_bt_Click(object sender, RoutedEventArgs e)
        {
            
            if (login.Verificar(Usuario_txt.Text, Senha_txt.Password))
            {
                //retorna o valor
                Retornado = true;

                //fechar a janela!
                this.Close();
            }
            else
            {
                if (Senha_txt.Password != string.Empty)
                {
                    Senha_txt.Clear();
                    i++;
                    MessageBox.Show("Senha incorreta " + i + "/3", "Suspeito", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (i == 3)
                    {
                        App.Current.Shutdown();
                    }
                }

                //retorna o valor
                Retornado = false;
            }

        }
    }
}
