using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck
{
    public class Order
    {
        public decimal Summary { get; set; }
        public string? ClientID { get; set; } = "NULL";
        public string FoodTruckID { get; set; }

        public Order(Dictionary<Product, int> basket, string FoodTruckID)
        {
            foreach (KeyValuePair<Product, int> p in basket)
                Summary += p.Key.Price*p.Value;
            this.FoodTruckID = FoodTruckID;
        }

        public bool ScanApp(string code)
        {
            bool isCorrectCode = Query.SQLBoolQuery("SELECT COUNT(*) FROM Klienci WHERE ID = " + code);
            if(isCorrectCode)
                ClientID = code;
            //TODO kupony
            return isCorrectCode;
        }

        public void Pay(Dictionary<Product, int> products)
        {
            Decimal.TryParse(Convert.ToString((double)Summary), out decimal total);
            Query.SQLQuery(@$"EXECUTE DodajZamowienie {total.ToString().Replace(',', '.')}, {ClientID}, '{FoodTruckID}'");

            string orderID = Query.SQLArrayQuery("SELECT TOP 1 ID FROM Zamowienia ORDER BY ID DESC")[0];
            foreach (KeyValuePair<Product, int> p in products)
            {
                Query.SQLQuery("INSERT INTO ZawartoscZamowienia (ZamowienieID, ProduktID, Ilosc)" + 
                              $"VALUES ({orderID}, {p.Key.ID}, {p.Value})");
            }
        }
    }
}
