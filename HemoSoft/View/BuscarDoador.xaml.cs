using HemoSoft.DAL;
using HemoSoft.Model;
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

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for BuscarDoador.xaml
    /// </summary>
    public partial class BuscarDoador : UserControl
    {
        public BuscarDoador()
        {
            InitializeComponent();
        }

        private void ButtonBuscar_Click(object sender, RoutedEventArgs e)
        {
            Doador doadorBusca = new Doador { Cpf = textCpf.Text };
            Doador doadorResultado = DoadorDAO.BuscarDoadorPorCpf(doadorBusca);

            if (doadorResultado == null)
            {
                MessageBox.Show("Doador inexistente.");
            }
            else
            {
                MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
                Grid GridPage = (Grid)parentWindow.FindName("GridPage");
                GridPage.Children.Clear();
                UserControl usc = new ExibirDoador(doadorResultado);
                GridPage.Children.Add(usc);
            }
        }
    }
}
