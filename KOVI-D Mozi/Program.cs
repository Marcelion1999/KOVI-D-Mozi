using System;


namespace KOVI_D_Mozi
{
    class Program
    {

        #region Kinézet
        static int tableWidth = 73;
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

        private static bool MainMenu()
        {

            Console.Clear();
            PrintHeader();
            Console.WriteLine("Válassz egy menüpontot:");
            Console.WriteLine("1) Menu A");
            Console.WriteLine("2) Menu B");
            Console.WriteLine("3) Kilépés");
            Console.Write("\r\nKérlek válassz: ");

            switch (Console.ReadLine())
            {
                case "1":
                    //ReverseString();
                    return true;
                case "2":
                    //RemoveWhitespace();
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }


        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
    }
}
