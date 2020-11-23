using System;
using System.Collections.Generic;
using System.Text;

namespace KOVI_D_Mozi
{
    class Szék
    {
        private int Terem_ID;
        private int Sor;
        private int Oszlop;

        public Szék(string sor)
        {
            string[] adat = sor.Split(';');
            this.Terem_ID = Convert.ToInt32(adat[0]);
            this.Sor = Convert.ToInt32(adat[1]); 
            this.Oszlop = Convert.ToInt32(adat[2]);
        }

        public int _Terem_ID { get => Terem_ID; set => Terem_ID = value; }
        public int _Sor { get => Sor; set => Sor = value; }
        public int _Oszlop { get => Oszlop; set => Oszlop = value; }
    }
}
