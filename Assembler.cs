using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///TIEP114-kurssin assembler.
///ohjelma toimii komentoriviargumenteilla muodossa Assembler <source> <destination>
///esim. Assembler Pong.asm Pong.hack
///@author Jere Honka
namespace Assembler
{
    class Assembler
    {     
        /// <summary>
        /// Pääohjelma.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            
            string source = args[0];
            string destination = args[1];
            if (source.Length == 0 || destination.Length == 0)
            {
                Console.WriteLine("syötä source-muuttujaan assembly-koodi ja destination-muuttujaan luotavan .hack-tiedoston nimi");
                return;
            }
            int address;

            Parser parser = new Parser(source);
            StreamWriter file = new StreamWriter(destination);
            StringBuilder komento = new StringBuilder();
            SymbolTable table = new SymbolTable(source);
            table.GetSymbols();
            parser.Reset();
            while (parser.Parse())
            {
                if (parser.currentLine.Length == 0) continue;
                if (parser.CommandType() == 'a')
                {
                    if (IsDigitsOnly(parser.Symbol()))
                        file.WriteLine(long.Parse(Convert.ToString(int.Parse(parser.Symbol()), 2)).ToString("D16"));
                    else
                    {
                        address = table.GetAddress(parser.Symbol());
                        file.WriteLine(long.Parse(Convert.ToString(address, 2)).ToString("D16"));

                    }
                }
                if (parser.CommandType() == 'c')
                {
                    komento.Append("111");
                    komento.Append(Code.Comp(parser.Comp()));
                    komento.Append(Code.Dest(parser.Dest()));
                    komento.Append(Code.Jump(parser.Jump()));
                    file.WriteLine(komento);
                    komento.Clear();
                }
            }
            file.Close();
        }
        /// <summary>
        /// Tarkistaa, sisältääkö parametrina saatu merkkijono vain numeroita
        /// </summary>
        /// <param name="s">tutkittava merkkijono</param>
        /// <returns>onko vain numeroita</returns>
        public static bool IsDigitsOnly(string s)
        {
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                    return false;
            }
                return true;
        }
    }

    /// <summary>
    /// Luokka joka hoitaa syötteen parsimisen
    /// </summary>
    public class Parser
    {
        StreamReader file;
        public string currentLine;
        string currentCommand;

        // konstruktori. Parseri lukee tiedostosta joka tulee parametrina
        public Parser(string fileLocation)
        {
            file = new StreamReader(fileLocation);
        }

        /// <summary>
        /// Kertoo onko vielä lisää komentoja jäljellä
        /// </summary>
        /// <returns>true jos on</returns>
        public bool HasMoreCommands()
        {
            if ((currentLine = file.ReadLine()) != null) return true;
            return false;
        }

        /// <summary>
        /// Asettaa nykyisen läpikäytävän rivin nykyiseksi komennoksi
        /// </summary>
        public void Advance()
        {
            currentCommand = currentLine;
        }

        /// <summary>
        /// Kertoo, onko kyseessä A-komento, C-komento vai L-komento
        /// </summary>
        /// <returns>komennon merkkinä</returns>
        public char CommandType()
        {
            switch (currentCommand[0])
            {
                case '@':
                    return 'a';
                case '(':
                    return 'l';
                default:
                    return 'c';
            }
        }

        /// <summary>
        /// Palauttaa A-komennon symbolin tai L-komennon sulkujen sisällön
        /// </summary>
        public string Symbol()
        {
            if (currentCommand[0] == '@') return currentCommand.Substring(1);
            return currentCommand.Substring(1, currentCommand.Length - 2);
        }

        /// <summary>
        /// Palauttaa C-komennon dest-osan
        /// </summary>
        public string Dest()
        {
            if (!(currentCommand.Contains('='))) return "";
            return currentCommand.Substring(0, currentCommand.IndexOf('='));
        }

        /// <summary>
        /// Palauttaa C-komennon comp-osan
        /// </summary>
        public string Comp()
        {
            if ((currentCommand.Contains('=') && currentCommand.Contains(';'))) return currentCommand.Substring(currentCommand.IndexOf('=') + 1, currentCommand.Length - currentCommand.IndexOf(';') - 1);
            if (currentCommand.Contains('=')) return currentCommand.Substring(currentCommand.IndexOf('=') + 1);
            return currentCommand.Substring(0, currentCommand.IndexOf(';'));
        }

        /// <summary>
        /// Palauttaa C-komennon jump-osan
        /// </summary>
        public string Jump()
        {
            if (!(currentCommand.Contains(';'))) return "";
            return currentCommand.Substring(currentCommand.IndexOf(';') + 1);
        }

        /// <summary>
        /// Hoitaa kaiken parsimiseen liittyvän
        /// </summary>
        /// <returns>onko lisää komentoja</returns>
        public bool Parse()
        {
            if (HasMoreCommands())
            {
                if (currentLine.Contains("//")) currentLine = currentLine.Substring(0, currentLine.IndexOf('/'));
                currentLine = currentLine.Trim();
                Advance();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Aloittaa koodin lukemisen alusta.
        /// </summary>
        public void Reset()
        {
            file.DiscardBufferedData();
            file.BaseStream.Seek(0, System.IO.SeekOrigin.Begin); 
        }
    }

    /// <summary>
    /// Muuntaa symbolisen konekielikäskyn binääriseksi
    /// </summary>
    public class Code
    {
        /// <summary>
        /// Palauttaa parametrina saadun käskyn dest-osan binääriesityksen
        /// </summary>
        /// <param name="mnemonic">käsky</param>
        /// <returns>binääriesitys merkkijonona</returns>
        public static string Dest(string mnemonic)
        {
            switch (mnemonic)
            {
                case "M":
                    return "001";
                case "D":
                    return "010";
                case "MD":
                    return "011";
                case "A":
                    return "100";
                case "AM":
                    return "101";
                case "AD":
                    return "110";
                case "AMD":
                    return "111";
                default:
                    return "000";
            }
        }

        /// <summary>
        /// Palauttaa parametrina saadun käskyn comp-osan binääriesityksen
        /// </summary>
        /// <param name="mnemonic">käsky</param>
        /// <returns>binääriesitys merkkijonona</returns>
        public static string Comp(string mnemonic)
        {
            switch (mnemonic)
            {
                case "0":
                    return "0101010";
                case "1":
                    return "0111111";
                case "-1":
                    return "0111010";
                case "D":
                    return "0001100";
                case "A":
                    return "0110000";
                case "!D":
                    return "0001101";
                case "!A":
                    return "0110001";
                case "-D":
                    return "0001111";
                case "-A":
                    return "0110011";
                case "D+1":
                    return "0011111";
                case "A+1":
                    return "0110111";
                case "D-1":
                    return "0001110";
                case "A-1":
                    return "0110010";
                case "D+A":
                    return "0000010";
                case "D-A":
                    return "0010011";
                case "A-D":
                    return "0000111";
                case "D&A":
                    return "0000000";
                case "D|A":
                    return "0010101";
                case "M":
                    return "1110000";
                case "!M":
                    return "1110001";
                case "-M":
                    return "1110011";
                case "M+1":
                    return "1110111";
                case "M-1":
                    return "1110010";
                case "D+M":
                    return "1000010";
                case "D-M":
                    return "1010011";
                case "M-D":
                    return "1000111";
                case "D&M":
                    return "1000000";
                case "D|M":
                    return "1010101";
                default:
                    return "error parsing comp: " + mnemonic;
            }
        }

        /// <summary>
        /// Palauttaa parametrina saadun käskyn jump-osan binääriesityksen
        /// </summary>
        /// <param name="mnemonic">käsky</param>
        /// <returns>binääriesitys merkkijonona</returns>
        public static string Jump(string mnemonic)
        {
            switch (mnemonic)
            {
                case "JGT":
                    return "001";
                case "JEQ":
                    return "010";
                case "JGE":
                    return "011";
                case "JLT":
                    return "100";
                case "JNE":
                    return "101";
                case "JLE":
                    return "110";
                case "JMP":
                    return "111";
                default:
                    return "000";
            }
        }
    }

    /// <summary>
    /// Hoitaa L-komentojen ja muuttujien tulkinnan
    /// </summary>
    public class SymbolTable
    {
        public Dictionary<string, int> symbolitaulu; // tähän tallennetaan symboleja vastaavat osoitteet
        public string source = "";
        private int currentRow = 0;
        private int currentMem = 16;

        /// <summary>
        /// Alustaa symbolitaulun ja lisää siihen standardiin kuuluvat merkkijonot osoitteineen
        /// </summary>
        /// <param name="source">luettava tiedosto</param>
        public SymbolTable(string source)
        {
            this.source = source;

            symbolitaulu = new Dictionary<string, int>();
            symbolitaulu.Add("SP", 0);
            symbolitaulu.Add("LCL", 1);
            symbolitaulu.Add("ARG", 2);
            symbolitaulu.Add("THIS", 3);
            symbolitaulu.Add("THAT", 4);
            symbolitaulu.Add("SCREEN", 16384);
            symbolitaulu.Add("KBD", 24576);
            for(int i = 0; i < 16; i++)
                symbolitaulu.Add("R" + i, i);
        }

        /// <summary>
        /// lisää osoitteen symbolitauluun
        /// </summary>
        /// <param name="symbol">osoitteen symboli</param>
        /// <param name="address">osoite</param>
        public void AddEntry(string symbol, int address)
        {
            symbolitaulu.Add(symbol, address);
        }

        /// <summary>
        /// kertoo onko symbolia vastaava osoite taulussa
        /// </summary>
        /// <param name="symbol">osoitteen symboli</param>
        /// <returns>onko taulussa vai ei</returns>
        public bool Contains(string symbol)
        {
            return symbolitaulu.ContainsKey(symbol);
        }

        /// <summary>
        /// Palauttaa symbolia vastaavan osoitteen
        /// </summary>
        /// <param name="symbol">osoitteen symboli</param>
        /// <returns>osoite</returns>
        public int GetAddress(string symbol)
        {
            return symbolitaulu[symbol];
        }

        /// <summary>
        /// hoitaa kaiken symboleihin liittyvän
        /// </summary>
        public void GetSymbols()
        {
            Parser parser = new Parser(source);
            while (parser.Parse())
            {
                if (parser.currentLine.Length == 0) continue;
                if (parser.CommandType() == 'l')
                {
                    AddEntry(parser.Symbol(), currentRow);
                    continue;
                }
                currentRow++;
            }
            parser.Reset();

            while(parser.Parse())
            {
                if (parser.currentLine.Length == 0) continue;
                if (parser.CommandType() == 'a' && !Assembler.IsDigitsOnly(parser.Symbol()))
                {
                    if (Contains(parser.Symbol())) continue;
                    AddEntry(parser.Symbol(), currentMem);
                    currentMem++;
                }
            }
            
            
        }
    }
}
