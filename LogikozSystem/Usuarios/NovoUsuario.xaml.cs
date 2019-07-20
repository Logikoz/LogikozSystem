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

namespace LogikozSystem.Usuarios
{
    /// <summary>
    /// Interação lógica para NovoUsuario.xam
    /// </summary>
    public partial class NovoUsuario : UserControl
    {
        public NovoUsuario()
        {
            InitializeComponent();
        }

        private void SelecionarImagem_img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CargoUsuario_txt.IsVisible)
            {
                MessageBox.Show("foi");
            }
        }
    }
}
