using HemoSoft.DAL;
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
            CarregarDadosDoacao();
        }

        private void CarregarDadosDoacao()
        {
            ValidarBotoes();

            // Triagem Clinica
            textId.Text = "#" + Convert.ToString(doacao.IdDoacao);
            textPeso.Text = Convert.ToString(doacao.TriagemClinica.Peso) + " Kg";
            textPulso.Text = Convert.ToString(doacao.TriagemClinica.Pulso) + " bpm";
            textTemperatura.Text = Convert.ToString(doacao.TriagemClinica.Temperatura) + " °C";
            textStatus.Text = GetStatusTriagem();

            // Impedimentos Temporários
            textBebida.Text = GetStatusBebida();
            textGravidez.Text = GetStatusGravidez();
            textGripe.Text = GetStatusGripe();
            textTatuagem.Text = GetStatusTatuagem();

            // Triagem Laboratorial/Impedimentos Definitivos
            textAntecedenteAvc.Text = GetStatusAntecedenteAvc();
            textHepatiteB.Text = GetStatusHepatiteB();
            textHepatiteC.Text = GetStatusHepatiteC();
            textHiv.Text = GetStatusHiv();
        }

        private void ValidarBotoes()
        {
            if (doacao.StatusDoacao == StatusDoacao.AguardandoAtendimento)
            {
                ButtonConfimar.IsEnabled = true;
            }

            if (doacao.StatusDoacao == StatusDoacao.AguardandoResultados)
            {
                ButtonCadastrar.IsEnabled = true;
            }
        }

        #region Eventos de cliques
        private void ButtonCadastrarExame_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
            janelaPrincipal.RenderizarCadastroExame(doacao);
        }

        private void ButtonConfimarColetaDoacao_Click(object sender, RoutedEventArgs e)
        {
            doacao.StatusDoacao = StatusDoacao.AguardandoResultados;
            DoacaoDAO.AlterarDoacao(doacao);

            MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
            janelaPrincipal.RenderizarCadastroExame(doacao);
        }

        #endregion

        #region Validação de status e atributos
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

        private String GetStatusTriagem()
        {
            if (doacao.TriagemClinica.StatusTriagem == StatusTriagem.Aprovado && doacao.TriagemLaboratorial.StatusTriagem == StatusTriagem.Aprovado)
            {
                return "Aprovado";
            }

            if (doacao.TriagemClinica.StatusTriagem == StatusTriagem.Reprovado && doacao.TriagemLaboratorial.StatusTriagem == StatusTriagem.Aprovado)
            {
                return "Reprovado temporariamente";
            }

            if (doacao.TriagemClinica.StatusTriagem == StatusTriagem.Aprovado && doacao.TriagemLaboratorial.StatusTriagem == StatusTriagem.Reprovado)
            {
                return "Reprovado definitivamente";
            }

            return "Aguardando resultados";
        }
        #endregion
    }
}
