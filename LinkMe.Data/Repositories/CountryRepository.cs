using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using LinkMe.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkMe.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly LinkMeContext dbContext;

        public CountryRepository(LinkMeContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Country>> ListAllCountries()
        {
            return await this.dbContext.Set<Country>().ToListAsync();
        }
    }
}
