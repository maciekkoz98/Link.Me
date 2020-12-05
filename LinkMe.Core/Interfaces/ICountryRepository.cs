using LinkMe.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkMe.Core.Interfaces
{
    public interface ICountryRepository
    {
        Task<IReadOnlyList<Country>> ListAllCountries();
    }
}
