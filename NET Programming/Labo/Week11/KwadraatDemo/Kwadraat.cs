using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace KwadraatDemo
{
    public class Kwadraat
    {
        private SqlInt64 _getal, _kwadraat;
        public Kwadraat(SqlInt64 getal, SqlInt64 kwadraat)
        {
            _getal = getal;
            _kwadraat = kwadraat;
        }
        [SqlFunction(FillRowMethodName = "VentileerRijen", TableDefinition = "getal bigint, kwadraat bigint")]
        public static IEnumerable MaakKwadraten(int maxgetal)
        {
            try
            {
                ArrayList kwadraten = new ArrayList();
                for (int i = 1; i <= maxgetal; i++)
                {
                    kwadraten.Add(new Kwadraat(i, i * i));
                }
                return kwadraten;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void VentileerRijen(object okwad, out SqlInt64 getal, out SqlInt64 kwadraat)
        {
            Kwadraat kwad = okwad as Kwadraat;
            getal = kwad._getal;
            kwadraat = kwad._kwadraat;
        }
    }
}
