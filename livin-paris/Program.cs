using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace livin_paris
{
    internal class Program
    {

        public static Dictionary<(int, int), double> LoadMatrix(string filePath, out int rows, out int cols)
        {
            var matrix = new Dictionary<(int, int), double>();
            rows = cols = 0;
            bool headerProcessed = false;

            foreach (var line in File.ReadLines(filePath))
            {
                // Ignorer les commentaires (%)
                if (line.StartsWith("%"))
                    continue;

                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                // Première ligne sans commentaire = dimensions de la matrice
                if (!headerProcessed)
                {
                    rows = int.Parse(parts[0]);
                    cols = int.Parse(parts[1]);
                    headerProcessed = true;
                }
                else
                {
                    int row = int.Parse(parts[0]) - 1;  // Indexation commence à 0
                    int col = int.Parse(parts[1]) - 1;
                    double value = double.Parse(parts[2], CultureInfo.InvariantCulture);

                    matrix[(row, col)] = value;
                }
            }

            return matrix;
        }
        static void Main(string[] args)
        {
            string filePath = "../../../soc-karate.mtx"; // Remplace avec ton chemin de fichier

            var matrix = LoadMatrix(filePath, out int rows, out int cols);

            Console.WriteLine($"Matrice {rows}x{cols} chargée avec {matrix.Count} valeurs non nulles.");

            foreach (var entry in matrix)
            {
                Console.WriteLine($"({entry.Key.Item1}, {entry.Key.Item2}) = {entry.Value}");
            }
        }
    }
}
