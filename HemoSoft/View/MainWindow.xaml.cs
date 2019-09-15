using HemoSoft.DAL;
using HemoSoft.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // TODO: Implementar o atributo triador para janela principal e receber via parametro
        Triador triador = TriadorDAO.BuscarTriadorPorMatricula(new Triador { Matricula = "triador1" });

        private static UserControl usc;

        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            // InicializarBancoDeDados();
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
            LimparPagina();
            
            Doador doador = DoadorDAO.BuscarDoadorPorCpf(new Doador { Cpf = "012345678901" });

            RenderizarPaginasPrincipais(sender, triador, doador);
        }

        // Criar uma variavel para armazenar o usuário (Triador/Solicitante)
        private void RenderizarPaginasPrincipais(object sender, Triador triador, Doador doador)
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
                case "BuscarDoacoes":
                    usc = new BuscarDoacoes();
                    GridPage.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        public void LimparPagina()
        {
            // TODO: MenuLateral.SelectedItems.Clear();
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

        public void RenderizarCadastroExame (Doacao doacao)
        {
            LimparPagina();
            usc = new CadastrarExame(doacao);
            GridPage.Children.Add(usc);
        }

        public void RenderizarListaDoacoes(List<Doacao> doacoes)
        {
            LimparPagina();
            usc = new ExibirListaDoacoes(doacoes);
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
                EstadoCivil = EstadoCivil.Separadx,
                TipoSanguineo = TipoSanguineo.A,
                FatorRh = FatorRh.Negativo,
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
                HepatiteB = false,
                HepatiteC = false,
                Hiv = false,
                StatusTriagem = StatusTriagem.Aprovado
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

            Solicitante solicitanteInit = new Solicitante
            {
                Cnpj = "01234567891234",
                RazaoSocial = "Clinica 1",
                Responsavel = "Seu Zé",
                Senha = "senhaclinica1",
                StatusUsuario = StatusUsuario.Ativo
            };

            TriadorDAO.CadastrarTriador(triadorInit);
            DoadorDAO.CadastrarDoador(doadorInit);
            DoacaoDAO.CadastrarDoacao(doacaoInit);
        }
    }
}
