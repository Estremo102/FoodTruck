namespace FoodTruck
{
    public class Session
    {
        public bool LoggedIn { get; private set; } = false;
        public string Login { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int Permissions { get; private set; }
        public string LicensePlate { get; private set; }

        public bool LogIn(string login, string password) => LoggedIn = Query.SQLBoolQuery($"SELECT [dbo].[Zaloguj]('{login}', '{password}')");

        public void GetEmployeeData(string employee)
        {
            string[] data = Query.SQLArrayQuery($"SELECT Imie, Nazwisko, Uprawnienia FROM Personel WHERE [Login] = '{employee}'");
            Name = data[0];
            Surname = data[1];
            Permissions = Convert.ToInt32(data[2]);
        }
    }
}