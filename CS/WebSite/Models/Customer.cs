using System;
using System.Collections.Generic;

namespace E2844.Models {
    public class Customer {
        public Customer() {
            ID = -1;
            Name = string.Empty;
            Country = -1;
            City = -1;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
    }

    
    public class Country {
        public int ID { get; set; }
        public string Name { get; set; }

        public static IEnumerable<Country> GetCountries(){
            List<Country> list = new List<Country>();
            for(int i = 0; i < 100; i++)
                list.Add(new Country{ID=i, Name="Country" + i.ToString()});
            return list;
        }
    }
    public class City {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CountryID { get; set; }

        public static IEnumerable<City> GetCities(int country) {
            List<City> list = new List<City>();
            for(int i = 0; i < 10000; i++)
                if(country >= 0 && i % 100 == country)
                    list.Add(new City { ID = i, Name = "City" + i.ToString(), CountryID = i % 100 });
            return list;
        }
    }
}