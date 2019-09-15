using HemoSoft.DAL;
using HemoSoft.Model;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = null;

            if (txtUsuario.Text.Equals("") || txtSenha.Equals(""))
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
            else
            {
                if (txtUsuario.Text.Length == 14)
                {
                    usuario = AutenticarSolicitante(txtUsuario, txtSenha);
                }
                else
                {
                    usuario = AutenticarTriador(txtUsuario, txtSenha);
                }

            }

            if (usuario != null)
            {
                // TODO: Passar usuario como parametro
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

        private Usuario AutenticarTriador(TextBox txtUsuario, PasswordBox txtSenha)
        {
            Triador triadorBusca = new Triador
            {
                Matricula = txtUsuario.Text
            };

            Triador triadorResultado = TriadorDAO.BuscarTriadorPorMatricula(triadorBusca);
            if (triadorResultado != null)
            {
                if (triadorResultado.Matricula.Equals(txtUsuario.Text) && triadorResultado.Senha.Equals(txtSenha.Password))
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

        private Usuario AutenticarSolicitante(TextBox txtUsuario, PasswordBox txtSenha)
        {
            Solicitante solicitanteBusca = new Solicitante { Cnpj = txtUsuario.Text };
            Solicitante solicitanteResultado = SolicitanteDAO.BuscarSolicitantePorCnpj(solicitanteBusca);

            if (solicitanteResultado != null)
            {
                if (solicitanteResultado.Cnpj.Equals(txtUsuario.Text) && solicitanteResultado.Senha.Equals(txtSenha.Password))
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