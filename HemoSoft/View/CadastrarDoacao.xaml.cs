using HemoSoft.DAL;
using HemoSoft.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for CadastrarDoacao.xaml
    /// </summary>
    public partial class CadastrarDoacao : UserControl
    {
        Doador doador;
        Triador triador;
        Boolean? bebida;
        Boolean? gripe;
        Boolean? tatuagem;
        Gravidez? gravidez;


        public CadastrarDoacao(Triador t, Doador d)
        {
            InitializeComponent();
            this.doador = d;
            this.triador = t;

        }
        private void RadioButtonBebidaNao_Click(object sender, RoutedEventArgs e)
        {
            textBebida.IsEnabled = false;
            this.bebida = false;
        }

        private void RadioButtonBebidaSim_Click(object sender, RoutedEventArgs e)
        {
            textBebida.IsEnabled = true;
            this.bebida = true;
        }

        private void RadioButtonGravidezNao_Click(object sender, RoutedEventArgs e)
        {
            textGravidez.IsEnabled = false;
            this.gravidez = Gravidez.Nenhuma;
        }

        private void RadioButtonGravidezNormal_Click(object sender, RoutedEventArgs e)
        {
            textGravidez.IsEnabled = true;
            this.gravidez = Gravidez.PartoNormal;
        }

        private void RadioButtonGravidezCesarea_Click(object sender, RoutedEventArgs e)
        {
            textGravidez.IsEnabled = true;
            this.gravidez = Gravidez.Cesarea;
        }

        private void RadioButtonGripeNao_Click(object sender, RoutedEventArgs e)
        {
            textGripe.IsEnabled = false;
            this.gripe = false;
        }

        private void RadioButtonGripeSim_Click(object sender, RoutedEventArgs e)
        {
            textGripe.IsEnabled = true;
            this.gripe = true;
        }

        private void RadioButtonTatuagemNao_Click(object sender, RoutedEventArgs e)
        {
            textTatuagem.IsEnabled = false;
            this.tatuagem = false;
        }

        private void RadioButtonTatuagemSim_Click(object sender, RoutedEventArgs e)
        {
            textTatuagem.IsEnabled = true;
            this.tatuagem = true;

        }

        private void ButtonCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (IsTriagemClinicaComplete() && IsImpetimentosTemporariosComplete())
            {
                // Informações do formulário.
                ImpedimentosTemporarios impedimentosTemporarios = CreateImpetimentosTemporarios();
                TriagemClinica triagemClinica = CreateTriagemClinica();

                // Informações que serão preenchidas após recebimento do exame laboratorial.
                ImpedimentosDefinitivos impedimentosDefinitivos = new ImpedimentosDefinitivos { };
                TriagemLaboratorial triagemLaboratorial = new TriagemLaboratorial { };

                Doacao doacao = new Doacao
                {
                    DataDoacao = DateTime.Now,
                    StatusDoacao = StatusDoacao.AguardandoAtendimento,
                    Doador = this.doador,
                    Triador = this.triador,
                    TriagemClinica = triagemClinica,
                    TriagemLaboratorial = triagemLaboratorial,
                    ImpedimentosTemporarios = impedimentosTemporarios,
                    ImpedimentosDefinitivos = impedimentosDefinitivos
                };

                DoacaoDAO.CadastrarDoacao(doacao);

                MessageBox.Show("Doação cadastrada com sucesso");

                var janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.CarregarPerfilDoador(DoadorDAO.BuscarDoadorPorCpf(doador));
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
        }



        private bool IsImpetimentosTemporariosComplete()
        {
            if (this.bebida == null || this.gravidez == null || this.gripe == null || this.tatuagem == null)
            {
                return false;
            }
            else
            {
                if ((textBebida.IsEnabled && textBebida.Text.Equals(""))
                    || (textGravidez.IsEnabled && textGravidez.Text.Equals(""))
                    || (textGripe.IsEnabled && textGripe.Text.Equals(""))
                    || (textTatuagem.IsEnabled && textTatuagem.Text.Equals("")))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsTriagemClinicaComplete()
        {
            if (textPeso.Text.Equals("") || textPulso.Text.Equals("") || textTemperatura.Text.Equals(""))
            {
                return false;
            }

            return true;
        }

        private static ImpedimentosTemporarios CreateImpetimentosTemporarios()
        {
            return new ImpedimentosTemporarios
            {
                BebidaAlcoolica = false,
                BebidaAlcoolicaUltimaVez = 0,
                Gravidez = Gravidez.PartoNormal,
                GravidezUltimaVez = 0,
                Gripe = false,
                GripeUltimaVez = 0,
                Tatuagem = false,
                TatuagemUltimaVez = 0
            };
        }

        private TriagemClinica CreateTriagemClinica()
        {
            return new TriagemClinica
            {
                Peso = double.Parse(textPeso.Text),
                Pulso = int.Parse(textPulso.Text),
                Temperatura = int.Parse(textTemperatura.Text),
            };
        }
    }
}
