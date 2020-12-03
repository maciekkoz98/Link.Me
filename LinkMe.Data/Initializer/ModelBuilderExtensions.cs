using LinkMe.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LinkMe.Data.Initializer
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var countries = new List<Country>();
            var lines = System.IO.File.ReadAllLines("../LinkMe.Data/Initializer/countries.txt");
            foreach (var line in lines)
            {
                countries.Add(new Country
                {
                    CountryCode = line,
                });
            }

            modelBuilder.Entity<Country>().HasData(countries);
        }
    }
}
