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

namespace ProjectCa
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

        private void login_Click(object sender, RoutedEventArgs e)
        {
            openLoginWindow();
            this.Close();
        }

        private void openLoginWindow()
        {
            var newWindow = new LoginWindow();
            newWindow.Show();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            openRegisterWindow();
            this.Close();
        }

        private void openRegisterWindow()
        {
            var newWindow = new RegisterWindow();
            newWindow.Show();
        }
    }
}
