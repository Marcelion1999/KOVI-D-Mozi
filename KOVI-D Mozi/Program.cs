using System;
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
        static List<Felhasználó> Userek = new List<Felhasználó>();


        public enum User_Státusz { Admin, Regisztrált_Látogató,Nem_Regisztrált}
        public enum Jegy_Státusz { Jegy_Állapot}

        #region Kinézet
        static int tableWidth = 75;
        static void PrintHeader(string title)
        {
            Console.WriteLine(new string('~', tableWidth));
            PrintRow(title);
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
        static int Last_ID;
        public static void Feltölt() 
        {
            Film Adat_Film;
            Szék Adat_Szék;
            Vetítés Adat_Vetítés;
            Felhasználó User;

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
            if (File.Exists("Userek.txt"))
            {
                Olvasó = new StreamReader("Userek.txt");
                string line;
                while ((line = Olvasó.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    User = new Felhasználó(line);
                    Userek.Add(User);
                    Last_ID = User.ID;
                }
            }
            else
            {
                Console.WriteLine("Nem található a Userek.txt");
            }
            Felhasználó admin = new Felhasználó();
            Userek.Add(admin);
            Olvasó.Close();
        }
        #endregion
        private static bool MainMenu()
        {
            Console.Clear();
            PrintHeader("KOVI-D MOZI");
            Console.WriteLine("Válassz egy menüpontot:");
            Console.WriteLine("\t1) Bejelentkezés");
            // felvitel
            Console.WriteLine("\t2) Regisztráció");
            Console.WriteLine("\t3) Keresés");
            Console.WriteLine("\t4) Listázás");
            Console.WriteLine("\t5) Kilépés");
            Console.Write("\r\nKérlek válassz: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Bejelentkezés();
                    break;
                case "2":
                    Regisztráció();
                    break;
                case "3":
                    Keresés();
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

        static void Bejelentkezés() 
        {
            Console.Clear();
            PrintHeader("KOVI - D MOZI");
            Console.Write("Kérlek add meg az e-mail címedet: ");
            string email = Console.ReadLine();
            Console.Write("Kérlek add meg a jelszót a fiókodhoz: ");
            string jelszó = Console.ReadLine();
            
                var query = from user in Userek
                            where email.Equals(user.Email)
                            where jelszó.Equals(user.Jelszó)
                            select new { user.Admin, user.Email, user.Jelszó };

            if (!query.Any())
            {
                Console.WriteLine("Nem sikerült bejelentkezni\nNyomj egy gombot a továbblépéshez!");
                Console.ReadKey();
            }
            else
            {
                bool showMenu = true;
                foreach (var i in query)
                {
                    Console.WriteLine(i.Email + " bejelentkezett");
                    if (i.Admin == true)
                    {
                        while (showMenu)
                        {
                            showMenu = Admin_Menu();
                        }
                        
                    }
                    else if (i.Admin == false)
                    {
                        while (showMenu)
                        {
                            showMenu = User_Menu();
                        }
                    }
                }
            }
        }

        private static bool User_Menu()
        {
            Console.WriteLine("User Menu");
            Console.ReadKey();

            return false;
        }

        private static bool Admin_Menu()
        {
            Console.Clear();
            PrintHeader("ADMIN MENU");
            Console.WriteLine("Válassz egy menüpontot:");
            Console.WriteLine("\t1) Vetítés felvitele");
            Console.WriteLine("\t2) Film felvitele");
            Console.WriteLine("\t3) Kijelentkezés");

            Console.Write("\r\nKérlek válassz: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Új_Vetítés();
                    break;
                case "2":
                    Új_Film();
                    break;
                case "3":
                    return false;
                default:
                    return true;
            }
            return true;
        }

        private static void Új_Film()
        {
            throw new NotImplementedException();
        }

        private static void Új_Vetítés()
        {
            throw new NotImplementedException();
        }

        static void Regisztráció() 
        {
            Console.Clear();
            PrintHeader("KOVI-D MOZI");
            Console.Write("Kérlek add meg az e-mail címedet: ");
            string email = Console.ReadLine();
            Console.Write("Kérlek add meg a jelszót a fiókodhoz: ");
            string jelszó = Console.ReadLine();
            Console.Write("Kérlek add meg a telefonszámod: ");
            string telefon = Console.ReadLine();
            StreamWriter Író = new StreamWriter("Userek.txt",true);
            try
            {
                Író.WriteLine("{0};{1};{2};{3}", Last_ID + 1, email, jelszó, telefon);
                Last_ID++;
            }
            catch (Exception e)
            {

                Console.WriteLine("Nem sikerült a regisztráció");
                Console.WriteLine("Hiba: {0}", e); throw;
            }
            Console.WriteLine("Sikerült a regisztráció");
            Író.Flush();
            Író.Close();
           // Felhasználó user = new Felhasználó(email,jelszo,telefon);
        }
        static void Listázás() 
        {
            Console.Clear();
            PrintHeader("KOVI-D MOZI");
            
            var query = from vetites in Vetítések
                        join film in Filmek
                        on vetites.Film_ID equals film.Film_ID
                        select new { vetites.ID, film.Név, vetites.Datum.S_date };

            foreach (var i in query)
            {
                Console.WriteLine(i.ID + ") " + i.Név + " : " + i.S_date + " óra");
            }
            Console.Write("\r\nKérlek válassz: ");
            Foglalás(Console.ReadLine());
        }
        static void Keresés()
        {
            Console.Clear();
            PrintHeader("KOVI-D MOZI");
            Console.Write("Mit keresel ?: ");
            string keresett_film = Console.ReadLine();
            var query = from vetites in Vetítések
                        join film in Filmek
                        on vetites.Film_ID equals film.Film_ID
                        where film.Név.Equals(keresett_film)
                        select new { vetites.ID, film.Név, vetites.Datum.S_date };
            foreach (var i in query)
            {
                Console.WriteLine(i.ID + ") " + i.Név + " : " + i.S_date + " óra");
            }
            Console.Write("\r\nKérlek válassz: ");
            Foglalás(Console.ReadLine());
        }

        static void Foglalás(string sor) { }
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