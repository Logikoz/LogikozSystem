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

namespace LogikozSystem.Pessoal
{
    /// <summary>
    /// Lógica interna para ExibirImagem.xaml
    /// </summary>
    public partial class ExibirImagem : Window
    {
        public ExibirImagem()
        {
            InitializeComponent();
        }

        private void FecharFotoPerfil_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginClass login = new LoginClass();
            string[] informacoes = login.PegarDados(TelaPrincipal.infoLogin[1], string.Empty);

            NomeUsuario_txt.Text = informacoes[4] + " ";
            LoginUsuario_txt.Text = "(" + informacoes[2] + ")";
            Imagem_img.Source = login.Converter(login.PegarImagem(TelaPrincipal.infoLogin[1]));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
