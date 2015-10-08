using ProjectFestival.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace labo6_extra
{
    public class Persoon
    {
        private int _id { get; set; }
        private string _voornaam { get; set; }
        private string _naam { get; set; }
        private int _leeftijd { get; set; }

        public List<Persoon> Personen;

        public void inlezenDatabase()
        {
            Personen = new List<Persoon>();
            string sql = "SELECT Id, Voornaam, Naam, Leeftijd FROM Persoon";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                Personen.Add(Create(reader));
            }
        }

        private static Persoon Create(IDataRecord record)
        {
            Persoon persoon = new Persoon();
            try
            {
                if (record["Id"] != DBNull.Value) persoon._id = Convert.ToInt32(record["Id"]);
                if (record["Voornaam"] != DBNull.Value) persoon._voornaam = Convert.ToString(record["Voornaam"]);
                if (record["Naam"] != DBNull.Value) persoon._naam = Convert.ToString(record["Naam"]);
                if (record["Leeftijd"] != DBNull.Value) persoon._leeftijd = Convert.ToInt32(record["Leeftijd"]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Kan geen persoon aanmaken: " + e);
            }
            return persoon;
        }

        public override string ToString()
        {
            return _voornaam + " " + _naam + " " + _leeftijd;
        }
    }
}
