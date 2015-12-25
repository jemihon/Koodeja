using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Euler11
{

    class Program
    {
        const int GRIDSIZE = 20;
        const int ADJ_NUMBER = 4;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(@"C:\MyTemp\input.txt");
            int[,] numbers = initializeArray(input);

            int result = greatestProduct(numbers);

            Console.WriteLine(result);

            Console.ReadKey();
        }

        static int[,] initializeArray(string[] input)
        {
            int[,] numbers = new int[GRIDSIZE, GRIDSIZE];
            for (int i = 0; i < GRIDSIZE; i++)
            {
                string[] strnum = input[i].Split();
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    numbers[i, j] = Int32.Parse(strnum[j]);
                }
            }

            return numbers;
        }

        static int greatestProduct(int[,] numbers)
        {
            int c;
            int result = 0;
            if ((c = theProduct(numbers, "vertical")) > result)
                result = c;
            if ((c = theProduct(numbers, "horizontal")) > result)
                result = c;
            if ((c = theProduct(numbers, "leftDiag")) > result)
                result = c;
            if ((c = theProduct(numbers, "rightDiag")) > result)
                result = c;

            return result;
        }

        static int theProduct(int[,] numbers, string direction)
        {
            int result = 0;
            int temp = 1;
            for (int i = 0; (direction == "leftDiag" || direction == "rightDiag") ? i <= GRIDSIZE - ADJ_NUMBER : i < GRIDSIZE; i++)
            {
                for (int j = (direction == "rightDiag") ? GRIDSIZE - 1 : 0; (direction == "rightDiag") ? j >= ADJ_NUMBER - 1 : j <= GRIDSIZE - ADJ_NUMBER; j += (direction == "rightDiag") ? -1 : 1)
                {
                    for (int k = 0; k < ADJ_NUMBER; k++)
                    {
                        switch(direction)
                        {
                            case "vertical":
                                temp *= numbers[j + k, i];
                                break;
                            case "horizontal":
                                temp *= numbers[i, j + k];
                                break;
                            case "leftDiag":
                                temp *= numbers[i + k, j + k];
                                break;
                            case "rightDiag":
                                temp *= numbers[i + k, j - k];
                                break;
                            default:
                                result = -1;
                                break;

                        }
                        
                    }
                    if (temp > result)
                        result = temp;
                    temp = 1;
                }
            }

            return result;
        }
    }
}
