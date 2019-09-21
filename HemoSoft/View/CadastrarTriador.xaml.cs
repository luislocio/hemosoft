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
    /// Interaction logic for CadastrarTriador.xaml
    /// </summary>
    public partial class CadastrarTriador : UserControl
    {
        public CadastrarTriador()
        {
            InitializeComponent();
        }

        #region Eventos de cliques
        private void ButtonCadastrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FormularioEstaCompleto())
            {
                Triador triador = CriarTriador();

                if (TriadorDAO.CadastrarTriador(triador))
                {
                    MessageBox.Show("Cadastro realizado com sucesso");
                }
                else
                {
                    MessageBox.Show("Triador já cadastrado!");
                }

                var janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilTriador(TriadorDAO.BuscarTriadorPorMatricula(triador));
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
        }

        private void ButtonListar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
            janelaPrincipal.RenderizarListaTriadores(TriadorDAO.ListarTriadores());
        }
        #endregion

        private bool FormularioEstaCompleto()
        {
            return
               !textNome.Text.Equals("") ||
               !textMatricula.Text.Equals("") ||
               !textSenha.Text.Equals("");
        }

        private Triador CriarTriador()
        {
            return new Triador
            {
                NomeCompleto = textNome.Text,
                Matricula = textMatricula.Text,
                Senha = textSenha.Text,
                StatusUsuario = StatusUsuario.Ativo
            };
        }


    }
}
