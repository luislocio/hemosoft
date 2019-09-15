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
        Boolean? statusAntecedenteAvc;
        Boolean? statusBebida;
        Boolean? statusGripe;
        Boolean? statusTatuagem;
        Gravidez? statusGravidez;

        public CadastrarDoacao(Doador d, Triador t)
        {
            InitializeComponent();
            this.doador = d;
            this.triador = t;
        }

        #region Radio Buttons
        // Radio buttons - Bebida        
        private void RadioButtonBebidaNao_Click(object sender, RoutedEventArgs e)
        {
            textBebida.IsEnabled = false;
            this.statusBebida = false;
        }

        private void RadioButtonBebidaSim_Click(object sender, RoutedEventArgs e)
        {
            textBebida.IsEnabled = true;
            this.statusBebida = true;
        }

        // Radio buttons - Gravidez
        private void RadioButtonGravidezNao_Click(object sender, RoutedEventArgs e)
        {
            textGravidez.IsEnabled = false;
            this.statusGravidez = Gravidez.Nenhuma;
        }

        private void RadioButtonGravidezNormal_Click(object sender, RoutedEventArgs e)
        {
            textGravidez.IsEnabled = true;
            this.statusGravidez = Gravidez.PartoNormal;
        }

        private void RadioButtonGravidezCesarea_Click(object sender, RoutedEventArgs e)
        {
            textGravidez.IsEnabled = true;
            this.statusGravidez = Gravidez.Cesarea;
        }

        // Radio buttons - Gripe
        private void RadioButtonGripeNao_Click(object sender, RoutedEventArgs e)
        {
            textGripe.IsEnabled = false;
            this.statusGripe = false;
        }

        private void RadioButtonGripeSim_Click(object sender, RoutedEventArgs e)
        {
            textGripe.IsEnabled = true;
            this.statusGripe = true;
        }

        // Radio buttons - Tatuagem
        private void RadioButtonTatuagemNao_Click(object sender, RoutedEventArgs e)
        {
            textTatuagem.IsEnabled = false;
            this.statusTatuagem = false;
        }

        private void RadioButtonTatuagemSim_Click(object sender, RoutedEventArgs e)
        {

            textTatuagem.IsEnabled = true;
            this.statusTatuagem = true;
        }

        // Radio buttons - Antecedente AVC
        private void RadioButtonAntecedenteAvcNao_Click(object sender, RoutedEventArgs e)
        {
            this.statusAntecedenteAvc = false;
        }

        private void RadioButtonAntecedenteAvcSim_Click(object sender, RoutedEventArgs e)
        {
            this.statusAntecedenteAvc = true;
        }


        #endregion

        private void ButtonCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (FormularioTriagemClinicaEstaCompleto() && FormularioImpedimentosTemporariosEstaCompleto())
            {
                // Informações do formulário.
                ImpedimentosTemporarios impedimentosTemporarios = CriarImpedimentosTemporarios();
                TriagemClinica triagemClinica = CriarTriagemClinica();
                ImpedimentosDefinitivos impedimentosDefinitivos = CriarImpedimentosDefinitivos();

                // Informações que serão preenchidas após recebimento do exame laboratorial.
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
                janelaPrincipal.RenderizarPerfilDoador(DoadorDAO.BuscarDoadorPorCpf(doador));
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
        }

        private ImpedimentosDefinitivos CriarImpedimentosDefinitivos()
        {
            return new ImpedimentosDefinitivos { AntecedenteAvc = statusAntecedenteAvc };
        }

        private bool FormularioImpedimentosTemporariosEstaCompleto()
        {
            if (this.statusBebida == null || this.statusGravidez == null || this.statusGripe == null || this.statusTatuagem == null)
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

        private bool FormularioTriagemClinicaEstaCompleto()
        {
            if (textPeso.Text.Equals("") || textPulso.Text.Equals("") || textTemperatura.Text.Equals(""))
            {
                return false;
            }

            return true;
        }

        private ImpedimentosTemporarios CriarImpedimentosTemporarios()
        {
            return new ImpedimentosTemporarios
            {
                BebidaAlcoolica = statusBebida,
                BebidaAlcoolicaUltimaVez = GetPeriodoBebida(),
                Gravidez = statusGravidez,
                GravidezUltimaVez = 0,
                Gripe = statusGripe,
                GripeUltimaVez = 0,
                Tatuagem = statusGripe,
                TatuagemUltimaVez = 0
            };
        }

        private TriagemClinica CriarTriagemClinica()
        {
            return new TriagemClinica
            {
                Peso = double.Parse(textPeso.Text),
                Pulso = int.Parse(textPulso.Text),
                Temperatura = int.Parse(textTemperatura.Text),
            };
        }

        private int? GetPeriodoBebida()
        {
            if (textBebida.IsEnabled == true)
            {
                return Convert.ToInt32(textBebida.Text);
            }
            return null;
        }

        private int? GetPeriodoGravidez()
        {
            if (textGravidez.IsEnabled == true)
            {
                return Convert.ToInt32(textGravidez.Text);
            }
            return null;
        }

        private int? GetPeriodoGripe()
        {
            if (textGripe.IsEnabled == true)
            {
                return Convert.ToInt32(textGripe.Text);
            }
            return null;
        }

        private int? GetPeriodoTatuagem()
        {
            if (textTatuagem.IsEnabled == true)
            {
                return Convert.ToInt32(textTatuagem.Text);
            }
            return null;
        }
    }
}


