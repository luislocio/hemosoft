using HemoSoft.Model;
using System;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirDoacao.xaml
    /// </summary>
    public partial class ExibirDoacao : UserControl
    {
        Doacao Doacao;
        public ExibirDoacao(Doacao d)
        {
            InitializeComponent();
            this.Doacao = d;
            CarregarDoacao();
        }

        private void CarregarDoacao()
        {
            // Triagem Clinica
            textId.Text = "#" + Convert.ToString(Doacao.IdDoacao);
            textPeso.Text = Convert.ToString(Doacao.TriagemClinica.Peso) + " Kg";
            textPulso.Text = Convert.ToString(Doacao.TriagemClinica.Pulso) + " bpm";
            textTemperatura.Text = Convert.ToString(Doacao.TriagemClinica.Temperatura) + " °C";

            // Impedimentos Temporários
            textBebida.Text = GetStatusBebida();
            textGravidez.Text = GetStatusGravidez();
            textGripe.Text = GetStatusGripe();
            textTatuagem.Text = GetStatusTatuagem();

            // Triagem Laboratorial/Impedimentos Definitivos
        }

        private String GetStatusBebida()
        {
            if (Doacao.ImpedimentosTemporarios.BebidaAlcoolica == true)
            {
                return "Sim";
            }
            return "Não";
        }

        private String GetStatusGravidez()
        {
            return Doacao.ImpedimentosTemporarios.Gravidez.ToString();
        }

        private String GetStatusGripe()
        {
            if (Doacao.ImpedimentosTemporarios.Gripe == true)
            {
                return "Sim";
            }
            return "Não";
        }

        private String GetStatusTatuagem()
        {
            if (Doacao.ImpedimentosTemporarios.Tatuagem == true)
            {
                return "Sim";
            }
            return "Não";
        }
    }
}
