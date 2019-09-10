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
        public MainWindow()
        {
            InitializeComponent();
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
            UserControl usc = null;
            GridPage.Children.Clear();

            //Triador triador = new Triador
            //{
            //    NomeCompleto = "triador1",
            //    Matricula = "triador1",
            //    Senha = "senhatriador1",
            //    StatusUsuario = StatusUsuario.Ativo,
            //};

            //Doador doador = new Doador
            //{
            //    NomeCompleto = "doador1",
            //    Cpf = "012345678901",
            //    Genero = Genero.Feminino,
            //    EstadoCivil = EstadoCivil.Separadx
            //};

            //TriagemClinica triagemClinica = new TriagemClinica
            //{
            //    Peso = 80,
            //    Pulso = 80,
            //    Temperatura = 60,
            //    StatusTriagem = StatusTriagem.Aprovado
            //};

            //TriagemLaboratorial triagemLaboratorial = new TriagemLaboratorial
            //{
            //    FatorRh = FatorRh.Negativo,
            //    HepatiteB = false,
            //    HepatiteC = false,
            //    Hiv = false,
            //    StatusTriagem = StatusTriagem.Aprovado,
            //    TipoSanguineo = TipoSanguineo.A,
            //};

            //ImpedimentosDefinitivos impedimentosDefinitivos = new ImpedimentosDefinitivos
            //{
            //    AntecedenteAvc = false,
            //    HepatiteB = false,
            //    HepatiteC = false,
            //    Hiv = false,
            //};

            //ImpedimentosTemporarios impedimentosTemporarios = new ImpedimentosTemporarios
            //{
            //    BebidaAlcoolica = false,
            //    BebidaAlcoolicaUltimaVez = 0,
            //    Gravidez = false,
            //    GravidezUltimaVez = 0,
            //    Gripe = false,
            //    GripeUltimaVez = 0,
            //    Tatuagem = false,
            //    TatuagemUltimaVez = 0
            //};

            //Doacao doacao = new Doacao
            //{
            //    DataDoacao = DateTime.Now,
            //    StatusDoacao = StatusDoacao.Disponivel,
            //    Doador = doador,
            //    Triador = triador,
            //    TriagemClinica = triagemClinica,
            //    TriagemLaboratorial = triagemLaboratorial,
            //    ImpedimentosTemporarios = impedimentosTemporarios,
            //    ImpedimentosDefinitivos = impedimentosDefinitivos
            //};

            //TriadorDAO.CadastrarTriador(triador);
            //DoadorDAO.CadastrarDoador(doador);
            //DoacaoDAO.CadastrarDoacao(doacao);


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
                case "CadastrarDoacao":
                    usc = new CadastrarDoacao();
                    GridPage.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
    }
}
