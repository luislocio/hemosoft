using HemoSoft.DAL;
using HemoSoft.Model;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window { 
    
        // TODO: Singleton perde valores ao mudar de janela
        // private static Usuario usuario = SingletonUsuario.GetInstance();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = null;
            if (textUsuario.Text.Equals("") || textSenha.Equals(""))
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
            else
            {
                if (textUsuario.Text.Length == 14)
                {
                    usuario = AutenticarSolicitante(textUsuario, textSenha);
                }
                else
                {
                    usuario = AutenticarTriador(textUsuario, textSenha);
                }
            }

            if (usuario != null)
            {
                MainWindow main = new MainWindow(usuario);
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
            else
            {
                MessageBox.Show("Dados Inválidos.");
            }
        }

        private Usuario AutenticarTriador(TextBox textUsuario, PasswordBox textSenha)
        {
            Triador triadorBusca = new Triador
            {
                Matricula = this.textUsuario.Text
            };

            Triador triadorResultado = TriadorDAO.BuscarTriadorPorMatricula(triadorBusca);
            if (triadorResultado != null)
            {
                if (triadorResultado.Matricula.Equals(textUsuario.Text) && triadorResultado.Senha.Equals(textSenha.Password))
                {
                    return new Usuario
                    {
                        IdUsuario = triadorResultado.IdTriador,
                        NomeDeUsuario = triadorResultado.NomeCompleto,
                        TipoUsuario = TipoUsuario.Triador
                    };
                }
            }

            return null;
        }

        private Usuario AutenticarSolicitante(TextBox textUsuario, PasswordBox textSenha)
        {
            Solicitante solicitanteBusca = new Solicitante { Cnpj = textUsuario.Text };
            Solicitante solicitanteResultado = SolicitanteDAO.BuscarSolicitantePorCnpj(solicitanteBusca);

            if (solicitanteResultado != null)
            {
                if (solicitanteResultado.Cnpj.Equals(textUsuario.Text) && solicitanteResultado.Senha.Equals(textSenha.Password))
                {
                    return new Usuario
                    {
                        IdUsuario = solicitanteResultado.IdSolicitante,
                        NomeDeUsuario = solicitanteResultado.RazaoSocial,
                        TipoUsuario = TipoUsuario.Solicitante
                    };
                }
            }

            return null;
        }
    }
}