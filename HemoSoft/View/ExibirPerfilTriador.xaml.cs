﻿using HemoSoft.DAL;
using HemoSoft.Model;
using HemoSoft.Model.Enum;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HemoSoft.View
{
    /// <summary>
    /// Interaction logic for ExibirPerfilTriador.xaml
    /// </summary>
    public partial class ExibirPerfilTriador : UserControl
    {
        Triador triador;

        public ExibirPerfilTriador(Triador t)
        {
            InitializeComponent();
            this.triador = t;
            CarregarTriador();
        }

        private void CarregarTriador()
        {
            textNome.Text = triador.NomeCompleto;
            textMatricula.Text = triador.Matricula;
            boxStatusUsuario.SelectedItem = triador.StatusUsuario;
        }

        private void ButtonEditar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Habilitar edição dos campos do formulário.
            textNome.IsReadOnly = false;
            textMatricula.IsReadOnly = false;
            boxStatusUsuario.IsEnabled = true;

            // Transformar botão EDITAR em SALVAR.
            buttonEditar.Content = "Salvar";
            buttonEditar.Click -= new RoutedEventHandler(ButtonEditar_Click);
            buttonEditar.Click += new RoutedEventHandler(ButtonSalvar_Click);
        }

        private void ButtonSalvar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (FormularioEstaCompleto())
            {
                triador.NomeCompleto = textNome.Text;
                triador.Matricula = textMatricula.Text;
                triador.StatusUsuario = (StatusUsuario)Enum.Parse(typeof(StatusUsuario), boxStatusUsuario.Text);

                TriadorDAO.AlterarTriador(triador);
                // Desabilitar edição dos campos do formulário.
                textNome.IsReadOnly = true;
                textMatricula.IsReadOnly = true;
                boxStatusUsuario.IsEnabled = false;

                // Transformar botão SALVAR em EDITAR.
                buttonEditar.Content = "Editar";
                buttonEditar.Click -= new RoutedEventHandler(ButtonSalvar_Click);
                buttonEditar.Click += new RoutedEventHandler(ButtonEditar_Click);
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos.");
            }
        }

        private bool FormularioEstaCompleto()
        {
            return
                !textNome.Text.Equals("") &&
                !textMatricula.Text.Equals("") &&
                !boxStatusUsuario.SelectionBoxItem.Equals("");
        }
    }
}
