using HemoSoft.DAL;
using HemoSoft.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirListaSolicitacoes.xaml
    /// </summary>
    public partial class ExibirListaSolicitacoes : UserControl
    {
        public ExibirListaSolicitacoes(List<Solicitacao> solicitacoes)
        {
            InitializeComponent();
            dataGridSolicitacoes.ItemsSource = solicitacoes;
        }

        private void DataGridSolicitacao_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Solicitacao solicitacaoSelecionada = dataGridSolicitacoes.SelectedItem as Solicitacao;

            if (solicitacaoSelecionada != null)
            {
                MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilSolicitacao(SolicitacaoDAO.BuscarSolicitacaoPorId(solicitacaoSelecionada));
            }
        }
    }
}
