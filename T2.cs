using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class T2
    {
        const int ARRAY_SIZE = 30;
        static void Main(string[] args)
        {
            Console.Write("Give the numbers: ");
            String line = Console.ReadLine();
            String[] luvut = line.Split();
            int[] intit = new int[ARRAY_SIZE];
            double[] doublet = new double[ARRAY_SIZE];
            int i = 0;
            int j = 0;
            foreach (string luku in luvut)
            {
                double tulos = Double.Parse(luku);
                if (tulos - (int)tulos != 0)
                    doublet[i++] = tulos;
                else
                    intit[j++] = (int)tulos;
            }

            StreamWriter file = new System.IO.StreamWriter(@"c:\\MyTemp\\T2Int.txt");
            for (int luku = 0; luku < j; luku++ )
                file.WriteLine(intit[luku]);
            file.Close();

            file = new System.IO.StreamWriter(@"c:\\MyTemp\\T2Double.txt");
            for (int luku = 0; luku < i; luku++)
                file.WriteLine(doublet[luku]);
            file.Close();


            Console.ReadKey();
        }
    }
