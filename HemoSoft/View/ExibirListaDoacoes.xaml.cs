using HemoSoft.DAL;
using HemoSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirListaDoacoes.xaml
    /// </summary>
    public partial class ExibirListaDoacoes : UserControl
    {
        public ExibirListaDoacoes(List<Doacao> d)
        {
            InitializeComponent();
            dataGridDoacoes.ItemsSource = d;
        }

        private void ButtonSolicitar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<Doacao> doacoesSelecionadas = dataGridDoacoes.SelectedItems.Cast<Doacao>().ToList();

            if (doacoesSelecionadas != null)
            {
                //TODO Implementar solicitante
                Solicitante solicitante = SolicitanteDAO.BuscarSolicitantePorCnpj(new Solicitante { Cnpj = "01234567891234" });

                Solicitacao solicitacao = new Solicitacao
                {
                    DataSolicitacao = DateTime.Now,
                    Solicitante = solicitante,
                    Doacoes = new List<Doacao>()
                };

                foreach (Doacao doacao in doacoesSelecionadas)
                {
                    solicitacao.Doacoes.Add(doacao);
                    doacao.StatusDoacao = StatusDoacao.NaoDisponivel;
                }

                SolicitacaoDAO.CadastrarSolicitacao(solicitacao);

                MessageBox.Show("Solicitação efetuada com sucesso.");
            }
            else
            {
                MessageBox.Show("Favor solicitar pelo menos uma doação.");
            }
            ;
        }
    }
}
