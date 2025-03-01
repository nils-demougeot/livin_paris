using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SkiaSharp;
using System.Diagnostics;
//using GrapheVisualizer;

namespace livin_paris
{
    internal class Graphe
    {
        public List<Noeud> Noeuds_Graphe;
        public List<Lien> Liens_Graphe;
        private Dictionary<int, List<Noeud>> listeAdjacence;
        private int[,] matriceAdjacence;
        private int nbNoeuds;

        public Graphe(string fichier)
        {
            this.Noeuds_Graphe = new List<Noeud>();
            this.Liens_Graphe = new List<Lien>();
            this.nbNoeuds = 0;
            ChargerGrapheBis(fichier);

            this.listeAdjacence = new Dictionary<int, List<Noeud>>();
            this.nbNoeuds = 0;
            ChargerGraphe(fichier);

            List<Noeud> dfs = DFS(new Noeud(1));
            Console.Write("DFS : ");
            AfficherListe(dfs);
            

            List<Noeud> bfs = BFS(new Noeud(1));
            Console.Write("BFS : ");
            AfficherListe(bfs);


            Console.WriteLine(this.EstConnexe());

            this.ContientCicuits();
        }

        public List<Noeud> GetNoeuds()
        {
            return Noeuds_Graphe;
        }
        public List<Lien> GetLiens()
        {
            return Liens_Graphe;
        }

        private void ChargerGrapheBis(string fichier)
        {
            try
            {
                string[] lignes = File.ReadAllLines(fichier);
                foreach (string ligne in lignes)
                {
                    if (ligne[0] != '%')
                    {
                        string[] noeudsLigne = ligne.Split(' ');

                        if (noeudsLigne.Length == 3)
                        {
                            this.nbNoeuds = int.Parse(noeudsLigne[0]);
                        }
                        if (noeudsLigne.Length == 2)
                        {
                            Noeud n1 = new Noeud(int.Parse(noeudsLigne[0]));
                            Noeud n2 = new Noeud(int.Parse(noeudsLigne[1]));

                            if (!Noeuds_Graphe.Any(n => n.Id == n1.Id))
                            {
                                Noeuds_Graphe.Add(n1);
                            }
                            if (!Noeuds_Graphe.Any(n => n.Id == n2.Id))
                            {
                                Noeuds_Graphe.Add(n2);
                            }
                            Liens_Graphe.Add(new Lien(n1, n2));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur : " + ex.Message);
            }
        }

        private void ChargerGraphe(string fichier)
        {
            try
            {
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
                                    verif = false;
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

                for (int i = 1; i <= this.nbNoeuds; i++)
                {
                    Console.Write("- " + i + " : ");
                    foreach (var n in this.listeAdjacence[i])
                    {
                        Console.Write(n.Id + ", ");
                    }
                    Console.WriteLine();
                }

                this.matriceAdjacence = new int[this.nbNoeuds, this.nbNoeuds];
                foreach (string ligne in lignes)
                {
                    if (ligne[0] != '%')
                    {
                        string[] noeudsLigne = ligne.Split(' ');
                        if (noeudsLigne.Length == 2)
                        {
                            matriceAdjacence[int.Parse(noeudsLigne[0]) - 1, int.Parse(noeudsLigne[1]) - 1]++;
                            matriceAdjacence[int.Parse(noeudsLigne[1]) - 1, int.Parse(noeudsLigne[0]) - 1]++;
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
            catch (Exception ex)
            {
                Console.WriteLine("erreur : " + ex.Message);
            }
        }

        /// <summary>
        /// Algorithme de parcours en profondeur d'abord (DFS) à partir du point de départ en entrée.
        /// </summary>
        /// <param name="depart">Noeud de départ de l'algorithme</param>
        /// <returns></returns>
        public List<Noeud> DFS(Noeud depart) //Parcours en profondeur d'abord
        {
            List<Noeud> noeudsParcourus = new List<Noeud>();
            Stack<Noeud> pile = new Stack<Noeud>();
            pile.Push(depart);

            while (pile.Count > 0)
            {
                Noeud noeudActuel = pile.Pop();
                bool dejaVisite = false;
                for (int i = 0; i < noeudsParcourus.Count; i++)
                {
                    if (noeudsParcourus[i].isEqual(noeudActuel))
                    {
                        dejaVisite = true;
                    }
                }
                if (dejaVisite == false)
                {
                    noeudsParcourus.Add(noeudActuel);

                    foreach (Noeud voisin in listeAdjacence[noeudActuel.Id])
                    {
                        bool dejaVisiteVoisin = false;
                        for (int j = 0; j < noeudsParcourus.Count; j++)
                        {
                            if (noeudsParcourus[j].isEqual(voisin))
                            {
                                dejaVisiteVoisin = true;
                            }
                        }
                        if (dejaVisiteVoisin == false)
                        {
                            pile.Push(voisin);
                        }
                    }
                }
            }
            return noeudsParcourus;
        }

        /// <summary>
        /// Algorithme de parcours en largeur d'abord (BFS) à partir du point de départ en entrée.
        /// </summary>
        /// <param name="depart">Noeud de départ de l'algorithme</param>
        /// <returns></returns>
        public List<Noeud> BFS(Noeud depart)
        {
            List <Noeud> noeudsParcourus = new List<Noeud>();
            Queue<Noeud> file = new Queue<Noeud>();
            file.Enqueue(depart);
            noeudsParcourus.Add(depart);

            while (file.Count > 0)
            {
                Noeud noeudActuel = file.Dequeue();

                foreach (Noeud voisin in listeAdjacence[noeudActuel.Id])
                {
                    bool dejaVisite = false;
                    for (int i = 0; i < noeudsParcourus.Count; i++)
                    {
                        if (noeudsParcourus[i].isEqual(voisin))
                        {
                            dejaVisite = true;
                        }
                    }
                    if (dejaVisite == false)
                    {
                        noeudsParcourus.Add(voisin);
                        file.Enqueue(voisin);
                    }
                }
            }
            return noeudsParcourus;
        }

        /// <summary>
        /// Indique si le graphe courrant est connexe.
        /// </summary>
        /// <returns></returns>
        public bool EstConnexe()
        {
            List<Noeud> noeudsParcourus = DFS(new Noeud(1));

            if (noeudsParcourus.Count == this.nbNoeuds)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Vérifie si le graphe contient des circuits (cycles) et les affiches dans la console.
        /// La methode se base sur l'algorithme du DFS (parcours en profondeur d'abord) mais est modifiée 
        /// pour essayer depuis tous les points du graphe et s'arrête dès qu'un cycle a été trouvé pour en 
        /// chercher un autre.
        /// </summary>
        /// <returns></returns>
        public bool ContientCicuits()
        {
            bool contientCicuits = false;
            for (int n = 1; n<= this.nbNoeuds; n++) {
                Noeud noeudDepart = new Noeud(n);

                List<Noeud> noeudsParcourus = new List<Noeud>();
                Stack<Noeud> pile = new Stack<Noeud>();
                Dictionary<int, int> parent = new Dictionary<int, int>();

                pile.Push(noeudDepart);
                parent[noeudDepart.Id] = -1;

                bool boucle = false;
                while (pile.Count > 0 && boucle == false)
                {
                    Noeud noeudActuel = pile.Pop();

                    bool dejaVisite = false;
                    for (int i = 0; i < noeudsParcourus.Count; i++)
                    {
                        if (noeudsParcourus[i].isEqual(noeudActuel))
                        {
                            dejaVisite = true;
                        }
                    }
                    if (dejaVisite == false)
                    {
                        noeudsParcourus.Add(noeudActuel);

                        foreach (Noeud voisin in listeAdjacence[noeudActuel.Id])
                        {
                            bool dejaVisiteVoisin = false;
                            for (int j = 0; j < noeudsParcourus.Count; j++)
                            {
                                if (noeudsParcourus[j].isEqual(voisin))
                                {
                                    dejaVisiteVoisin = true;
                                }
                            }
                            if (dejaVisiteVoisin == false)
                            {
                                parent[voisin.Id] = noeudActuel.Id;
                                pile.Push(voisin);
                            }
                            else if (parent[noeudActuel.Id] != voisin.Id)
                            {
                                contientCicuits = true;
                                boucle = true;
                            }
                        }
                    }
                }
                Console.Write("Cycle " + n + " : " );
                AfficherListe(noeudsParcourus);
            }
            return contientCicuits;
        }

        /// <summary>
        /// Permet d'afficher dans la console une liste de Noeuds.
        /// </summary>
        /// <param name="liste">Liste de noeuds à afficher</param>
        private void AfficherListe(List<Noeud> liste)
        {
            for (int i = 0; i < liste.Count; i++)
            {
                Console.Write(liste[i].Id + ", ");
            }
            Console.WriteLine();
        }
    }
}

/*namespace GrapheVisualizer
{
    public class VisualiseurGraphe
    {
        /// <summary>
        /// Génère et sauvegarde une image représentant un graphe à partir d'une liste de nœuds et de liens.
        /// </summary>
        /// <param name="graphe">Instance du graphe contenant les nœuds et les liens</param>
        /// <param name="cheminSauvegarde">Adresse où le fichier image sera sauvegardé</param>
        /// <param name="largeurImage">Largeur en pixels de l'image</param>
        /// <param name="hauteurImage">Hauteur en pixels de l'image</param>
        public void CreerGraphe(Graphe graphe, string cheminSauvegarde, int largeurImage = 800, int hauteurImage = 600)
        {
            if (graphe.GetNoeuds().Count == 0)
            {
                throw new ArgumentException("Le graphe est vide.");
            }

            using var bitmap = new SKBitmap(largeurImage, hauteurImage);
            using var canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.White);

            Dictionary<int, (float X, float Y)> positions = CalculerPositions(graphe, largeurImage, hauteurImage);

            // Dessiner les liens en premier
            using var paintLien = new SKPaint
            {
                Color = SKColors.Black,
                StrokeWidth = 2,
                IsAntialias = true
            };

            foreach (var lien in graphe.GetLiens())
            {
                var pos1 = positions[lien.Noeud1.Id];
                var pos2 = positions[lien.Noeud2.Id];
                canvas.DrawLine(pos1.X, pos1.Y, pos2.X, pos2.Y, paintLien);
            }

            // Dessiner les nœuds par-dessus
            using var paintNoeud = new SKPaint
            {
                Color = SKColors.Blue,
                IsAntialias = true
            };
            using var paintTexte = new SKPaint
            {
                Color = SKColors.White,
                TextSize = 20,
                IsAntialias = true
            };

            foreach (var noeud in graphe.GetNoeuds())
            {
                var pos = positions[noeud.Id];
                canvas.DrawCircle(pos.X, pos.Y, 25, paintNoeud);
                canvas.DrawText(noeud.Id.ToString(), pos.X - 10, pos.Y + 5, paintTexte);
            }

            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(cheminSauvegarde);
            data.SaveTo(stream);

            Console.WriteLine("Graphe sauvegardé à : " + Path.GetFullPath(cheminSauvegarde));
        }

        /// <summary>
        /// Calcule les positions des nœuds pour une répartition circulaire dans l'image.
        /// </summary>
        private Dictionary<int, (float X, float Y)> CalculerPositions(Graphe graphe, int largeur, int hauteur)
        {
            Dictionary<int, (float, float)> positions = new Dictionary<int, (float, float)>();
            int rayon = Math.Min(largeur, hauteur) / 3;
            int centreX = largeur / 2;
            int centreY = hauteur / 2;
            int totalNoeuds = graphe.GetNoeuds().Count;

            for (int i = 0; i < totalNoeuds; i++)
            {
                double angle = (2 * Math.PI * i) / totalNoeuds;
                int id = graphe.GetNoeuds()[i].Id;
                positions[id] = ((float)(centreX + rayon * Math.Cos(angle)), (float)(centreY + rayon * Math.Sin(angle)));
            }

            return positions;
        }

        /// <summary>
        /// Affiche le graphe en ouvrant l'image après sa création.
        /// </summary>
        public static void AfficherGraphe(Graphe graphe)
        {
            string cheminImage = "Graphe.png";
            VisualiseurGraphe visualiseur = new VisualiseurGraphe();
            visualiseur.CreerGraphe(graphe, cheminImage);

            Process.Start(new ProcessStartInfo(cheminImage) { UseShellExecute = true });
        }
    }
}*/