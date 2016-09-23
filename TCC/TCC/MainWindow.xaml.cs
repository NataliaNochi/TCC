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

        private void btTesteF2_Click(object sender, RoutedEventArgs e)
        {
            FrmFase2 f2 = new FrmFase2();
            f2.ShowDialog();
        }

        private void btCadastrarUser_Click(object sender, RoutedEventArgs e)
        {
            FrmCadastro cadUser = new FrmCadastro();
            cadUser.ShowDialog();
        }

        

        private void btEntrar_Click(object sender, RoutedEventArgs e)
        {
            string email = txtLogin.Text;
            string senha = txtSenha.Text;
            try
            {
                UsuarioVO user = new UsuarioVO();
                user = UsuarioDAO.Login(email, senha);

                if (user != null)
                {
                    //Dados.idusuario = usu.GetIdusuario();
                    //Dados.nome = usu.GetNome();

                    FrmFase2 f2 = new FrmFase2();
                    f2.ShowDialog();
                }
                else
                    throw new Exception("Email ou Senha inválidos, tente novamente por favor");
            }
            catch(Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
        }

      
    }
}
