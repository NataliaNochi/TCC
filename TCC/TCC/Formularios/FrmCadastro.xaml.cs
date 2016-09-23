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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;
using TCC.Classes.Cadastro;

namespace TCC.Formularios
{
    /// <summary>
    /// Interaction logic for FrmCadastro.xaml
    /// </summary>
    public partial class FrmCadastro : Window
    {
        public FrmCadastro()
        {
            InitializeComponent();
        }


        public void LimparCampos()
        {
            txtCPF.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtSenha.Clear();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UsuarioVO user = new UsuarioVO();
                user.SetCPF(txtCPF.Text);
                user.SetNome(txtNome.Text);
                user.SetEmail(txtEmail.Text);
                user.SetSenha(txtSenha.Text);

                UsuarioDAO.Cadastrar(user);
                LimparCampos();
                MessageBox.Show("Usuário Cadastrado com Sucesso");

                FrmCadastro cadUser = new FrmCadastro();
                cadUser.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            FrmCadastro cadUser = new FrmCadastro();
            cadUser.Close();
        }
    }
}
