using HemoSoft.DAL;
using HemoSoft.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
