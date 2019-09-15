using HemoSoft.DAL;
using HemoSoft.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // TODO: Implementar o atributo triador para janela principal
        Triador triador = TriadorDAO.BuscarTriadorPorMatricula(new Triador { Matricula = "triador1" });

        private static UserControl usc;

        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // InicializarBancoDeDados();

            LimparPagina();

            
            Doador doador = DoadorDAO.BuscarDoadorPorCpf(new Doador { Cpf = "012345678901" });

            RenderizarMenu(sender, triador, doador);
        }

        // Criar uma variavel para armazenar o usuário (Triador/Solicitante)
        private void RenderizarMenu(object sender, Triador triador, Doador doador)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "CadastrarDoador":
                    usc = new CadastrarDoador();
                    GridPage.Children.Add(usc);
                    break;
                case "BuscarDoador":
                    usc = new BuscarDoador();
                    GridPage.Children.Add(usc);
                    break;
                case "CadastrarExame":
                    //TODO: remover hard code
                    Doacao doacao = DoacaoDAO.BuscarDoacaoPorId(new Doacao { IdDoacao = 3 });
                    usc = new CadastrarExame(doacao);
                    GridPage.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        public void LimparPagina()
        {
            usc = null;
            GridPage.Children.Clear();
        }

        public void RenderizarPerfilDoador(Doador doador)
        {
            LimparPagina();
            usc = new ExibirDoador(DoadorDAO.BuscarDoadorPorCpf(doador));
            GridPage.Children.Add(usc);
        }

        public void RenderizarPerfilDoacao(Doacao doacao)
        {
            LimparPagina();
            usc = new ExibirDoacao(DoacaoDAO.BuscarDoacaoPorId(doacao));
            GridPage.Children.Add(usc);
        }

        public void RenderizarCadastroDoacao(Doador doador)
        {
            LimparPagina();
            usc = new CadastrarDoacao(doador, triador);
            GridPage.Children.Add(usc);
        }


        private static void InicializarBancoDeDados()
        {
            Triador triadorInit = new Triador
            {
                NomeCompleto = "triador1",
                Matricula = "triador1",
                Senha = "senhatriador1",
                StatusUsuario = StatusUsuario.Ativo,
            };

            Doador doadorInit = new Doador
            {
                NomeCompleto = "doador1",
                Cpf = "012345678901",
                Genero = Genero.Feminino,
                EstadoCivil = EstadoCivil.Separadx
            };

            TriagemClinica triagemClinicaInit = new TriagemClinica
            {
                Peso = 80,
                Pulso = 80,
                Temperatura = 60,
                StatusTriagem = StatusTriagem.Aprovado
            };

            TriagemLaboratorial triagemLaboratorialInit = new TriagemLaboratorial
            {
                FatorRh = FatorRh.Negativo,
                HepatiteB = false,
                HepatiteC = false,
                Hiv = false,
                StatusTriagem = StatusTriagem.Aprovado,
                TipoSanguineo = TipoSanguineo.A,
            };

            ImpedimentosDefinitivos impedimentosDefinitivosInit = new ImpedimentosDefinitivos
            {
                AntecedenteAvc = false,
                HepatiteB = false,
                HepatiteC = false,
                Hiv = false,
            };

            ImpedimentosTemporarios impedimentosTemporariosInit = new ImpedimentosTemporarios
            {
                BebidaAlcoolica = false,
                BebidaAlcoolicaUltimaVez = 0,
                Gravidez = Gravidez.Nenhuma,
                GravidezUltimaVez = 0,
                Gripe = false,
                GripeUltimaVez = 0,
                Tatuagem = false,
                TatuagemUltimaVez = 0
            };

            Doacao doacaoInit = new Doacao
            {
                DataDoacao = DateTime.Now,
                StatusDoacao = StatusDoacao.Disponivel,
                Doador = doadorInit,
                Triador = triadorInit,
                TriagemClinica = triagemClinicaInit,
                TriagemLaboratorial = triagemLaboratorialInit,
                ImpedimentosTemporarios = impedimentosTemporariosInit,
                ImpedimentosDefinitivos = impedimentosDefinitivosInit
            };

            TriadorDAO.CadastrarTriador(triadorInit);
            DoadorDAO.CadastrarDoador(doadorInit);
            DoacaoDAO.CadastrarDoacao(doacaoInit);
        }

    }
}
