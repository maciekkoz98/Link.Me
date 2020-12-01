using LinkMe.Core.DTOs;
using LinkMe.Core.Interfaces;
using System.Threading.Tasks;

namespace LinkMe.Data.Services
{
    public class LinkStatsService : ILinkStatsService
    {
        private readonly ILinkRepository linkRepository;
        private readonly ILinkClickRepository linkClickRepository;

        public LinkStatsService(ILinkRepository linkRepository, ILinkClickRepository linkClickRepository)
        {
            this.linkRepository = linkRepository;
            this.linkClickRepository = linkClickRepository;
        }

        public async Task<LinkStatsDto> GetLinkStatsAsync(int linkId)
        {
            var link = await this.linkRepository.GetByIdAsync(linkId);
            if (link == null)
            {
                return null;
            }

            var regions = await this.linkClickRepository.GetRegionsStatsByLinkIdAsync(link.Id);
            var dates = await this.linkClickRepository.GetDateStatsByLinkIdAsync(link.Id);
            return new LinkStatsDto()
            {
                OwnerId = link.OwnerId,
                RegionDtos = regions,
                DateStatsDtos = dates,
            };
        }
    }
}
