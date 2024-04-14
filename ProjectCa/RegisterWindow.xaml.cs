using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }   

        private void openMainWindow()
        {
            var newWindow = new MainWindow();
            newWindow.Show();
        }

        private void register_btn1_Click(object sender, RoutedEventArgs e)
        {
            Model1Container DB = new Model1Container();

            MemberDetails newM = new MemberDetails();



            try
            {
                if (string.IsNullOrEmpty(gmail_tbx.Text) || string.IsNullOrEmpty(password_tbx.Text) || string.IsNullOrEmpty(memberFirstName_tbx.Text) || string.IsNullOrEmpty(memberLastName_tbx.Text))
                {
                    MessageBox.Show("Please fill in the required details:");
                }
                else
                {
                    newM.FirstName = memberFirstName_tbx.Text;
                    newM.LastName = memberLastName_tbx.Text;
                    newM.MemberGmail = gmail_tbx.Text;
                    newM.Password = password_tbx.Text;
                    DB.MemberDetails.Add(newM);
                    DB.SaveChanges();
                    MessageBox.Show("Succesfully Registered");
                    openMainWindow();
                    this.Close();
                }

            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show("Please fill in the required details:" + ex);
            }
        }
    }
}
