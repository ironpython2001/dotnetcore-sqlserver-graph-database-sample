using Microsoft.Data.SqlClient;
using System;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {


            var hostname = "127.0.0.1";
            var database = "graphdemo";

            string str = $"Server = {hostname}; Database = {database}; User Id = sa; Password = Mydev#333;";

            using (SqlConnection conn = new SqlConnection(str))
            {

               
                try
                {
                    conn.Open();


                    //Find Restaurants that John likes
                    var query1 = @"SELECT Restaurant.name FROM Person, likes, Restaurant
                            WHERE MATCH(Person - (likes)->Restaurant)
                            AND Person.name = @personname;";
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    cmd1.Parameters.AddWithValue("personname", "John");

                    SqlDataReader rdr1 = cmd1.ExecuteReader();
                    while (rdr1.Read())
                    {
                        Console.WriteLine(rdr1[0]);
                    }
                    rdr1.Close();


                    //Find people who like a restaurant in the same city they live in
                    var query2 = @"SELECT Person.name
                    FROM Person, likes, Restaurant, livesIn, City, locatedIn
                    WHERE MATCH(Person - (likes)->Restaurant - (locatedIn)->City AND Person - (livesIn)->City)";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    SqlDataReader rdr2 = cmd2.ExecuteReader();
                    while (rdr2.Read())
                    {
                        Console.WriteLine(rdr2[0]);
                    }
                    rdr2.Close();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();

            }
        }
    }
}
