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

        private void ButonCadastrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (textNome.Text.Equals("") || textCpf.Text.Equals("") || boxEstadoCivil.SelectionBoxItem.Equals("") || boxGenero.SelectionBoxItem.Equals(""))
            {
                MessageBox.Show("Campo vazio!");
            }
            else
            {
                MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
                Grid GridPage = (Grid)parentWindow.FindName("GridPage");
                GridPage.Children.Clear();

                Doador doador = new Doador
                {
                    NomeCompleto = textNome.Text,
                    Cpf = textCpf.Text,
                    EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), boxEstadoCivil.Text),
                    Genero = (Genero)Enum.Parse(typeof(Genero), boxGenero.Text)
                };


                if (DoadorDAO.CadastrarDoador(doador))
                {
                    MessageBox.Show("Cadastro realizado com sucesso");
                }
                else
                {
                    MessageBox.Show("Cliente já cadastrado!");

                    UserControl usc = new ExibirDoador(DoadorDAO.BuscarDoadorPorCpf(doador));
                    GridPage.Children.Add(usc);
                }
            }
        }
    }
}
