using HemoSoft.DAL;
using HemoSoft.Model;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirPerfilSolicitacao.xaml
    /// </summary>
    public partial class ExibirPerfilSolicitacao : UserControl
    {
        Solicitacao solicitacao;
        public ExibirPerfilSolicitacao(Solicitacao s)
        {
            InitializeComponent();
            this.solicitacao = s;
            CarregarSolicitacao();

        }

        private void DataGridDoacao_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Doacao doacaoSelecionada = dataGridDoacao.SelectedItem as Doacao;

            if (doacaoSelecionada != null)
            {
                MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilDoacao(DoacaoDAO.BuscarDoacaoPorId(doacaoSelecionada));
            }
        }

        private void CarregarSolicitacao()
        {
            textId.Text = "# " + solicitacao.IdSolicitacao;
            textDataSolicitacao.Text = solicitacao.DataSolicitacao.ToString();
            dataGridDoacao.ItemsSource = solicitacao.Doacoes;
        }
    }
}
