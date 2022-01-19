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
using FoodTruck;

namespace LangTruckStaff
{
    /// <summary>
    /// Logika interakcji dla klasy LTS.xaml
    /// </summary>
    public partial class LTS : Window
    {
        Session s;
        public LTS(Session s)
        {
            InitializeComponent();
            this.s = s;
            Permissions p = new Permissions();
            p = (Permissions)s.Permissions;
            EmployeeData.Content = $"{s.Name} {s.Surname}; {p}";

            if((int)p>0)
            {
                nz.Visibility = Visibility.Visible;
                if((int)p>1)
                {
                    pd.Visibility = Visibility.Visible;
                    zprac.Visibility = Visibility.Visible;
                    if((int)p>2)
                    {
                        zf.Visibility = Visibility.Visible;
                        zk.Visibility = Visibility.Visible;
                        zprod.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        enum Permissions
        {
            Zwolniony = 0,
            Sprzedawca = 1,
            Kierownik = 2,
            Administrator = 3
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void nz_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products = Product.GetProducts();
            centerText.Text = "";
            foreach(Product product in products)
                centerText.Text += $"{product.ID} | {product.Name} - {product.Price:c}\n{product.Description}\n";
        }
    }
}
