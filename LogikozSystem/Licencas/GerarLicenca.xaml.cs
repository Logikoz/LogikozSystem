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
using System.Windows.Shapes;

namespace LogikozSystem.Licencas
{
    /// <summary>
    /// Lógica interna para GerarLicenca.xaml
    /// </summary>
    public partial class GerarLicenca : Window
    {
        public GerarLicenca()
        {
            InitializeComponent();
        }

        private void Fechar_janela_bt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GerarNovaLicenca_bt_Click(object sender, RoutedEventArgs e)
        {
            CaixaLicenca_txt.Text = GerarAleatorio(5);
        }

        public string GerarAleatorio(Byte tamanho)
        {
            string pKeys = "abcdefghtu!vwxyz012345NOPQRSTUVWX@YZ6789ijklmnopqrsABCDE$FGHIJKLM";

            Random ale = new Random();

            string[] a = new string[5];

            for (byte i = 0; i < 5; i++)
            {
                a[i] = new string(Enumerable.Repeat(pKeys, tamanho).Select(keyNumber => keyNumber[ale.Next(keyNumber.Length)]).ToArray());
            }

            return $"{a[0]}-{a[1]}-{a[2]}-{a[3]}-{a[4]}";
        }

        private void SalvarLicenca_bt_Click(object sender, RoutedEventArgs e)
        {
            LicencaClass criar = new LicencaClass();

            if (!criar.Verificar(CaixaLicenca_txt.Text))
            {
                if (MessageBox.Show("Realmente quer adicionar essa serial?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    criar.Adicionar(CaixaLicenca_txt.Text);
                    MessageBox.Show(CaixaLicenca_txt.Text + "\nFoi adicionada com sucesso!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show(CaixaLicenca_txt.Text+ "\nJa foi adicionada. Por favor, cria outro serial e tente novamente!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(CaixaLicenca_txt.Text != string.Empty)
            {
                //copia texto da caixa!
                Clipboard.SetText(CaixaLicenca_txt.Text);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
