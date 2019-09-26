using SasaCrljenica_AdoNetWPFApplication.View;
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

namespace SasaCrljenica_AdoNetWPFApplication
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userName = "admin";
            string password = "admin";
            if (txtName.Text.Equals(userName) && passwordBox.Password.Equals(password))
            {
                MainView mainView = new MainView();
                this.Close();
                mainView.ShowDialog();

            }
            else
            {
                MessageBox.Show("Error! Please enter valid Username and password!","Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
