using HemoSoft.DAL;
using HemoSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void ButtonCadastrarExame_Click(object sender, RoutedEventArgs e)
        {
            if (FormularioEstaCompleto())
            {
                TriagemLaboratorial triagemLaboratorial = CriarTriagemLaboratorial();
                ImpedimentosDefinitivos impedimentosDefinitivos = CriarImpetimentosDefinitivos(triagemLaboratorial);

                MessageBox.Show("Exame laboratorial com sucesso");

                var janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilDoacao(doacao);
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
        }

        private ImpedimentosDefinitivos CriarImpetimentosDefinitivos(TriagemLaboratorial triagemLaboratorial)
        {
            return new ImpedimentosDefinitivos()
            {
                AntecedenteAvc = doacao.ImpedimentosDefinitivos.AntecedenteAvc,
                HepatiteB = triagemLaboratorial.HepatiteB,
                HepatiteC = triagemLaboratorial.HepatiteC,
                Hiv = triagemLaboratorial.Hiv,
            };
        }

        private TriagemLaboratorial CriarTriagemLaboratorial()
        {
            return new TriagemLaboratorial
            {
                HepatiteB = statusHepatiteB,
                HepatiteC = statusHepatiteC,
                Hiv = statusHiv,
                StatusTriagem = VerificarResultadoDoExame(),
                FatorRh = (FatorRh)Enum.Parse(typeof(FatorRh), boxFatorRh.Text),
                TipoSanguineo = (TipoSanguineo)Enum.Parse(typeof(TipoSanguineo), boxTipoSanguineo.Text)
            };
        }

        private StatusTriagem VerificarResultadoDoExame()
        {
            if (statusHepatiteB == true ||
                statusHepatiteC == true ||
                statusHiv == true)
            {
                return StatusTriagem.Reprovado;
            }
            return StatusTriagem.Aprovado;
        }

        private bool FormularioEstaCompleto()
        {
            if (this.statusHepatiteB == null ||
                this.statusHepatiteC == null ||
                this.statusHiv == null ||
                boxFatorRh.SelectedItem.Equals("") ||
                boxTipoSanguineo.SelectedItem.Equals(""))
            {
                return false;
            }
            return true;
        }
    }
}
