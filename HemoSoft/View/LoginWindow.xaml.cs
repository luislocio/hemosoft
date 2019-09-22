using HemoSoft.DAL;
using HemoSoft.Model;
using HemoSoft.Model.Enum;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private static Usuario usuario = SingletonUsuario.GetInstance();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (textUsuario.Text.Equals("") || textSenha.Equals(""))
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
            else
            {
                if (textUsuario.Text.Length == 14)
                {
                    AutenticarSolicitante(textUsuario, textSenha);
                }
                else
                {
                    AutenticarTriador(textUsuario, textSenha);
                }
            }

            if (usuario.IdUsuario > 0)
            {
                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
            else
            {
                MessageBox.Show("Dados Inválidos.");
            }
        }

        private void AutenticarTriador(TextBox textUsuario, PasswordBox textSenha)
        {
            Triador triadorBusca = new Triador
            {
                Matricula = this.textUsuario.Text
            };

            Triador triadorResultado = TriadorDAO.BuscarTriadorPorMatricula(triadorBusca);
            if (triadorResultado != null && triadorResultado.StatusUsuario != StatusUsuario.Inativo)
            {
                if (triadorResultado.Matricula.Equals(textUsuario.Text) && triadorResultado.Senha.Equals(textSenha.Password))
                {
                    usuario.IdUsuario = triadorResultado.IdTriador;
                    usuario.NomeDeUsuario = triadorResultado.NomeCompleto;
                    usuario.TipoUsuario = TipoUsuario.Triador;
                }
            }
        }

        private void AutenticarSolicitante(TextBox textUsuario, PasswordBox textSenha)
        {
            Solicitante solicitanteBusca = new Solicitante { Cnpj = textUsuario.Text };
            Solicitante solicitanteResultado = SolicitanteDAO.BuscarSolicitantePorCnpj(solicitanteBusca);

            if (solicitanteResultado != null && solicitanteResultado.StatusUsuario != StatusUsuario.Inativo)
            {
                if (solicitanteResultado.Cnpj.Equals(textUsuario.Text) && solicitanteResultado.Senha.Equals(textSenha.Password))
                {
                    usuario.IdUsuario = solicitanteResultado.IdSolicitante;
                    usuario.NomeDeUsuario = solicitanteResultado.RazaoSocial;
                    usuario.TipoUsuario = TipoUsuario.Solicitante;
                }
            }
        }
    }
}