using HemoSoft.DAL;
using HemoSoft.Model;
using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for CadastrarDoador.xaml
    /// </summary>
    public partial class CadastrarDoador : UserControl
    {
        public CadastrarDoador()
        {
            InitializeComponent();
        }

        private void ButtonCadastrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FormularioEstaCompleto())
            {
                MessageBox.Show("Favor preencher todos os campos!");
            }
            else
            {
                Doador doador = CriarDoador();

                if (DoadorDAO.CadastrarDoador(doador))
                {
                    MessageBox.Show("Cadastro realizado com sucesso");
                }
                else
                {
                    MessageBox.Show("Cliente já cadastrado!");
                }

                var janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilDoador(DoadorDAO.BuscarDoadorPorCpf(doador));
            }
        }

        private bool FormularioEstaCompleto()
        {
            return
                textNome.Text.Equals("") ||
                textCpf.Text.Equals("") ||
                boxEstadoCivil.SelectionBoxItem.Equals("") ||
                boxGenero.SelectionBoxItem.Equals("");
        }

        private Doador CriarDoador()
        {
            return new Doador
            {
                NomeCompleto = textNome.Text,
                Cpf = textCpf.Text,
                EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), boxEstadoCivil.Text),
                Genero = (Genero)Enum.Parse(typeof(Genero), boxGenero.Text)
            };
        }
    }
}
