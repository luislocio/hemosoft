using HemoSoft.DAL;
using HemoSoft.Model;
using HemoSoft.Model.Enum;
using HemoSoft.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirPerfilDoador.xaml
    /// </summary>
    public partial class ExibirPerfilDoador : UserControl
    {
        Doador doador;

        public ExibirPerfilDoador(Doador d)
        {
            InitializeComponent();
            this.doador = d;
            CarregarDoador();
        }

        private void CarregarDoador()
        {
            textNome.Text = doador.NomeCompleto;
            textTipoSanguineo.Text = GetStatusTipoSaguineo();
            textCpf.Text = doador.Cpf;
            boxGenero.SelectedItem = doador.Genero;
            boxEstadoCivil.SelectedItem = doador.EstadoCivil;
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

        private void ButtonCadastrarDoacao_Click(object sender, RoutedEventArgs e)
        {
            MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
            janelaPrincipal.RenderizarCadastroDoacao(doador);
        }

        private void ButtonEditarDoador_Click(object sender, RoutedEventArgs e)
        {
            // Habilitar edição dos campos do formulário.
            textCpf.IsReadOnly = false;
            textNome.IsReadOnly = false;
            boxEstadoCivil.IsEnabled = true;
            boxGenero.IsEnabled = true;
            buttonCadastrarDoacao.IsEnabled = false;

            // Transformar botão EDITAR em SALVAR.
            buttonEditarDoador.Content = "Salvar Cadastro";
            buttonEditarDoador.Click -= new RoutedEventHandler(ButtonEditarDoador_Click);
            buttonEditarDoador.Click += new RoutedEventHandler(ButtonSalvarDoador_Click);
        }

        private void ButtonSalvarDoador_Click(object sender, RoutedEventArgs e)
        {
            if (FormularioEstaCompleto())
            {
                if (Validacao.CnpjEhValido(textCpf.Text))
                {
                    doador.Cpf = textCpf.Text;
                    doador.NomeCompleto = textNome.Text;
                    doador.Genero = (Genero)Enum.Parse(typeof(Genero), boxGenero.Text);
                    doador.EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), boxEstadoCivil.Text);

                    DoadorDAO.AlterarDoador(doador);

                    // Habilitar edição dos campos do formulário.
                    textCpf.IsReadOnly = true;
                    textNome.IsReadOnly = true;
                    boxEstadoCivil.IsEnabled = false;
                    boxGenero.IsEnabled = false;
                    buttonCadastrarDoacao.IsEnabled = true;

                    // Transformar botão EDITAR em SALVAR.
                    buttonEditarDoador.Content = "Editar Cadastro";
                    buttonEditarDoador.Click -= new RoutedEventHandler(ButtonSalvarDoador_Click);
                    buttonEditarDoador.Click += new RoutedEventHandler(ButtonEditarDoador_Click);
                }
                else
                {
                    MessageBox.Show("CPF inválido");
                }
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
        }
        #endregion

        private bool FormularioEstaCompleto()
        {
            return
                !textNome.Text.Equals("") ||
                !textCpf.Text.Equals("") ||
                !boxEstadoCivil.SelectionBoxItem.Equals("") ||
                !boxGenero.SelectionBoxItem.Equals("");
        }
    }
}