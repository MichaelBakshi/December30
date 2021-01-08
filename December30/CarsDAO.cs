using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace December30
{
    static class CarsDAO
    {
        public static void GetAllCars()
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source = C:\\USERS\\MICHAEL\\DOCUMENTS\\December30Test.db; Version = 3;"))
            {
                conn.Open();

                using (SQLiteCommand select_query = new SQLiteCommand("SELECT * from Cars", conn))
                {
                    using (SQLiteDataReader reader = select_query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]}, {reader["Manufacturer"]}, {reader["Model"]}, {reader["Year"]}");
                        }
                    }
                }
            }
        }

        static int ExecuteNonQuery(string query)
        {
            int result = 0;

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                using (cmd.Connection = new SQLiteConnection(@"Data Source = C:\\USERS\\MICHAEL\\DOCUMENTS\\December30Test.db; Version = 3;"))
                {
                    cmd.Connection.Open();

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = query;

                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }
        public static void AddCar(Car c)
        {
            ExecuteNonQuery("INSERT INTO CARS(Manufacturer, Model, Year)" +
            $"VALUES('{c.Manufacturer}', '{c.Model}', {c.Year});");

        }

        public static void UpdateCar(Car c, int id)
        {
            int result = ExecuteNonQuery(
                $"UPDATE CARS SET Manufacturer='{c.Manufacturer}', Model='{c.Model}', Year={c.Year} " +
                $"WHERE ID={id}");
        }

        public static List<Car> SelectCarsByManufacturer(string manufacturer)
        {
            List<Car> listOfCars = new List<Car>();
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                using (cmd.Connection = new SQLiteConnection(@"Data Source = C:\\USERS\\MICHAEL\\DOCUMENTS\\December30Test.db; Version = 3;"))
                {
                    cmd.Connection.Open();

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"Select * from Cars WHERE Manufacturer='{manufacturer}'";

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Car c = new Car
                        {
                            ID = (Int64)reader["ID"],
                            Manufacturer = reader["Manufacturer"].ToString(),
                            Model = reader["Model"].ToString(),
                            Year = (Int64)reader["Year"]
                        };

                        listOfCars.Add(c);
                    }

                }
            }

            return listOfCars;
        }

        public static void GetAllCarsWithTests()
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source = C:\\USERS\\MICHAEL\\DOCUMENTS\\December30Test.db; Version = 3;"))
            {
                conn.Open();

                using (SQLiteCommand select_query = new SQLiteCommand("SELECT * from Cars JOIN Tests on Cars.ID=Tests.Car_ID", conn))
                {
                    using (SQLiteDataReader reader = select_query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]}, {reader["Manufacturer"]}, {reader["Model"]}, {reader["Year"]}, {reader["Car_ID"]}, {reader["IsPassed"]}, {reader["Date"]}");
                        }
                    }
                }
            }
        }

        public static  int RemoveCar(int id)
        {
            int result = ExecuteNonQuery($"DELETE FROM CARS WHERE ID={id}");
            return result;
        }
    }
}
