using HemoSoft.DAL;
using HemoSoft.Model;
using HemoSoft.Model.Enum;
using HemoSoft.Utils;
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
    /// Interaction logic for CadastrarSolicitante.xaml
    /// </summary>
    public partial class CadastrarSolicitante : UserControl
    {
        public CadastrarSolicitante()
        {
            InitializeComponent();
        }

        #region Eventos de cliques
        private void ButtonCadastrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FormularioEstaCompleto())
            {
                if (Validacao.CnpjEhValido(textCnpj.Text))
                {
                    Solicitante solicitante = CriarSolicitante();

                    if (SolicitanteDAO.CadastrarSolicitante(solicitante))
                    {
                        MessageBox.Show("Cadastro realizado com sucesso");
                    }
                    else
                    {
                        MessageBox.Show("Solicitante já cadastrado!");
                    }

                    var janelaPrincipal = Window.GetWindow(this) as MainWindow;
                    janelaPrincipal.RenderizarPerfilSolicitante(SolicitanteDAO.BuscarSolicitantePorCnpj(solicitante));
                }
                else
                {
                    MessageBox.Show("CNPJ inválido");

                }
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
        }

        private void ButtonListar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
            janelaPrincipal.RenderizarListaSolicitantes(SolicitanteDAO.ListarSolicitantes());
        }
        #endregion

        private bool FormularioEstaCompleto()
        {
            return
                !textCnpj.Text.Equals("") ||
                !textRazaoSocial.Text.Equals("") ||
                !textResponsavel.Text.Equals("") ||
                !textSenha.Text.Equals("");
        }

        private Solicitante CriarSolicitante()
        {
            return new Solicitante
            {
                Cnpj = textCnpj.Text,
                RazaoSocial = textRazaoSocial.Text,
                Responsavel = textResponsavel.Text,
                Senha = textSenha.Text,
                StatusUsuario = StatusUsuario.Ativo
            };
        }

    }
}

