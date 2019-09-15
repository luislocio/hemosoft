using HemoSoft.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirDoacao.xaml
    /// </summary>
    public partial class ExibirDoacao : UserControl
    {
        Doacao doacao;

        public ExibirDoacao(Doacao d)
        {
            InitializeComponent();
            this.doacao = d;
            CarregarDoacao();
        }

        private void CarregarDoacao()
        {
            if (doacao.TriagemLaboratorial.StatusTriagem != null)
            {
                ButtonCadastrar.IsEnabled = false;
            }

            // Triagem Clinica
            textId.Text = "#" + Convert.ToString(doacao.IdDoacao);
            textPeso.Text = Convert.ToString(doacao.TriagemClinica.Peso) + " Kg";
            textPulso.Text = Convert.ToString(doacao.TriagemClinica.Pulso) + " bpm";
            textTemperatura.Text = Convert.ToString(doacao.TriagemClinica.Temperatura) + " °C";

            // Impedimentos Temporários
            textBebida.Text = GetStatusBebida();
            textGravidez.Text = GetStatusGravidez();
            textGripe.Text = GetStatusGripe();
            textTatuagem.Text = GetStatusTatuagem();

            // TODO: Triagem Laboratorial/Impedimentos Definitivos
            textAntecedenteAvc.Text = GetStatusAntecedenteAvc();
            textHepatiteB.Text = GetStatusHepatiteB();
            textHepatiteC.Text = GetStatusHepatiteC();
            textHiv.Text = GetStatusHiv();
        }

        private String GetStatusBebida()
        {
            if (doacao.ImpedimentosTemporarios.BebidaAlcoolica == true)
            {
                return "Sim";
            }
            return "Não";
        }

        private String GetStatusGravidez()
        {
            return doacao.ImpedimentosTemporarios.Gravidez.ToString();
        }

        private String GetStatusGripe()
        {
            if (doacao.ImpedimentosTemporarios.Gripe == true)
            {
                return "Sim";
            }
            return "Não";
        }

        private String GetStatusTatuagem()
        {
            if (doacao.ImpedimentosTemporarios.Tatuagem == true)
            {
                return "Sim";
            }
            return "Não";
        }

        private String GetStatusAntecedenteAvc()
        {
            if (doacao.ImpedimentosDefinitivos.AntecedenteAvc == true)
            {
                return "Positivo";
            }

            return "Negativo";
        }

        private string GetStatusHepatiteB()
        {
            if (doacao.ImpedimentosDefinitivos.HepatiteB == true)
            {
                return "Positivo";
            }

            if (doacao.ImpedimentosDefinitivos.HepatiteB == false)
            {
                return "Negativo";
            }

            return "Aguardando resultados";
        }

        private String GetStatusHepatiteC()
        {
            if (doacao.ImpedimentosDefinitivos.HepatiteC == true)
            {
                return "Positivo";
            }

            if (doacao.ImpedimentosDefinitivos.HepatiteC == false)
            {
                return "Negativo";
            }

            return "Aguardando resultados";
        }

        private String GetStatusHiv()
        {
            if (doacao.ImpedimentosDefinitivos.Hiv == true)
            {
                return "Positivo";
            }

            if (doacao.ImpedimentosDefinitivos.Hiv == false)
            {
                return "Negativo";
            }

            return "Aguardando resultados";
        }

        private void ButtonCadastrarExame_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
            janelaPrincipal.RenderizarCadastroExame(doacao);
        }
    }
}
