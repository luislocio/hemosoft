using HemoSoft.DAL;
using HemoSoft.Model;
using System.Windows;
using System.Windows.Controls;

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

        public  void ButtonBuscar_Click(object sender, RoutedEventArgs e)
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
                UserControl usc = new ExibirPerfilDoador(doadorResultado);
                GridPage.Children.Add(usc);
            }
        }
    }
}
