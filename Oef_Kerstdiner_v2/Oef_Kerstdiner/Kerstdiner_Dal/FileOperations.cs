using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Kerstdiner_Models;

namespace Kerstdiner_Dal
{
    public class FileOperations
    {
        public List<Gerecht> BestandLezen(string bestand)
        {
            List<Gerecht> lijstRetour = new List<Gerecht>();            
            using (StreamReader Lezer = new StreamReader(bestand))
            {
                while (!Lezer.EndOfStream)
                {
                    string lijn = Lezer.ReadLine();
                    List<string> gegevens = new List<string>();
                    gegevens = lijn.Split(';').ToList();
                    if (double.TryParse(gegevens[1],out double prijs))
                    {
                        Gerecht g = new Gerecht(gegevens[2], gegevens[0], prijs);
                        lijstRetour.Add(g);
                    }                    
                }
            }
            return lijstRetour;
        }

        public void FoutLoggen(Exception fout)
        {
            using (StreamWriter writer = new StreamWriter("foutenbestand.txt", true))
            {
                writer.WriteLine(DateTime.Now.ToString("d") + " - " + DateTime.Now.ToString("HH:mm:ss tt"));
                writer.WriteLine(fout.GetType().Name);
                writer.WriteLine(fout.Message);
                writer.WriteLine(fout.StackTrace);
                writer.WriteLine(new String('-', 80));
                writer.WriteLine();
            }
        }
    }
}
