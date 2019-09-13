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
    /// Interaction logic for CadastrarDoacao.xaml
    /// </summary>
    public partial class CadastrarDoacao : UserControl
    {
        bool bebida;
        bool gripe;
        bool tatuagem;
        Gravidez gravidez;

        public CadastrarDoacao()
        {
            InitializeComponent();
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
            if (textPeso.Text.Equals("") || textPulso.Text.Equals("") || textTemperatura.Text.Equals(""))
            {

            }

            Triador triador = new Triador
            {
                NomeCompleto = "triador1",
                Matricula = "triador1",
                Senha = "senhatriador1",
                StatusUsuario = StatusUsuario.Ativo,
            };

            Doador doador = new Doador
            {
                NomeCompleto = "doador1",
                Cpf = "012345678901",
                Genero = Genero.Feminino,
                EstadoCivil = EstadoCivil.Separadx
            };

            TriagemClinica triagemClinica = new TriagemClinica
            {
                Peso = double.Parse(textPeso.Text),
                Pulso = int.Parse(textPulso.Text),
                Temperatura = int.Parse(textTemperatura.Text),
            };

            TriagemLaboratorial triagemLaboratorial = new TriagemLaboratorial { };

            ImpedimentosDefinitivos impedimentosDefinitivos = new ImpedimentosDefinitivos { };

            ImpedimentosTemporarios impedimentosTemporarios = new ImpedimentosTemporarios
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

            Doacao doacao = new Doacao
            {
                DataDoacao = DateTime.Now,
                StatusDoacao = StatusDoacao.AguardandoAtendimento,
                Doador = doador,
                Triador = triador,
                TriagemClinica = triagemClinica,
                TriagemLaboratorial = triagemLaboratorial,
                ImpedimentosTemporarios = impedimentosTemporarios,
                ImpedimentosDefinitivos = impedimentosDefinitivos
            };

            TriadorDAO.CadastrarTriador(triador);
            DoadorDAO.CadastrarDoador(doador);
            DoacaoDAO.CadastrarDoacao(doacao);
        }
    }
}
