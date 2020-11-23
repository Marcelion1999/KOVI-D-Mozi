using System;
using System.Collections.Generic;
using System.Text;

namespace KOVI_D_Mozi
{
    class Szék
    {
        private int terem_ID;
        private int sor;
        private int oszlop;

        public Szék(string sor)
        {
            string[] adat = sor.Split(';');
            this.Terem_ID = Convert.ToInt32(adat[0]);
            this.Sor = Convert.ToInt32(adat[1]); 
            this.Oszlop = Convert.ToInt32(adat[2]);
        }

        public int Terem_ID { get => terem_ID; set => terem_ID = value; }
        public int Sor { get => sor; set => sor = value; }
        public int Oszlop { get => oszlop; set => oszlop = value; }
    }
}
