using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Données initiales :
            int[] p = { 100 , 105 , 110 , 115 , 120 , 125 ,130 ,0};

            //les tableaux v,r,rcumul ne commence qu'a l'index 1. La valeur a l'index zero ne sera jamais utilisée
            int[] v = {-1, 50, 30, 20, 10, 10, 5 , 0  };
            int[] r = {-1, 30, 40, 50, 70, 90, 100, 100};
            int[] rcumul = {-1, 30, 70, 120, 190, 280, 380, 480 };
     
            //matrice d'adjacence
            int[,] matrice = new int[8,8];

            //initialisation (calcul des valeures des arcs)
            for (int i=0;i<8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j > i)
                    {
                        matrice[i,j] = p[i] + rcumul[j-i] - v[j-i];
                    }else{
                        matrice[i,j] = -1;
                    }
                }
            }

            displayMatrice(matrice);

            Bellman_ss_circuit(matrice, 0);


        }//end main

        static void displayMatrice(int[,] matrice)
        {
            int x = matrice.GetLength(0);
            int y = matrice.GetLength(1);

            Console.WriteLine("┌" + string.Concat(Enumerable.Repeat("────┬", x-1)) + "────┐");

            for (int i=0; i<x; i++)
            {
                Console.Write("│");
                for (int j =0;j<y; j++)
                {
                    Console.Write(matrice[i, j].ToString().PadLeft(4) + "│");
                }
                Console.WriteLine();
                
            }
            Console.WriteLine("└" + string.Concat(Enumerable.Repeat("────┴", x-1)) + "────┘");

        }//end displayMatrice

        private struct sommet
        {
            public int id;
            public int d;
            public int previous;
        }

        private class som
        {
            public int id;
            public int d;
            public som previous;

            public som(int id)
            {
                this.id = id;
                this.d = -1;
                this.previous = null;
            }
        }

        /*
         * Implemenation de bellman sans circuit, sur une matrice d'adjacence.
         * TODO : 
         * gerer les valuation d'arc negative
         * gerer le point de depart
         * utiliser une classe sommet, plutot qu'une structure, pour stocker la signature via un pointeur.
         */
        static void Bellman_ss_circuit(int[,]matrice, int depart)
        {
            sommet[] bellman = new sommet[matrice.GetLength(0)];


            //init
            for (int i=0; i<bellman.Length; i++)
            {
                bellman[i].id = i;
                bellman[i].d = -1;
                bellman[i].previous = -1;
            }

            bellman[0].d = 0;

            for (int s = 0; s<bellman.Length; s++)
            {
                for (int next = 0; next < matrice.GetLength(1); next++)
                {
                    if (matrice[s, next] < 0) { continue; }
                    if (bellman[next].d == -1 || bellman[next].d > bellman[s].d +  matrice[s,next])
                    {
                        bellman[next].d = bellman[s].d + matrice[s, next];
                        bellman[next].previous = s;
                    }
                }
            }

            //display result
            Console.WriteLine("│" + "s".PadLeft(2) + " │ " + "d[s]".ToString().PadLeft(4) + " │ " + "prev".ToString().PadLeft(4) + "│");
            for (int s=0;s<bellman.Length; s++)
            {
                Console.WriteLine("│" + s.ToString().PadLeft(2) + " │ " + bellman[s].d.ToString().PadLeft(4) + " │ " + bellman[s].previous.ToString().PadLeft(4)+ "│");
            }


            
        }
    }


}
