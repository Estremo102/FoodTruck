using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck
{
    public record Product
    {
        public int ID { get; }
        public string Name { get; }
        public decimal Price { get; }
        public string Description { get; }
        public bool IsAvailable { get; }

        public Product(int ID)
        {
            this.ID = ID;
            string[] data = Query.SQLArrayQuery($"SELECT Nazwa, Cena, Opis, Dostepny FROM Produkty WHERE ID = {ID}");
            Name = data[0];
            Price = Convert.ToDecimal(data[1]);
            Description = data[2];
            IsAvailable = Convert.ToBoolean(data[3]);
        }

        public Product(string name, decimal price, string desctription, bool isAvailable)
        {
            Name = name;
            Price = price;
            Description = desctription;
            IsAvailable = isAvailable;
        }

        public void AddToDatabase() => Query.SQLQuery($"INSERT INTO Produkty (Nazwa, Cena, Opis) VALUES ('{Name}', {Price}, '{Description}')");

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string[,] IDs = Query.SQL2DArrayQuery("SELECT ID FROM PRODUKTY");
            foreach (string ID in IDs)
                products.Add(new Product(Convert.ToInt32(ID)));
            return products;
        }
    }
}
