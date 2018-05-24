﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIProjekt
{
    public class DaneWejsciowe
    {
        public int LiczbaRund { get; set; }
        public int LiczbaAutomatow { get; set; }
        public Automat[] Automaty { get; set; }
        
        public DaneWejsciowe()
        {
            LiczbaRund = 10;
            LiczbaAutomatow = 6;
            Automaty = new Automat[LiczbaAutomatow];
            for (int i = 0; i < LiczbaAutomatow; i++)
            {
                Automat automat = new Automat(i + 1);
                switch (i)
                {
                    case 0:
                        automat.dodajNagrode(new Nagroda(1000, 20));
                        automat.dodajNagrode(new Nagroda(100, 70));
                        automat.dodajNagrode(new Nagroda(-300, 10));
                        break;
                    case 1:
                        automat.dodajNagrode(new Nagroda(500, 50));
                        automat.dodajNagrode(new Nagroda(300, 30));
                        automat.dodajNagrode(new Nagroda(-700, 15));
                        automat.dodajNagrode(new Nagroda(2000, 5));
                        break;
                    case 2:
                        automat.dodajNagrode(new Nagroda(1500, 50));
                        automat.dodajNagrode(new Nagroda(-2000, 50));
                        break;
                    case 3:
                        automat.dodajNagrode(new Nagroda(100, 100));
                        break;
                    case 4:
                        automat.dodajNagrode(new Nagroda(-200, 20));
                        automat.dodajNagrode(new Nagroda(0, 75));
                        automat.dodajNagrode(new Nagroda(3000, 5));
                        break;
                    case 5:
                        automat.dodajNagrode(new Nagroda(500, 80));
                        automat.dodajNagrode(new Nagroda(-1000, 20));
                        break;
                }
                automat.ustalZakresyNagrod();
                Automaty[i] = automat;
            }
        }

        private static DaneWejsciowe singleton = null;
        public static DaneWejsciowe Instancja
        {
            get
            {
                if (singleton == null)
                    singleton = new DaneWejsciowe();
                return singleton;
            }
        }
    }
}