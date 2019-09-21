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
    /// Interaction logic for ExibirListaSolicitantes.xaml
    /// </summary>
    public partial class ExibirListaSolicitantes : UserControl
    {
        public ExibirListaSolicitantes(List<Solicitante> t)
        {
            InitializeComponent();
            dataGridSolicitantes.ItemsSource = t;
        }

        private void DataGridSolicitantes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Solicitante solicitanteSelecionado = dataGridSolicitantes.SelectedItem as Solicitante;

            if (solicitanteSelecionado != null)
            {
                MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilSolicitante(SolicitanteDAO.BuscarSolicitantePorId(solicitanteSelecionado));
            }
        }
    }
}
