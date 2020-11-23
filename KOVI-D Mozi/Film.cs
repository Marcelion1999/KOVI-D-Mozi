using System;
using System.Collections.Generic;
using System.Text;

namespace KOVI_D_Mozi
{
    class Film
    {
        private string Név;

        private int Film_ID;

        public Film(string sor)
        {
            string[] adat = sor.Split(';');
            this.Film_ID = Convert.ToInt32(adat[0]);
            this.Név = adat[1];
        }

        public string _Név { get => Név; set => Név = value; }
        public int _Film_ID { get => Film_ID; set => Film_ID = value; }
    }
}
