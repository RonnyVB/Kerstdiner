using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerstdiner_Models
{
    public class KerstdinerReservatie:DinerReservatie
    {
        //Instantievariabelen
        private string _nagerecht;
        private double _prijsNagerecht;

        //Eigenschappen
        public double PrijsNagerecht
        {
            get { return _prijsNagerecht; }
            set { _prijsNagerecht = value; }
        }


        public string Nagerecht
        {
            get { return _nagerecht; }
            set { _nagerecht = value; }
        }

        //Constructor
        public KerstdinerReservatie(string naam, int aantalPersonen,string hoofdgerecht, double prijsHoofdgerecht, string nagerecht, double prijsNagerecht):base(naam,aantalPersonen,hoofdgerecht,prijsHoofdgerecht)
        {
            this.Nagerecht = nagerecht;
            this.PrijsNagerecht = prijsNagerecht;
        }

        public override double Totaal()
        {
            return base.Totaal() + AantalPersonen * PrijsNagerecht;
        }
    }
}
