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

namespace ProjectCa
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Model1Container DB = new Model1Container();

        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            Model1Container DB = new Model1Container();
            var query = from g in DB.MemberDetails
                        select g.MemberGmail;

            var query2 = from g in DB.MemberDetails
                         select g.Password;

            if (query.Contains(gmail_tbx.Text) && query2.Contains(password_tbx.Text))
            {
                // Login successful
                MessageBox.Show("Successfully logged in");
                openHomeWindow();
                this.Close();
            }
            else
            {
                // Login failed
                MessageBox.Show("Incorrect gmail or password please try again");
            }

        }

        private void openHomeWindow()
        {
            var newWindow = new HomWindow();
            newWindow.Show();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            openMainWindow();
            this.Close();
        }

        private void openMainWindow()
        {
            var newWindow = new MainWindow();
            newWindow.Show();
        }
    }
}
