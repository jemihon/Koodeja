using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/// @author Jere
/// @version ..2015
public class demo8tb1
{
    /// <summary>
    /// Yksinkertainen kolikonheittopeli.
    /// </summary>
    public static void Main()
    {
        Alku:
            Console.Write("Montako perättäistä klaavaa halutaan? > ");
        int klaava = int.Parse(Console.ReadLine());
        Console.Write("Montako kertaa haluat tuloksen? > ");
        int maara = int.Parse(Console.ReadLine());
        Console.WriteLine("Tulos: " + klaava + " perättäistä klaavaa " + maara + " kertaa saatiin " + KolikonHeittoPeli(klaava, maara) + " heitolla.");
        Console.WriteLine("Keskimäärin " + klaava + " perättäistä klaavaa saatiin heittämällä " + KolikonHeittoPeli(klaava, maara) / maara + " heittoa.");
        Console.ReadKey();
        goto Alku;
    }
    /// <summary>
    /// Ohjelma palauttaa lukuarvona sen, kuinka monta kertaa pitää arpoa luku 0:n ja 1:n väliltä että saadaan suotuisa tulos halutun monta kertaa.
    /// </summary>
    /// <param name="klaava">Suotuisten tulosten peräkkäinen määrä.</param>
    /// <param name="maara">Suotuisten peräkkäisyyksien suotuisa määrä.</param>
    /// <returns>kuinka monta kertaa meni, että saatiin luku 1 'klaava' kertaa peräkkäin 'maara' kertaa.</returns>
    public static int KolikonHeittoPeli(int klaava, int maara)
    {
        Random rnd = new Random();
        int kolikko = rnd.Next(2); // onko kolikko kruuna (0) vai klaava (1);

        int loopvalue = 0; // kuinka monta kertaa loop on suoritettu
        
        int klaavojenMaara = 0;
        int klaavaJonojenMaara = 0;

        while (klaavaJonojenMaara < maara)
        {
            while (klaavojenMaara < klaava)
            {
                switch (kolikko)
                {
                    case 0:
                        loopvalue++;
                        klaavojenMaara = 0;
                        break;

                    case 1:
                        klaavojenMaara++;
                        loopvalue++;
                        break;
                    default:
                        return 0;
                }
                kolikko = rnd.Next(2);
            }
            klaavojenMaara = 0;
            klaavaJonojenMaara++;
        }
        return loopvalue;
    }
}
