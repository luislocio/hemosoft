using HemoSoft.DAL;
using HemoSoft.Model;
using HemoSoft.Model.Enum;
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

        #region Eventos de radioButtons
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

        #region Eventos de cliques
        private void ButtonCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (FormularioTriagemClinicaEstaCompleto() && FormularioImpedimentosTemporariosEstaCompleto())
            {
                // Informações do formulário.
                ImpedimentosDefinitivos impedimentosDefinitivos = CriarImpedimentosDefinitivos();
                ImpedimentosTemporarios impedimentosTemporarios = CriarImpedimentosTemporarios();
                TriagemClinica triagemClinica = CriarTriagemClinica();

                // Informações que serão preenchidas após recebimento do exame laboratorial.
                TriagemLaboratorial triagemLaboratorial = new TriagemLaboratorial { };

                Doacao doacao = CriarDoacao(impedimentosTemporarios, triagemClinica, impedimentosDefinitivos, triagemLaboratorial);
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
        #endregion

        #region Validação dos formulários
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
        #endregion

        #region Criação de objetos para cadastro
        private Doacao CriarDoacao(ImpedimentosTemporarios impedimentosTemporarios, TriagemClinica triagemClinica, ImpedimentosDefinitivos impedimentosDefinitivos, TriagemLaboratorial triagemLaboratorial)
        {
            return new Doacao
            {
                DataDoacao = DateTime.Now,
                Doador = this.doador,
                Triador = this.triador,
                TriagemClinica = triagemClinica,
                TriagemLaboratorial = triagemLaboratorial,
                StatusDoacao = GetStatusDoacao(triagemClinica, impedimentosDefinitivos),
                ImpedimentosTemporarios = impedimentosTemporarios,
                ImpedimentosDefinitivos = impedimentosDefinitivos
            };
        }

        private ImpedimentosDefinitivos CriarImpedimentosDefinitivos()
        {
            return new ImpedimentosDefinitivos { AntecedenteAvc = statusAntecedenteAvc };
        }

        private ImpedimentosTemporarios CriarImpedimentosTemporarios()
        {
            return new ImpedimentosTemporarios
            {
                BebidaAlcoolica = statusBebida,
                BebidaAlcoolicaUltimaVez = GetPeriodoBebida(),
                Gravidez = statusGravidez,
                GravidezUltimaVez = GetPeriodoGravidez(),
                Gripe = statusGripe,
                GripeUltimaVez = GetPeriodoGripe(),
                Tatuagem = statusGripe,
                TatuagemUltimaVez = GetPeriodoTatuagem()
            };
        }

        private TriagemClinica CriarTriagemClinica()
        {
            return new TriagemClinica
            {
                Peso = double.Parse(textPeso.Text),
                Pulso = int.Parse(textPulso.Text),
                Temperatura = int.Parse(textTemperatura.Text),
                StatusTriagem = GetStatusTriagemClinica()
            };
        }

        #endregion

        #region Validação de status e atributos
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

        private StatusDoacao GetStatusDoacao(TriagemClinica triagemClinica, ImpedimentosDefinitivos impedimentosDefinitivos)
        {
            if (triagemClinica.StatusTriagem == StatusTriagem.Aprovado &&
                impedimentosDefinitivos.AntecedenteAvc == false)
            {
                return StatusDoacao.AguardandoAtendimento;
            }

            return StatusDoacao.AguardandoResultados;
        }
        private StatusTriagem GetStatusTriagemClinica()
        {
            if (statusBebida == false &&
                statusGravidez == Gravidez.Nenhuma &&
                statusGripe == false &&
                statusTatuagem == false)
            {
                return StatusTriagem.Aprovado;
            }

            return StatusTriagem.Reprovado;
        }
        #endregion

        #region Validação triagem clinica
        private void TextPeso_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(textPeso.Text) < 50)
            {
                ButtonCadastrar.IsEnabled = false;
                MessageBox.Show("Doador deve ter mais de 50 kg.");
            }
            else
            {
                ButtonCadastrar.IsEnabled = true;
            }
        }

        private void TextPulso_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(textPulso.Text) < 60 ||
                Convert.ToInt32(textPulso.Text) > 100)
            {
                ButtonCadastrar.IsEnabled = false;
                MessageBox.Show("Doador deve estar com batimentos entre 60bpm e 100bpm.");
            }
            else
            {
                ButtonCadastrar.IsEnabled = true;
            }
        }

        private void TextTemperatura_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(textTemperatura.Text) < 36.1 ||
                Convert.ToDouble(textTemperatura.Text) > 37.2)
            {
                ButtonCadastrar.IsEnabled = false;
                MessageBox.Show("Doador deve estar com temperatura entre 36,1°C e 37.2°C.");
            }
            else
            {
                ButtonCadastrar.IsEnabled = true;
            }
        }
        #endregion
    }
}


