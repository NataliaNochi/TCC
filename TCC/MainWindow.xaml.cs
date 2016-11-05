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
using TCC.Classes.Cadastro;
using TCC.Formularios;

namespace TCC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        UsuarioVO user = new UsuarioVO();
        string email;
        string senha;
        private void fazerLogin()
        {
            email = txtLogin.Text;
            senha = txtSenha.Password;
            try
            {

                user = UsuarioDAO.Login(email, senha);

                if (user != null)
                {

                    gbFases.Visibility = System.Windows.Visibility.Visible;
                    gbLogin.Visibility = System.Windows.Visibility.Hidden;

                }
                else
                    throw new Exception("Email ou Senha inválidos, tente novamente por favor");
            }
            catch (Exception erro)
            {
                msgTitulo.Text = "Erro";
                msgTexto.Text = erro.Message;
                gdMensagem.Visibility = System.Windows.Visibility.Visible;
            }
        }
        #region Grid de login
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            fazerLogin();
        }

        #endregion

        #region Grid de fases
        private void btnF1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                FrmFase1 f1 = new FrmFase1(user);
                f1.ShowDialog();
            }
            finally
            {
                this.Show();
            }
        }

        private void btnF2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                FrmFase2 f2 = new FrmFase2();
                f2.ShowDialog();
            }
            finally
            {
                this.Show();
            }
        }

        private void btnLogoff_Click(object sender, RoutedEventArgs e)
        {
            user = null;
            gbFases.Visibility = System.Windows.Visibility.Hidden;
            gbLogin.Visibility = System.Windows.Visibility.Visible;

            txtLogin.Text = "Login";
            txtSenha.Password = "";
        }
        #endregion

        #region Grid mensagem
        private void msgTitulo_MouseEnter(object sender, MouseEventArgs e)
        {
            ThicknessConverter thickness = new ThicknessConverter();
            msgTitulo.BorderThickness = (System.Windows.Thickness)thickness.ConvertFromString("0");
        } //esconder a borda do textbox para não aparecer quando o mouse estiver nele

        private void msgTexto_MouseEnter(object sender, MouseEventArgs e)
        {
            ThicknessConverter thickness = new ThicknessConverter();
            msgTexto.BorderThickness = (System.Windows.Thickness)thickness.ConvertFromString("0");
        } //esconder a borda do textbox para não aparecer quando o mouse estiver nele

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            gdMensagem.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion

        #region Grid cadastro
        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UsuarioVO user = new UsuarioVO();
                user.SetCPF(txtCadCpf.Text);
                user.SetNome(txtCadNome.Text);
                user.SetEmail(txtCadEmail.Text);
                if (txtCadSenha.Password == txtCadConfirmaSenha.Password)
                {
                    user.SetSenha(txtCadSenha.Password);
                    UsuarioDAO.Cadastrar(user);
                    LimparCampos();
                    lbSucesso.Visibility = System.Windows.Visibility.Visible;
                    lbSenhaDiferente.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show(txtCadSenha.Password);
                    MessageBox.Show(txtCadConfirmaSenha.Password);
                    lbSenhaDiferente.Visibility = System.Windows.Visibility.Visible;
                    lbSucesso.Visibility = System.Windows.Visibility.Hidden;
                }
 
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        public void LimparCampos()
        {
            txtCadCpf.Clear();
            txtCadNome.Clear();
            txtCadEmail.Clear();
            txtSenha.Clear();
            txtCadConfirmaSenha.Clear();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            gbCadastro.Visibility = System.Windows.Visibility.Hidden;
            gbLogin.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion

        #region acerta foco e placeholder
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            gbLogin.Visibility = System.Windows.Visibility.Hidden;
            gbCadastro.Visibility = System.Windows.Visibility.Visible;

        }

        private void txtLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "Login")
                txtLogin.Text = string.Empty;
        }

        private void txtLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == string.Empty)
                txtLogin.Text = "Login";
        }

        private void txtSenha_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSenha.Password == "senha")
                txtSenha.Password = string.Empty;
        }

        private void txtSenha_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSenha.Password == string.Empty)
                txtSenha.Password = "senha";
        }

        private void txtLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtSenha.Focus();
        }

        private void txtSenha_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                fazerLogin();
        }
        #endregion

    }
}
