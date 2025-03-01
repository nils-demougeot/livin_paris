using System;
using System.IO;
using System.Collections.Generic;
using livin_paris;

class Program
{
    static void Main()
    {
        string filePath = "../../../soc-karate.mtx";
        //string filePath = "C:\\Users\\Nils\\Documents\\Projet liv\'in paris\\Association-soc-karate\\soc-karate.mtx";
        new Graphe(filePath);
        /*if (!File.Exists(filePath))
        {
            Console.WriteLine("Fichier introuvable.");
            return;
        }

        try
        {
            List<(int, int)> edges = new List<(int, int)>();
            int numRows = 0, numCols = 0, numEdges = 0;
            bool dimensionsRead = false;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    // Ignorer les lignes de commentaire
                    if (line.StartsWith("%") || line.Length == 0)
                        continue;

                    // Lire les dimensions de la matrice (nombre de nœuds + arêtes)
                    if (!dimensionsRead)
                    {
                        string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 3)
                        {
                            numRows = int.Parse(parts[0]);
                            numCols = int.Parse(parts[1]);
                            numEdges = int.Parse(parts[2]);
                            dimensionsRead = true;
                        }
                        continue;
                    }

                    // Lire les connexions entre les nœuds (row, col)
                    string[] data = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 2)
                    {
                        int row = int.Parse(data[0]);
                        int col = int.Parse(data[1]);

                        edges.Add((row, col));
                        // Comme la matrice est symétrique, ajouter aussi (col, row)
                        edges.Add((col, row));
                    }
                }
            }

            Console.WriteLine($"Graphe chargé : {numRows} sommets, {numEdges} arêtes");
            Console.WriteLine("Exemples de connexions :");
            for (int i = 0; i < Math.Min(edges.Count, 10); i++)
            {
                Console.WriteLine($"{edges[i].Item1} <-> {edges[i].Item2}");
            }

            // Ici, tu peux utiliser la liste "edges" pour construire un graphe
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de la lecture du fichier : " + ex.Message);
        }*/
    }
}
