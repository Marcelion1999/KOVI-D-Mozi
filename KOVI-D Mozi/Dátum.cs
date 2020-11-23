using System;
using System.Collections.Generic;
using System.Text;

namespace KOVI_D_Mozi
{
    class Dátum
    {
        private int Év;
        private int Hónap;
        private int Nap;
        private int Óra;

        public Dátum(string sor)
        { 
            string[] adat = sor.Split(';');
            this.Év = Convert.ToInt32(adat[0]);
            this.Hónap = Convert.ToInt32(adat[1]);
            this.Nap = Convert.ToInt32(adat[2]);
            this.Óra = Convert.ToInt32(adat[3]); 
        }

        public int _Év { get => Év; set => Év = value; }
        public int _Hónap { get => Hónap; set => Hónap = value; }
        public int _Nap { get => Nap; set => Nap = value; }
        public int _Óra { get => Óra; set => Óra = value; }

    }
}
