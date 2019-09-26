using HemoSoft.DAL;
using HemoSoft.Model;
using HemoSoft.Model.Enum;
using HemoSoft.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirPerfilSolicitante.xaml
    /// </summary>
    public partial class ExibirPerfilSolicitante : UserControl
    {
        Solicitante solicitante;

        public ExibirPerfilSolicitante(Solicitante s)
        {
            InitializeComponent();
            this.solicitante = s;
            CarregarSolicitante();
        }

        private void CarregarSolicitante()
        {
            textCnpj.Text = solicitante.Cnpj;
            textRazaoSocial.Text = solicitante.RazaoSocial;
            textResponsavel.Text = solicitante.Responsavel;
            boxStatusUsuario.SelectedItem = solicitante.StatusUsuario;
        }

        private void ButtonEditar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Habilitar edição dos campos do formulário.
            textCnpj.IsReadOnly = false;
            textRazaoSocial.IsReadOnly = false;
            textResponsavel.IsReadOnly = false;
            boxStatusUsuario.IsEnabled = true;

            // Transformar botão EDITAR em SALVAR.
            buttonEditar.Content = "Salvar";
            buttonEditar.Click -= new RoutedEventHandler(ButtonEditar_Click);
            buttonEditar.Click += new RoutedEventHandler(ButtonSalvar_Click);
        }

        private void ButtonSalvar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FormularioEstaCompleto())
            {
                if (Validacao.CnpjEhValido(textCnpj.Text))
                {
                    solicitante.Cnpj = textCnpj.Text;
                    solicitante.RazaoSocial = textRazaoSocial.Text;
                    solicitante.Responsavel = textResponsavel.Text;
                    solicitante.StatusUsuario = (StatusUsuario)Enum.Parse(typeof(StatusUsuario), boxStatusUsuario.Text);

                    SolicitanteDAO.AlterarSolicitante(solicitante);

                    // Desabilitar edição dos campos do formulário.
                    textCnpj.IsReadOnly = true;
                    textRazaoSocial.IsReadOnly = true;
                    textResponsavel.IsReadOnly = true;
                    boxStatusUsuario.IsEnabled = false;

                    // Transformar botão SALVAR em EDITAR.
                    buttonEditar.Content = "Editar";
                    buttonEditar.Click -= new RoutedEventHandler(ButtonSalvar_Click);
                    buttonEditar.Click += new RoutedEventHandler(ButtonEditar_Click);
                }
                else
                {
                    MessageBox.Show("CNPJ inválido.");
                }
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos.");
            }
        }

        private bool FormularioEstaCompleto()
        {
            return
                !textCnpj.Text.Equals("") &&
                !textRazaoSocial.Text.Equals("") &&
                !textResponsavel.Text.Equals("") &&
                !boxStatusUsuario.SelectionBoxItem.Equals("");
        }
    }
}
