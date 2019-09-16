using HemoSoft.Model;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirTriador.xaml
    /// </summary>
    public partial class ExibirTriador : UserControl
    {
        Triador triador;

        public ExibirTriador(Triador t)
        {
            InitializeComponent();
            this.triador = t;
            CarregarTriador();
        }

        private void CarregarTriador()
        {
            textNome.Text = triador.NomeCompleto;
            textMatricula.Text = triador.Matricula;
            textStatus.Text = triador.StatusUsuario.ToString();
        }
    }
}
