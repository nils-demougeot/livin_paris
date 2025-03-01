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
        private int nbNoeuds;

        public Graphe(string fichier)
        {
            this.listeAdjacence = new Dictionary<Noeud, List<Noeud>>();
            this.nbNoeuds = 0;
            ChargerGraphe(fichier);
        }
        private void ChargerGraphe(string fichier)
        {
            /*try
            {*/
                string[] lignes = File.ReadAllLines(fichier);
                foreach (string ligne in lignes)
                {
                    if (ligne[0] != '%'){
                        string[] noeudsLigne = ligne.Split(' ');

                    if (noeudsLigne.Length == 3)
                    {
                        nbNoeuds = int.Parse(noeudsLigne[0]);
                        for (int i = 1; i <= nbNoeuds; i++)
                        {
                            this.listeAdjacence.Add(new Noeud(i), new List<Noeud>());
                        }
                    }
                    if (noeudsLigne.Length == 2)
                    {  
                        Noeud n1 = new Noeud(int.Parse(noeudsLigne[0]));
                        Noeud n2 = new Noeud(int.Parse(noeudsLigne[1]));
                        /*bool test_n1 = false;
                        bool test_n2 = false;
                        for (int i = 0, i < listeAdjacence.Count, i++)
                        {
                            if (noeud.isEqual(n1))
                            {
                                test_n1 = true;
                            }
                            if (noeud.isEqual(n2))
                            {
                                test_n2 = true;
                            }
                        }

                        if (!test_n1)
                        {
                            this.listeAdjacence.Add(n1, new List<Noeud>());
                            this.nbNoeuds++;
                        }

                        if (!test_n2)
                        {
                            this.listeAdjacence.Add(n2, new List<Noeud>());
                            this.nbNoeuds++;
                        }*/

                        /*if (!this.listeAdjacence.ContainsKey(n1))
                        {
                            this.listeAdjacence.Add(n1, new List<Noeud>());
                            this.nbNoeuds++;
                        }

                        if (!this.listeAdjacence.ContainsKey(n2))
                        {
                            this.listeAdjacence.Add(n2, new List<Noeud>());
                            this.nbNoeuds++;
                        }*/

                        if (!this.listeAdjacence[n1].Contains(n2))
                            {
                                this.listeAdjacence[n1].Add(n2);
                                Console.WriteLine(ligne+" : "+n2.Id);
                            }

                            
                        if (!this.listeAdjacence[n2].Contains(n1))
                        {
                            this.listeAdjacence[n2].Add(n1);
                            Console.WriteLine(ligne + " : " + n1.Id);
                        }

                            Lien l = new Lien(n1, n2);
                        }
                    }
                }

            /*} catch (Exception ex) {
                Console.WriteLine("erreur : " +ex.Message);
            }*/
        }
    }
}
