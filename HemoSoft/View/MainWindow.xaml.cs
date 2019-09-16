﻿using HemoSoft.DAL;
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
        private static Usuario usuario = SingletonUsuario.GetInstance();
        private static UserControl usc;

        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));

            // TODO: REMOVER HARDCODED
            // usuario = new Usuario { IdUsuario = 1, NomeDeUsuario = "Teste", TipoUsuario = TipoUsuario.Solicitante };
            usuario = new Usuario { IdUsuario = 1, NomeDeUsuario = "Teste", TipoUsuario = TipoUsuario.Triador };

            RenderizarMenuLateral();
            // InicializarBancoDeDados();
        }

        #region Eventos de cliques
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

            RenderizarPaginasPrincipais(sender);
        }
        #endregion

        #region Renderização das telas principais
        private void RenderizarMenuLateral()
        {
            if (usuario.TipoUsuario == TipoUsuario.Solicitante)
            {
                MenuLateral.Items.Remove(CadastrarDoador);
                MenuLateral.Items.Remove(BuscarDoador);
                MenuLateral.Items.Remove(CadastrarExame);
            }
        }

        private void RenderizarPaginasPrincipais(object sender)
        {
            if (MenuLateral.SelectedItems.Count > 0)
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
        }

        public void LimparPagina()
        {
            usc = null;
            GridPage.Children.Clear();
        }
        #endregion

        #region Renderização de telas auxiliares
        public void RenderizarPerfilDoador(Doador doador)
        {
            LimparPagina();
            MenuLateral.SelectedItems.Clear();

            usc = new ExibirDoador(DoadorDAO.BuscarDoadorPorCpf(doador));
            GridPage.Children.Add(usc);
        }

        public void RenderizarPerfilDoacao(Doacao doacao)
        {
            LimparPagina();
            MenuLateral.SelectedItems.Clear();

            usc = new ExibirDoacao(DoacaoDAO.BuscarDoacaoPorId(doacao));
            GridPage.Children.Add(usc);
        }

        public void RenderizarCadastroDoacao(Doador doador)
        {
            Triador triador = TriadorDAO.BuscarTriadorPorId(new Triador { IdTriador = usuario.IdUsuario });

            LimparPagina();
            MenuLateral.SelectedItems.Clear();

            usc = new CadastrarDoacao(doador, triador);
            GridPage.Children.Add(usc);
        }

        public void RenderizarCadastroExame(Doacao doacao)
        {
            LimparPagina();
            MenuLateral.SelectedItems.Clear();

            usc = new CadastrarExame(doacao);
            GridPage.Children.Add(usc);
        }

        public void RenderizarListaDoacoes(List<Doacao> doacoes)
        {
            LimparPagina();
            MenuLateral.SelectedItems.Clear();

            usc = new ExibirListaDoacoes(doacoes);
            GridPage.Children.Add(usc);
        }
        #endregion

        private static void InicializarBancoDeDados()
        {
            Triador triadorInit = new Triador
            {
                NomeCompleto = "triador1",
                Matricula = "1234567",
                Senha = "senhatriador",
                StatusUsuario = StatusUsuario.Ativo,
            };

            Doador doadorInit = new Doador
            {
                NomeCompleto = "doador1",
                Cpf = "12345678901",
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
                Cnpj = "12345678901234",
                RazaoSocial = "Clinica 1",
                Responsavel = "Responsavel 1",
                Senha = "senhasolicitante",
                StatusUsuario = StatusUsuario.Ativo
            };

            TriadorDAO.CadastrarTriador(triadorInit);
            SolicitanteDAO.CadastrarSolicitante(solicitanteInit);

            DoadorDAO.CadastrarDoador(doadorInit);
            DoacaoDAO.CadastrarDoacao(doacaoInit);
        }
    }
}
