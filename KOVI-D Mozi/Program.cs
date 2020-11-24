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

        static StreamReader Olvasó;
        static StreamWriter Író;

        static int User_Last_ID;
        static int Film_Last_ID;
        static int Vetítés_Last_ID;

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


        static void User_Betölt() 
        {
            Userek.Clear();
            Felhasználó User;
            if (File.Exists("Userek.txt"))
            {
                Olvasó = new StreamReader("Userek.txt");
                string line;
                while ((line = Olvasó.ReadLine()) != null)
                {
                    User = new Felhasználó(line);
                    Userek.Add(User);
                    User_Last_ID = User.ID;
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
        static void Film_Betölt() 
        {
            Filmek.Clear();
            Film Adat_Film;
            if (File.Exists("Filmek.txt"))
            {
                Olvasó = new StreamReader("Filmek.txt");
                string line;
                while ((line = Olvasó.ReadLine()) != null)
                {
                    Adat_Film = new Film(line);
                    Filmek.Add(Adat_Film);
                    Film_Last_ID = Adat_Film.Film_ID;
                }
            }
            else
            {
                Console.WriteLine("Nem található a Filmek.txt");
            }
            Olvasó.Close();
        }
        static void Vetítés_Betölt() 
        {
            Vetítések.Clear();
            Vetítés Adat_Vetítés;
            if (File.Exists("Vetítések.txt"))
            {
                Olvasó = new StreamReader("Vetítések.txt");
                string line;
                while ((line = Olvasó.ReadLine()) != null)
                {
                    Adat_Vetítés = new Vetítés(line);
                    Vetítések.Add(Adat_Vetítés);
                    Vetítés_Last_ID = Adat_Vetítés.ID;
                }
            }
            else
            {
                Console.WriteLine("Nem található a Vetítések.txt");
            }
            Olvasó.Close();
        }
        static void Szék_Betölt() 
        { 
            Székek.Clear();
            Szék Adat_Szék;
            if (File.Exists("Székek.txt"))
            {
                Olvasó = new StreamReader("Székek.txt");
                string line;
                while ((line = Olvasó.ReadLine()) != null)
                {
                    Adat_Szék = new Szék(line);
                    Székek.Add(Adat_Szék);
                }
            }
            else
            {
                Console.WriteLine("Nem található a Székek.txt");
            }
            Olvasó.Close();
        }
        public static void Feltölt() 
        {
            Film_Betölt();
            Vetítés_Betölt();
            Szék_Betölt();
            User_Betölt();
            Olvasó.Close();
        }
        #endregion

        #region Menu
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
                    return false;
                default:
                    return true ;
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
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintHeader("USER MENU");
            Console.WriteLine("Válassz egy menüpontot:");
            Console.WriteLine("\t1) Foglalás");
            Console.WriteLine("\t2) SAMPLE MENU");
            Console.WriteLine("\t3) Kijelentkezés");

            Console.Write("\r\nKérlek válassz: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Foglalás(" ");
                    break;
                case "2":
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return false;
                default:
                    return true;
            }
            return true;
        }

        private static bool Admin_Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            PrintHeader("ADMIN MENU");
            //Console.ForegroundColor = ConsoleColor.Gray;
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
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return false;
                default:
                    return true;
            }
            return true;
        }

        private static void Új_Film()
        {
            Console.Clear();
            PrintHeader("ADMIN - ÚJ FILM");
            Console.Write("Kérlek add meg a film címét: ");
            string film_cím = Console.ReadLine();
            Író = new StreamWriter("Filmek.txt", true);
            try
            {
                Író.WriteLine("{0};{1}", Film_Last_ID + 1, film_cím);
                Film_Last_ID++;
             
            }
            catch (Exception e)
            {
                Console.WriteLine("Nem sikerült a Film felvétele");
                Console.WriteLine("Hiba: {0}", e); throw;
            }
            Console.WriteLine("Sikeresen felvitted : {0}\n Nyomj meg egy gombot, hogy továbblépj", film_cím);
            Console.ReadKey();
            Író.Flush();
            Író.Close();
            Film_Betölt(); // Újra betölti,hogy benne legyen az amit most vitt fel
        }

        private static void Új_Vetítés()
        {
            Console.Clear();
            PrintHeader("ADMIN - ÚJ VETÍTÉS");
            Console.WriteLine("FILMEK:");
            var query = from film in Filmek
                        select new { film.Film_ID, film.Név };

            foreach (var i in query)
            {
                Console.WriteLine("\t" + i.Film_ID + ") " + i.Név);
            }
            Console.Write("\nKérlek add meg a Film ID-jét: ");
            string film_id = Console.ReadLine();
            Console.Write("Kérlek add meg a termet: ");
            string terem = Console.ReadLine();
            Console.Write("Kérlek add meg a dátumot: YYYY-MM-DD-HH ");
            string datum = Console.ReadLine();

            Író = new StreamWriter("Vetítések.txt", true);

            try
            {
                Író.WriteLine("{0};{1};{2};{3}", Vetítés_Last_ID + 1, film_id,terem, datum);
                Vetítés_Last_ID++;

            }
            catch (Exception e)
            {
                Console.WriteLine("Nem sikerült a Film felvétele");
                Console.WriteLine("Hiba: {0}", e); throw;
            }
            Console.WriteLine("Sikeresen felvitted a vetítést!\n Nyomj meg egy gombot, hogy továbblépj");
            Console.ReadKey();
            Író.Flush();
            Író.Close();
            Vetítés_Betölt();
        }

        static void Regisztráció()
        {
            Console.Clear();
            PrintHeader("KOVI-D MOZI - REGISZTRÁCIÓ");
            Console.Write("Kérlek add meg az e-mail címedet: ");
            string email = Console.ReadLine();
            Console.Write("Kérlek add meg a jelszót a fiókodhoz: ");
            string jelszó = Console.ReadLine();
            Console.Write("Kérlek add meg a telefonszámod: ");
            string telefon = Console.ReadLine();
            Író = new StreamWriter("Userek.txt", true);
            try
            {
                Író.WriteLine("{0};{1};{2};{3}", User_Last_ID + 1, email, jelszó, telefon);
                User_Last_ID++;
            }
            catch (Exception e)
            {

                Console.WriteLine("Nem sikerült a regisztráció");
                Console.WriteLine("Hiba: {0}", e); throw;
            }
            Console.WriteLine("Sikerült a regisztráció");
            Író.Flush();
            Író.Close();
            User_Betölt(); // újratölti,hogy benne legyen az újonnan felvitt
            // Felhasználó user = new Felhasználó(email,jelszo,telefon);
        }
        static void Listázás()
        {
            Console.Clear();
            PrintHeader("KOVI-D MOZI - VETÍTÉSEK");

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
            PrintHeader("KOVI-D MOZI - VETÍTÉS KERESÉS");
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

        static void Foglalás(string sor) 
        {
            Console.Clear();
            PrintHeader("KOVI-D MOZI - FOGLALÁS");
            Console.ReadKey();
        }
        #endregion

        static void Main(string[] args)
        {
            bool showMenu = true;
            Feltölt();
            while (showMenu)
            {
                showMenu = MainMenu();
            }
            Olvasó.Close();
            //Író.Flush();
            //Író.Close();
            Environment.Exit(0);
        }
    }
}