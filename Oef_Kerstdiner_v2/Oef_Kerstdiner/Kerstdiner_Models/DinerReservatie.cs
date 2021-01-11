using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerstdiner_Models
{
    public class DinerReservatie
    {
        //Instantievariabelen
        private int _aantalPersonen;
        private string _hoofdgerecht;
        private string _naam;
        private double _prijsHoofdgerecht;

        //Eigenschappen
        public double PrijsHoofdgerecht
        {
            get { return _prijsHoofdgerecht; }
            set { _prijsHoofdgerecht = value; }
        }


        public string Naam
        {
            get { return _naam; }
            set
            {
                if (string.IsNullOrEmpty(value)==true)
                {
                    throw new CustomException("Naam is niet ingevuld!");
                }
                else
                {
                    _naam = value;
                }
            }
        }


        public string Hoofdgerecht
        {
            get { return _hoofdgerecht; }
            set { _hoofdgerecht = value; }
        }


        public int AantalPersonen
        {
            get { return _aantalPersonen; }
            set
            {
                if (value<0)
                {
                    throw new CustomException("Aantal personen moet een positief getal zijn!");
                }
                else
                {
                    _aantalPersonen = value;
                }
            }
        }
        //Constructor
        public DinerReservatie(string naam, int aantalPersonen, string hoofdgerecht, double prijsHoofdgerecht)
        {
            this.Naam = naam;
            this.AantalPersonen = aantalPersonen;
            this.Hoofdgerecht = hoofdgerecht;
            this.PrijsHoofdgerecht = prijsHoofdgerecht;
        }

        //Methoden
        public virtual double Totaal()
        {
            return AantalPersonen * PrijsHoofdgerecht;
        }

        public override bool Equals(object obj)
        {
            if (obj is DinerReservatie Diner)
            {
                return Diner.Naam == this.Naam;
            }
            return false;
        }
        public override string ToString()
        {
            string Type = this.GetType().Name.Replace("Reservatie", "");
            return Type.PadRight(15)+Naam.PadRight(28)+""+Totaal().ToString("0.00 €").PadLeft(10);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
