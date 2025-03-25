using System;
using System.IO;
using System.Collections.Generic;
using livin_paris;
using MySql.Data.MySqlClient;


class Program
{
    static void Main()
    {
        // string filePath = "../../../soc-karate.mtx";
        // new Graphe(filePath);


        string connectionString = "Server=localhost;Database=film;User ID=root;Password=root;SslMode=none;";
        MySqlConnection conn = ConnexionSQL(connectionString);



        Console.WriteLine("test starting ::");

        //test requetes sql
        string requete = "SELECT titre,annee FROM film WHERE pays = 'USA';";
        MySqlCommand command1 = conn.CreateCommand();
        command1.CommandText = requete;

        MySqlDataReader reader = command1.ExecuteReader();

        string[] valueString = new string[reader.FieldCount];

        while (reader.Read())
        {
            string titre = (string)reader["titre"];
            int annee = (int)reader["annee"];
            Console.WriteLine(titre+ "  "+ annee);

            for (int i = 0; i<reader.FieldCount; i++)
            {
                valueString[i] = reader.GetValue(i).ToString();
                Console.Write(valueString[i] + " , ");
            }
            Console.WriteLine();
        }

        reader.Close();
        command1.Dispose();
    }


    static MySqlConnection ConnexionSQL(string connectionString)
    {
        MySqlConnection conn = new MySqlConnection(connectionString);
        try
        {
            conn.Open();
            Console.WriteLine("Connexion réussie !");

        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur : " + ex.Message);
        }
        
        return conn;
    }

}
