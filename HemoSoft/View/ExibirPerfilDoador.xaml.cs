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
            ValidarBotoes();
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

        private void ValidarBotoes()
        {
            Doacao ultimaDoacao = DoacaoDAO.BuscarUltimaDoacaoPorDoador(doador);
            if (ultimaDoacao != null)
            {
                string mensagem = "";
                mensagem = ValidarImpedimentosDefinitivos(mensagem);
                mensagem = ValidarImpedimentosTemporarios(mensagem, ultimaDoacao);

                if (!mensagem.Equals(""))
                {
                    MessageBox.Show(mensagem);
                    buttonCadastrarDoacao.IsEnabled = false;
                }
            }
        }

        #region Validação de Impedimentos
        private string ValidarImpedimentosTemporarios(string mensagem, Doacao doacao)
        {
            if (doacao.TriagemLaboratorial.StatusTriagem == StatusTriagem.Reprovado)
            {
                if (doacao.ImpedimentosTemporarios.BebidaAlcoolicaUltimaVez > 0 &&
                    DateTime.Now.Subtract(doacao.DataDoacao).Hours < 7)
                {
                    mensagem += "Bebida alcoolica nas ultimas " + DateTime.Now.Subtract(doacao.DataDoacao).Hours + " horas.\n";
                }

                if (doacao.ImpedimentosTemporarios.GravidezUltimaVez > 0)
                {
                    int limite = GetLimiteGravidez(doacao);

                    if (DateTime.Now.Subtract(doacao.DataDoacao).Days < limite)
                    {
                        mensagem += "Gravidez nos ultimos " + DateTime.Now.Subtract(doacao.DataDoacao).Days + " meses.\n";

                    }
                }

                if (doacao.ImpedimentosTemporarios.GripeUltimaVez > 0 &&
                    DateTime.Now.Subtract(doacao.DataDoacao).Days < 12)
                {
                    mensagem += "Gripe nos ultimos " + DateTime.Now.Subtract(doacao.DataDoacao).Days + " dias.\n";
                }

                if (doacao.ImpedimentosTemporarios.TatuagemUltimaVez > 0 &&
                    DateTime.Now.Subtract(doacao.DataDoacao).Days < 365)
                {
                    mensagem += "Tatuagem no ultimos " + DateTime.Now.Subtract(doacao.DataDoacao).Days / 30 + " meses.\n";
                }
            }
            return mensagem;
        }

        private string ValidarImpedimentosDefinitivos(string mensagem)
        {
            bool possuiImpedimentosDefinitivos = DoacaoDAO.VerificarDoacoesPorStatusTriagemLaboratorial(new TriagemLaboratorial { StatusTriagem = StatusTriagem.Reprovado });
            if (possuiImpedimentosDefinitivos)
            {
                mensagem += "Doador possui impedimentos definitivos.\n";
            }

            return mensagem;
        }
        #endregion

        #region Processamentos de dados
        private String GetStatusTipoSaguineo()
        {
            if (doador.TipoSanguineo != null &&
                doador.FatorRh != null)
            {
                return doador.TipoSanguineo + " " + doador.FatorRh;
            }

            return "Aguardando resultados";
        }

        private static int GetLimiteGravidez(Doacao doacao)
        {
            int limite = 0;
            if (doacao.ImpedimentosTemporarios.Gravidez == Gravidez.PartoNormal)
            {
                limite = 90;
            }
            else
            {
                limite = 180;
            }

            return limite;
        }
        #endregion

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
            Doacao ultimaDoacao = DoacaoDAO.BuscarUltimaDoacaoPorDoador(doador);

            if (ultimaDoacao == null || DateTime.Now.Subtract(ultimaDoacao.DataDoacao).Days > 180)
            {
                MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarCadastroDoacao(doador);
            }
            else
            {
                MessageBox.Show("Doação realizada nos ultimos 3 meses.");
            }
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
                if (Validacao.CpfEhValido(textCpf.Text))
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
                !textNome.Text.Equals("") &&
                !textCpf.Text.Equals("") &&
                !boxEstadoCivil.SelectionBoxItem.Equals("") &&
                !boxGenero.SelectionBoxItem.Equals("");
        }
    }
}