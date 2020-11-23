﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace KOVI_D_Mozi
{
    class Program
    {
        static List<Vetítés> Vetítések = new List<Vetítés>();
        static List<Film> Filmek = new List<Film>();
        static List<Szék> Székek = new List<Szék>();

        #region Kinézet
        static int tableWidth = 75;
        static void PrintHeader()
        {
            Console.WriteLine(new string('~', tableWidth));
            PrintRow("KOVI-D MOZI");
            Console.WriteLine(new string('~', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        #endregion
        #region Adat_betölt
        static StreamReader Olvasó;
        public static void Feltölt() 
        {
            Film Adat_Film;
            Szék Adat_Szék;
            Vetítés Adat_Vetítés;
          
            if (File.Exists("Filmek.txt"))
            {
                Olvasó = new StreamReader("Filmek.txt");
                string line;
                while ((line = Olvasó.ReadLine()) != null)
                {
                   //Console.WriteLine(line);
                    Adat_Film = new Film(line);
                    Filmek.Add(Adat_Film);
                }
            }
            else
            {
                Console.WriteLine("Nem található a Filmek.txt");
            }

            if (File.Exists("Székek.txt"))
            {
                Olvasó = new StreamReader("Székek.txt");
                string line;
                while ((line = Olvasó.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    Adat_Szék = new Szék(line);
                    Székek.Add(Adat_Szék);
                }

            }
            else
            {
                Console.WriteLine("Nem található a Székek.txt");
            }

            if (File.Exists("Vetítések.txt"))
            {
                Olvasó = new StreamReader("Vetítések.txt");
                string line;
                while ((line = Olvasó.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    Adat_Vetítés = new Vetítés(line);
                    Vetítések.Add(Adat_Vetítés);
                }
            }
            else
            {
                Console.WriteLine("Nem található a Vetítések.txt");
            }
            Olvasó.Close();
        }
        #endregion
        private static bool MainMenu()
        {

            Console.Clear();
            PrintHeader();
            Console.WriteLine("Válassz egy menüpontot:");
            Console.WriteLine("1) Bejelentkezés");
            // felvitel
            Console.WriteLine("2) Regisztráció");
            Console.WriteLine("3) Keresés");
            Console.WriteLine("4) Listázás");
            Console.WriteLine("5) Kilépés");
            Console.Write("\r\nKérlek válassz: ");

            switch (Console.ReadLine())
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    Listázás();
                    break;
                case "5":
                    break;
                default:
                    return false;
            }
            return true;
        }

        static void Listázás() 
        {
            var query = from vetites in Vetítések

                        join film in Filmek
                        on vetites._Film_ID equals film._Film_ID

                        select new { film._Név,
                                     vetites._Datum.S_date
                        };

            // Execute the query.
            foreach (var i in query)
            {
                Console.Write(i._Név + " : " + i.S_date + " óra");
            }
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            bool showMenu = true;
            Feltölt();
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
    }
}
