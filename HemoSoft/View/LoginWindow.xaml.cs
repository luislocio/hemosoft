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
            bool usuarioAutenticado = false;

            if (txtUsuario.Text.Equals("") || txtSenha.Equals(""))
            {
                MessageBox.Show("Campo vazio!");
            }
            else
            {
                if (txtUsuario.Text.Length == 14)
                {
                    usuarioAutenticado = AutenticarSolicitante(txtUsuario, txtSenha);
                } else
                {
                    usuarioAutenticado = AutenticarTriador(txtUsuario, txtSenha);
                }
                
            }

            if (usuarioAutenticado == true)
            {
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