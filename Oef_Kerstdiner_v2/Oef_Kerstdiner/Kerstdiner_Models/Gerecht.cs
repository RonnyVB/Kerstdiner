using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerstdiner_Models
{
    public class Gerecht
    {
        //Instantievariabelen
        private string _benaming;
        private double _prijs;
        private string _type;

        //Eigenschappen
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        public double Prijs
        {
            get { return _prijs; }
            set { _prijs = value; }
        }


        public string Benaming
        {
            get { return _benaming; }
            set { _benaming = value; }
        }
        //Constructor
        public Gerecht(string type,string benaming, double prijs)
        {
            this.Type = type;
            this.Benaming = benaming;
            this.Prijs = prijs;
        }

        //Methoden
        public override string ToString()
        {
            return Benaming + " - " + Prijs.ToString("0.00 €");
        }
    }
}
