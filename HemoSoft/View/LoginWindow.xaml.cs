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
            bool autenticacaoDoUsuario = false;

            if (txtUsuario.Text.Equals("") || txtSenha.Equals(""))
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
            else
            {
                if (txtUsuario.Text.Length == 14)
                {
                    autenticacaoDoUsuario = AutenticarSolicitante(txtUsuario, txtSenha);
                } else
                {
                    autenticacaoDoUsuario = AutenticarTriador(txtUsuario, txtSenha);
                }
                
            }

            if (autenticacaoDoUsuario == true)
            {
                // TODO: Passar usuario como parametro
                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            } else
            {
                MessageBox.Show("Dados Inválidos.");
            }
        }

        private bool AutenticarTriador(TextBox txtUsuario, PasswordBox txtSenha)
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
                    return true;
                }
            }

            return false;
        }

        // TODO: Implementar login do solicitante
        private bool AutenticarSolicitante(TextBox txtUsuario, PasswordBox txtSenha)
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
                    return true;
                }
            }

            return false;
        }
    }
}