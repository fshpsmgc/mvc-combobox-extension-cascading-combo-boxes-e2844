using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS.Models{
    public class Country{
        public int ID { get; set; }
        public string Name { get; set; }

        public static IEnumerable<Country> GetCountries(){
            List<Country> list = new List<Country>();
            for (int i = 0; i < 100; i++)
                list.Add(new Country { ID = i, Name = "Country " + i.ToString() });
            return list;
        }
    }
}