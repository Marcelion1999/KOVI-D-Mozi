﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KOVI_D_Mozi
{
    class Felhasználó
    {
        private int iD;
        private string email;
        private string jelszó;
        private string telefonszám;
        private bool admin;

        public int ID { get => iD; set => iD = value; }
        public string Email { get => email; set => email = value; }
        public string Jelszó { get => jelszó; set => jelszó = value; }
        public string Telefonszám { get => telefonszám; set => telefonszám = value; }
        public bool Admin { get => admin; set => admin = value; }

        public Felhasználó()
        {
            this.admin = true;
            this.iD = 0;
            this.email = "admin";
            this.jelszó = "admin";
            this.telefonszám = "3258";
        }
        public Felhasználó(string sor)
        {
            string[] adat = sor.Split(';');
            this.admin = false;

            this.iD = Convert.ToInt32(adat[0]);
            this.email = adat[1];
            this.jelszó = adat[2];
            this.telefonszám = adat[3];

        }
        public Felhasználó(string _email, string _jelszó, string _telefon)
        {
            this.email = _email;
            this.jelszó = _jelszó;
            this.telefonszám = _telefon;
            this.admin = false;
        }
    }
}
