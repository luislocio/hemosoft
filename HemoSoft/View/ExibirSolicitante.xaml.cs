using HemoSoft.Model;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirSolicitante.xaml
    /// </summary>
    public partial class ExibirSolicitante : UserControl
    {
        Solicitante solicitante;

        public ExibirSolicitante(Solicitante s)
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
