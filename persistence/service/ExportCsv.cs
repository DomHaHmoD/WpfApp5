/*using System;
using System.Text;
using core.persistence;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace core.service
{
   
    public class ExportCsv : ParentService
    {
        
        public void Export()
        {

            //var datameteolist = connexion.datameteo.ToList();
            List<datameteo> c = (from d in connexion.datameteo select d).ToList(); // identique with Linq

            StringBuilder csvcontent = new StringBuilder();
            csvcontent.AppendLine("id, date, temperature, taux d'humidite");

            foreach (datameteo data in c)
            {

                csvcontent.AppendLine(data.id.ToString() + "," +
                data.date.ToString() + "," +
                data.temperature.ToString().Replace(",", ".") + "," +
                data.humudite.ToString() + "%");

            }
            Console.ReadLine();
            MessageBox.Show(csvcontent.ToString());
            try
            {
                string pathCsv = @"C:\\Users\Dominique H\source\repos\WpfApp5\WpfApp5\sourcedonnees.csv";
                File.AppendAllText(pathCsv, csvcontent.ToString());

                Console.WriteLine("fichier csv créé");
                MessageBox.Show("votre fichier .csv a bien été céer");
            }
            catch
            {
                MessageBox.Show("attention le fichier n'a pu être créer");
            }

        }


}



}*/

using core.persistence;
//using core.service;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
//using System.Collections.Generic;

namespace core.service
{
    public class ExportCsv : ParentService
    {
        public void Export()
        {
            var datameteolist = connexion.datameteo.ToList();
            //var datameteolist = (from d in connexion.datameteolist select d).ToList();  //identique à la ligne du dessus mais au format Linq

            StringBuilder csvcontent = new StringBuilder();
            csvcontent.AppendLine("id, date, temperature, taux d'humidite");

            foreach (datameteo data in datameteolist)
            {

                csvcontent.AppendLine(data.id.ToString() + "," +
                    data.date.ToString() + "," +
                    data.temperature.ToString().Replace(",", ".") + "," +
                    data.humudite.ToString() + "%");
            }

            Console.ReadLine();
            try
            {
                string pathCsv = @"C:\Users\Dominique H\source\repos\WpfApp5\WpfApp5\sourcedonnees.csv";
                File.AppendAllText(pathCsv, csvcontent.ToString());

                Console.WriteLine("fichier CSV créé");
                MessageBox.Show("votre fichier .csv a bien été créé");
            }
            catch
            {
                MessageBox.Show("attention le fichier n'a pu être créé");
            }
        }
    }
}
