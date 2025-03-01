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
        private Dictionary<int, List<Noeud>> listeAdjacence;
        private int[,] matriceAdjacence;
        private int nbNoeuds;

        public Graphe(string fichier)
        {
            this.listeAdjacence = new Dictionary<int, List<Noeud>>();
            this.nbNoeuds = 0;
            ChargerGraphe(fichier);

            DFS(new Noeud(1));
            BFS(new Noeud(1));
        }
        private void ChargerGraphe(string fichier)
        {
            /*try
            {*/
            string[] lignes = File.ReadAllLines(fichier);
            foreach (string ligne in lignes)
            {
                if (ligne[0] != '%')
                {
                    string[] noeudsLigne = ligne.Split(' ');

                    if (noeudsLigne.Length == 3)
                    {
                        this.nbNoeuds = int.Parse(noeudsLigne[0]);
                        for (int i = 1; i <= nbNoeuds; i++)
                        {
                            this.listeAdjacence.Add(i, new List<Noeud>());
                        }
                    }
                    if (noeudsLigne.Length == 2)
                    {
                        
                        Noeud n1 = new Noeud(int.Parse(noeudsLigne[0]));
                        Noeud n2 = new Noeud(int.Parse(noeudsLigne[1]));

                        bool verif = true;
                        foreach (Noeud noeud in this.listeAdjacence[n1.Id])
                        {
                            if (noeud.isEqual(n2))
                            {
                                verif= false;
                            }
                        }

                        if (verif == true)
                        {
                            this.listeAdjacence[n1.Id].Add(n2);
                        }

                        verif = true;
                        foreach (Noeud noeud in this.listeAdjacence[n2.Id])
                        {
                            if (noeud.isEqual(n1))
                            {
                                verif = false;
                            }
                        }
                        if (verif == true)
                        {
                            this.listeAdjacence[n2.Id].Add(n1);
                        }

                        Lien l = new Lien(n1, n2);
                    }
                }
            }

            for(int i = 1; i <= this.nbNoeuds; i++)
            {
                Console.Write("- " + i + " : ");
                foreach(var n in this.listeAdjacence[i])
                {
                    Console.Write(n.Id+", ");
                }
                Console.WriteLine();
            }

            /*} catch (Exception ex) {
                Console.WriteLine("erreur : " +ex.Message);
            }*/



            this.matriceAdjacence = new int[this.nbNoeuds, this.nbNoeuds];
            foreach (string ligne in lignes)
            {
                if (ligne[0] != '%')
                {
                    string[] noeudsLigne = ligne.Split(' ');
                    if (noeudsLigne.Length == 2)
                    {
                        matriceAdjacence[int.Parse(noeudsLigne[0])-1, int.Parse(noeudsLigne[1]) - 1]++;
                        matriceAdjacence[int.Parse(noeudsLigne[1])-1, int.Parse(noeudsLigne[0])-1]++;
                    }
                }
            }
            for (int i = 0; i < matriceAdjacence.GetLength(0); i++)
            {
                for (int j = 0; j < matriceAdjacence.GetLength(0); j++)
                {
                    Console.Write(matriceAdjacence[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void DFS(Noeud depart) //Parcours en profondeur d'abord
        {
            Console.Write("DFS : ");
            int departIndex = depart.Id;
            List<int> dejaVisite = new List<int>();
            Stack<int> pile = new Stack<int>();
            pile.Push(departIndex);

            while (pile.Count > 0)
            {
                int noeudActuel = pile.Pop();
                if (!dejaVisite.Contains(noeudActuel))
                {
                    dejaVisite.Add(noeudActuel);
                    Console.Write(noeudActuel + " ");

                    foreach (Noeud voisin in listeAdjacence[noeudActuel])
                    {
                        if (!dejaVisite.Contains(voisin.Id))
                        {
                            pile.Push(voisin.Id);
                        }
                    }
                }
            }
            Console.WriteLine();
        }

        public void BFS(Noeud depart) //Parcours en largeur d'abord
        {
            //List <Noeud> noeudsParcourus = new List<Noeud>();

            Console.Write("BFS : ");
            int departIndex = depart.Id;
            List<int> dejaVisite = new List<int>();
            Queue<int> file = new Queue<int>();
            file.Enqueue(departIndex);
            dejaVisite.Add(departIndex);

            while (file.Count > 0)
            {
                int noeudActuel = file.Dequeue();
                Console.Write(noeudActuel + " ");

                foreach (Noeud voisin in listeAdjacence[noeudActuel])
                {
                    if (!dejaVisite.Contains(voisin.Id))
                    {
                        dejaVisite.Add(voisin.Id);
                        file.Enqueue(voisin.Id);
                    }
                }
            }
            Console.WriteLine();
        }

        /*public bool EstConnexe()
        {
            bool estConnexe = true;

            for (int i = 1; i<this.nbNoeuds; i++)
            {
                noeudsParcourus = new DFS(new Noeud(i));
                if ()
            }
            


            return estConnexe;
        }*/
    }
}