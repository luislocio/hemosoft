using HemoSoft.Model;
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
    }
}
