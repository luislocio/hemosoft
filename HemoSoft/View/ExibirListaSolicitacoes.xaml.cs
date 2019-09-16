using HemoSoft.Model;
using System.Collections.Generic;
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
            dataGridDoacoes.ItemsSource = solicitacoes;
        }
    }
}
