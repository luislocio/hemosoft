using HemoSoft.DAL;
using HemoSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirListaDoacoes.xaml
    /// </summary>
    public partial class ExibirListaDoacoes : UserControl
    {
        private static Usuario usuario = SingletonUsuario.GetInstance();


        public ExibirListaDoacoes(List<Doacao> d)
        {
            InitializeComponent();
            dataGridDoacoes.ItemsSource = d;
            ValidarBotoes();
        }

        #region Eventos de cliques
        private void ButtonSolicitar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Lista de itens selecionados na tabela
            List<Doacao> doacoesSelecionadas = dataGridDoacoes.SelectedItems.Cast<Doacao>().ToList();

            if (doacoesSelecionadas.Count > 0)
            {
                Solicitante solicitante = SolicitanteDAO.BuscarSolicitantePorId(new Solicitante { IdSolicitante = usuario.IdUsuario });
                Solicitacao solicitacao = CriarSolicitacao(doacoesSelecionadas, solicitante);
                SolicitacaoDAO.CadastrarSolicitacao(solicitacao);

                MessageBox.Show("Solicitação efetuada com sucesso.");

                dataGridDoacoes.ItemsSource = null;
                dataGridDoacoes.ItemsSource = DoacaoDAO.BuscarDoacaoPorStatus(new Doacao {StatusDoacao = StatusDoacao.Disponivel });
            }
            else
            {
                MessageBox.Show("Favor solicitar pelo menos uma doação.");
            };
        }

        private void DataGridDoacoes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Doacao doacaoSelecionada = dataGridDoacoes.SelectedItem as Doacao;

            if (doacaoSelecionada != null &&
                usuario.TipoUsuario != TipoUsuario.Solicitante)
            {
                MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilDoacao(DoacaoDAO.BuscarDoacaoPorId(doacaoSelecionada));
            }
        }
        #endregion

        private static Solicitacao CriarSolicitacao(List<Doacao> doacoesSelecionadas, Solicitante solicitante)
        {
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

            return solicitacao;
        }

        private void ValidarBotoes()
        {
            if (usuario.TipoUsuario == TipoUsuario.Triador)
            {
                listaDoacoes.Children.Remove(ButtonSolicitar);
            }
        }
    }
}
