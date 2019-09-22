using HemoSoft.DAL;
using HemoSoft.Model;
using HemoSoft.Model.Enum;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for CadastrarExame.xaml
    /// </summary>
    public partial class CadastrarExame : UserControl
    {
        Doacao doacao;
        Boolean? statusHepatiteB;
        Boolean? statusHepatiteC;
        Boolean? statusHiv;

        public CadastrarExame(Doacao d)
        {
            InitializeComponent();
            this.doacao = d;
            CarregarDadosExistentes();
        }

        private void CarregarDadosExistentes()
        {
            if (doacao.Doador.FatorRh != null && doacao.Doador.TipoSanguineo != null)
            {
                boxFatorRh.SelectedItem = doacao.Doador.FatorRh;
                boxFatorRh.IsEnabled = false;

                boxTipoSanguineo.SelectedItem = doacao.Doador.TipoSanguineo;
                boxTipoSanguineo.IsEnabled = false;
            };
        }

        #region Radio Buttons

        private void RadioButtonHepatiteBNegativo_Click(object sender, RoutedEventArgs e)
        {
            statusHepatiteB = false;
        }

        private void RadioButtonHepatiteBPositivo_Click(object sender, RoutedEventArgs e)
        {
            statusHepatiteB = true;
        }

        private void RadioButtonHepatiteCNegativo_Click(object sender, RoutedEventArgs e)
        {
            statusHepatiteC = false;
        }

        private void RadioButtonHepatiteCPositivo_Click(object sender, RoutedEventArgs e)
        {
            statusHepatiteC = true;
        }

        private void RadioButtonHivNegativo_Click(object sender, RoutedEventArgs e)
        {
            statusHiv = false;
        }

        private void RadioButtonHivPositivo_Click(object sender, RoutedEventArgs e)
        {
            statusHiv = true;
        }
        #endregion

        #region Eventos de cliques
        private void ButtonCadastrarExame_Click(object sender, RoutedEventArgs e)
        {
            if (FormularioEstaCompleto())
            {
                doacao.TriagemLaboratorial = AtualizarTriagemLaboratorial();
                doacao.ImpedimentosDefinitivos = AtualizarImpedimentosDefinitivos(doacao.TriagemLaboratorial);
                doacao.Doador = AtualizarDadosDoSangue();
                doacao.StatusDoacao = GetStatusDoacao();

                DoacaoDAO.AlterarDoacao(doacao);

                MessageBox.Show("Exame laboratorial cadastrado com sucesso");

                var janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilDoacao(doacao);
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
        } 
        #endregion

        #region Atualização de atributos da doação
        private TriagemLaboratorial AtualizarTriagemLaboratorial()
        {
            TriagemLaboratorial triagemLaboratorial = doacao.TriagemLaboratorial;

            triagemLaboratorial.HepatiteB = statusHepatiteB;
            triagemLaboratorial.HepatiteC = statusHepatiteC;
            triagemLaboratorial.Hiv = statusHiv;
            triagemLaboratorial.StatusTriagem = GetStatusExameLaboratorial();

            return triagemLaboratorial;
        }

        private ImpedimentosDefinitivos AtualizarImpedimentosDefinitivos(TriagemLaboratorial triagemLaboratorial)
        {
            ImpedimentosDefinitivos impedimentosDefinitivos = doacao.ImpedimentosDefinitivos;

            impedimentosDefinitivos.AntecedenteAvc = doacao.ImpedimentosDefinitivos.AntecedenteAvc;
            impedimentosDefinitivos.HepatiteB = triagemLaboratorial.HepatiteB;
            impedimentosDefinitivos.HepatiteC = triagemLaboratorial.HepatiteC;
            impedimentosDefinitivos.Hiv = triagemLaboratorial.Hiv;

            return impedimentosDefinitivos;
        }

        private Doador AtualizarDadosDoSangue()
        {
            Doador doador = doacao.Doador;

            doador.TipoSanguineo = (TipoSanguineo)Enum.Parse(typeof(TipoSanguineo), boxTipoSanguineo.Text);
            doador.FatorRh = (FatorRh)Enum.Parse(typeof(FatorRh), boxFatorRh.Text);

            return doador;
        } 
        #endregion

        #region Validação de status e atributos
        private StatusTriagem GetStatusExameLaboratorial()
        {
            if (doacao.ImpedimentosDefinitivos.AntecedenteAvc == true ||
                statusHepatiteB == true ||
                statusHepatiteC == true ||
                statusHiv == true)
            {
                return StatusTriagem.Reprovado;
            }
            return StatusTriagem.Aprovado;
        }

        private StatusDoacao GetStatusDoacao()
        {
            if (doacao.TriagemLaboratorial.StatusTriagem == StatusTriagem.Aprovado &&
                doacao.TriagemClinica.StatusTriagem == StatusTriagem.Aprovado)
            {
                return StatusDoacao.Disponivel;
            }

            return StatusDoacao.NaoDisponivel;
        }
        #endregion

        #region Validação dos campos do formulário
        private bool FormularioEstaCompleto()
        {
            return this.statusHepatiteB != null ||
                this.statusHepatiteC != null ||
                this.statusHiv != null ||
                boxFatorRh.SelectedItem != null ||
                boxTipoSanguineo.SelectedItem != null;
        } 
        #endregion
    }
}
