using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS.Models{
    public class City{
        public int ID { get; set; }
        public string Name { get; set; }
        public int CountryID { get; set; }

        public static IEnumerable<City> GetCities(int country)
        {
            List<City> list = new List<City>();
            for (int i = 0; i < 10000; i++)
                if (country >= 0 && i % 100 == country)
                    list.Add(new City { ID = i, Name = "City " + i.ToString(), CountryID = i % 100 });
            return list;
        }
    }
}