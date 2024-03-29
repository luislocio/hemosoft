﻿using HemoSoft.DAL;
using HemoSoft.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirListaTriadores.xaml
    /// </summary>
    public partial class ExibirListaTriadores : UserControl
    {
        public ExibirListaTriadores(List<Triador> t)
        {
            InitializeComponent();
            dataGridTriadores.ItemsSource = t;
        }

        private void DataGridTriadores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Triador triadorSelecionado = dataGridTriadores.SelectedItem as Triador;

            if (triadorSelecionado != null)
            {
                MainWindow janelaPrincipal = Window.GetWindow(this) as MainWindow;
                janelaPrincipal.RenderizarPerfilTriador(TriadorDAO.BuscarTriadorPorId(triadorSelecionado));
            }
        }
    }
}
