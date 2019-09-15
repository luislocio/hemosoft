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
    /// Interaction logic for ExibirDoador.xaml
    /// </summary>
    public partial class ExibirDoador : UserControl
    {
        Doador Doador;

        public ExibirDoador()
        {
            InitializeComponent();
        }

        public ExibirDoador(Doador d)
        {
            InitializeComponent();
            this.Doador = d;
            CarregarDoador();
        }

        private void CarregarDoador()
        {
            textNome.Text = Doador.NomeCompleto;
            textCpf.Text = Doador.Cpf;
            textEstadoCivil.Text = Doador.EstadoCivil.ToString();
            textGenero.Text = Doador.Genero.ToString();

            dataGridDoacao.ItemsSource = Doador.Doacoes; ;
        }

        private void DataGridDoacao_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Doacao doacaoSelecionada = dataGridDoacao.SelectedItem as Doacao;
            if (doacaoSelecionada != null)
            {
            MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
            janelaPrincipal.CarregarDoacao(DoacaoDAO.BuscarDoacaoPorId(doacaoSelecionada));
            }
        }
    }
}
