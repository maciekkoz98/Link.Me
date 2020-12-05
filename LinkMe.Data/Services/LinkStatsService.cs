using LinkMe.Core.DTOs;
using LinkMe.Core.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace LinkMe.Data.Services
{
    public class LinkStatsService : ILinkStatsService
    {
        private readonly ILinkRepository linkRepository;
        private readonly ILinkClickRepository linkClickRepository;
        private readonly ICountryRepository countryRepository;

        public LinkStatsService(ILinkRepository linkRepository, ILinkClickRepository linkClickRepository, ICountryRepository countryRepository)
        {
            this.linkRepository = linkRepository;
            this.linkClickRepository = linkClickRepository;
            this.countryRepository = countryRepository;
        }

        public async Task<LinkStatsDto> GetLinkStatsAsync(int linkId)
        {
            var link = await this.linkRepository.GetByIdAsync(linkId);
            if (link == null)
            {
                return null;
            }

            var regions = await this.linkClickRepository.GetRegionsStatsByLinkIdAsync(link.Id);
            var countries = await this.countryRepository.ListAllCountries();
            var regionsStats = countries.SelectMany(
                country => regions.Where(region => region.Id.Equals(country.CountryCode)).DefaultIfEmpty(),
                (country, region) => new RegionStatsDto
                {
                    Id = country.CountryCode,
                    Value = region == null ? 0 : region.Value,
                }).ToList();

            var dates = await this.linkClickRepository.GetDateStatsByLinkIdAsync(link.Id);
            return new LinkStatsDto()
            {
                OwnerId = link.OwnerId,
                RegionDtos = regionsStats,
                DateStatsDtos = dates,
            };
        }
    }
}
