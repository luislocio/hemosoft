using HemoSoft.DAL;
using HemoSoft.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirDoador.xaml
    /// </summary>
    public partial class ExibirDoador : UserControl
    {
        Doador doador;

        public ExibirDoador(Doador d)
        {
            InitializeComponent();
            this.doador = d;
            CarregarDoador();
        }

        private void CarregarDoador()
        {
            textNome.Text = doador.NomeCompleto;
            textCpf.Text = doador.Cpf;
            textEstadoCivil.Text = doador.EstadoCivil.ToString();
            textGenero.Text = doador.Genero.ToString();
            textTipoSanguineo.Text = GetStatusTipoSaguineo();
            
            dataGridDoacao.ItemsSource = doador.Doacoes;
        }

        private String GetStatusTipoSaguineo()
        {
            if (doador.TipoSanguineo != null &&
                doador.FatorRh != null)
            {
                return doador.TipoSanguineo + " " + doador.FatorRh;
            }

            return "Aguardando resultados";
        }

        #region Eventos de cliques
        private void DataGridDoacao_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Doacao doacaoSelecionada = dataGridDoacao.SelectedItem as Doacao;

            if (doacaoSelecionada != null)
            {
                MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilDoacao(DoacaoDAO.BuscarDoacaoPorId(doacaoSelecionada));
            }
        }

        private void ButtonCadastrar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
            janelaPrincipal.RenderizarCadastroDoacao(doador);
        } 
        #endregion
    }
}
