using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livin_paris
{
    internal class Graphe
    {
        private Dictionary<Noeud, List<Noeud>> listeAdjacence;
        private int[,] matriceAdjacence;

        public Graphe(string fichier)
        {
            ChargerGraphe(fichier);
        }
        private void ChargerGraphe(string fichier)
        {
            try
            {
                string[] lignes = File.ReadAllLines(fichier);
                foreach (string ligne in lignes)
                {
                    if (ligne[0] != '$'){
                        string[] noeudsLigne = ligne.Split(' ');

                    }
                }

            } catch (Exception ex) {
                Console.WriteLine("erreur : " +ex.Message);
            }
        }
    }
}
