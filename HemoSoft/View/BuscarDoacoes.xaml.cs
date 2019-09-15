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
    /// Interaction logic for BuscarDoacoes.xaml
    /// </summary>
    public partial class BuscarDoacoes : UserControl
    {
        public BuscarDoacoes()
        {
            InitializeComponent();
        }

        private void ButtonBuscarStatus_Click(object sender, RoutedEventArgs e)
        {
            StatusDoacao statusBusca = (StatusDoacao)Enum.Parse(typeof(StatusDoacao), boxStatus.Text);
            Doacao doacaoBusca = new Doacao { StatusDoacao = statusBusca };

            MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
            janelaPrincipal.RenderizarListaDoacoes(DoacaoDAO.BuscarDoacaoPorStatus(doacaoBusca));
        }
    }
}
