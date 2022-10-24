using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleAdatbázis
{
    internal class Program
    {

        static void Main(string[] args)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "pizza";

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                Console.WriteLine("23.feladat:Hány házhoz szállítása volt az egyes futároknak?");
                command.CommandText = "SELECT fnev, COUNT(rendeles.fazon) FROM `futar`, `rendeles` WHERE rendeles.fazon=futar.fazon GROUP BY fnev;";
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {                      
                        string fnev = dr.GetString("fnev");
                        int CountR = dr.GetInt32("COUNT(rendeles.fazon)");
                        Console.WriteLine($" {fnev}.{CountR}");
                        
                    }
                }
                CConsole.WriteLine("24.feladat:A fogyasztás alapján mi a pizzák népszerűségi sorrendje?");
                command.CommandText = "SELECT pnev, SUM(db) FROM `pizza`, `tetel` WHERE tetel.pazon=pizza.pazon GROUP BY pnev ORDER BY SUM(db) DESC;";
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string pnev = dr.GetString("pnev");
                        int SumR = dr.GetInt32("SUM(db)");
                        Console.WriteLine($" {pnev}.{SumR}");

                    }
                }
                Console.WriteLine("25.feladat:A rendelések összértéke alapján mi a vevők sorrendje?");
                command.CommandText = "SELECT vnev, SUM(par*db) FROM `vevo`, `tetel`, `rendeles`, `pizza` WHERE tetel.razon=rendeles.razon AND rendeles.vazon=vevo.vazon AND tetel.pazon=pizza.pazon GROUP BY vnev ORDER BY SUM(par*db) DESC;";
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string vnev = dr.GetString("vnev");
                        int SumRD = dr.GetInt32("SUM(par*db)");
                        Console.WriteLine($" {vnev}.{SumRD}");

                    }
                }
                Console.WriteLine("26.feladat:Melyik a legdrágább pizza?");
                command.CommandText = "SELECT par, pnev FROM `pizza` ORDER BY par DESC LIMIT 1;";
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string pnev = dr.GetString("pnev");
                        int par = dr.GetInt32("par");
                        Console.WriteLine($" {par}.{pnev}");

                    }
                }
                Console.WriteLine("27.feladat:Ki szállította házhoz a legtöbb pizzát?");
                command.CommandText = "SELECT fnev, SUM(db) FROM `futar`, `rendeles`, `tetel` WHERE rendeles.razon=tetel.razon AND rendeles.fazon=futar.fazon GROUP BY fnev ORDER BY SUM(db) DESC LIMIT 1;";
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string fnev = dr.GetString("fnev");
                        int SUMdb1 = dr.GetInt32("SUM(db)");
                        Console.WriteLine($" {fnev}.{SUMdb1}");

                    }
                }
                Console.WriteLine("28.feladat:Ki ette a legtöbb pizzát?");
                command.CommandText = "SELECT vnev, SUM(db) FROM `vevo`, `rendeles`, `tetel` WHERE rendeles.razon=tetel.razon AND rendeles.vazon=vevo.vazon GROUP BY vnev ORDER BY SUM(db) DESC LIMIT 1;";
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string vnev = dr.GetString("vnev");
                        int SUMdb2 = dr.GetInt32("SUM(db)");
                        Console.WriteLine($" {vnev}.{SUMdb2}");

                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
            Console.WriteLine("\nProgram vége!");
            Console.Read();
        }
    }
}
