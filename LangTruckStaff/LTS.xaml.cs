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
        private static List<Product> products = new List<Product>();
        private static Dictionary<Product, int> basket = new Dictionary<Product, int>();
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
            basket.Clear();
            Basket.Items.Clear();
            products = Product.GetProducts();
            centerText.Items.Clear();
            foreach(Product product in products)
                if(product.IsAvailable)
                    centerText.Items.Add($"{product.ID} | {product.Name} - {product.Price:c}\n{product.Description}");
            order.Visibility = Visibility.Visible;
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void centerText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Product product = products[centerText.SelectedIndex];
                centerText.UnselectAll();
                try
                {
                    basket.Add(product, 1);
                }
                catch
                {
                    basket[product]++;
                }
                Basket.Items.Clear();
                decimal total = 0m;
                foreach (KeyValuePair<Product, int> p in basket)
                {
                    Basket.Items.Add($"{p.Key.Name} x {p.Value}");
                    total += p.Key.Price*p.Value;
                }
                summary.Content = $"{total:c}";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Basket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int a = Basket.SelectedIndex;
            int b = 0;
            foreach (KeyValuePair<Product, int> p in basket)
            {
                if (a==b++)
                {
                    if (p.Value==1)
                        basket.Remove(p.Key);
                    else
                        basket[p.Key]--;
                    break;
                }
            }
            decimal total = 0m;
            Basket.UnselectAll();
            Basket.Items.Clear();
            foreach (KeyValuePair<Product, int> p in basket)
            {
                Basket.Items.Add($"{p.Key.Name} x {p.Value}");
                total += p.Key.Price*p.Value;
            }
            summary.Content = $"{total:c}";
        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
