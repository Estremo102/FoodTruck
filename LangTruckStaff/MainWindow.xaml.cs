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
using FoodTruck;

namespace LangTruckStaff
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Session s = new Session();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (s.LogIn(LoginField.Text, PasswordField.Password))
            {
                s.GetEmployeeData(LoginField.Text);
                LTS lts = new LTS(s);
                lts.Show();
                this.Close();
            } 
            else
                MessageBox.Show("Niepoprawne Dane");
        }
    }
}
