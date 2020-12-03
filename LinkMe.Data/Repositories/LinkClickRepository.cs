using LinkMe.Core.DTOs;
using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using LinkMe.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkMe.Data.Repositories
{
    public class LinkClickRepository : EFRepository<LinkClick>, ILinkClickRepository
    {
        public LinkClickRepository(LinkMeContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<LinkClick>> GetCicksByLinkIdAsync(int linkId)
        {
            return await this.dbContext.LinkClicks.Where(x => x.LinkId.Equals(linkId)).ToListAsync();
        }

        public async Task<IReadOnlyList<DateStatsDto>> GetDateStatsByLinkIdAsync(int linkId)
        {
            return await this.dbContext.LinkClicks
                .Where(x => x.LinkId.Equals(linkId))
                .GroupBy(x => new
                {
                    x.WhenClicked,
                })
                .Select(x => new DateStatsDto()
                {
                    ClickDate = x.Key.WhenClicked,
                    Count = x.Count(),
                })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<RegionStatsDto>> GetRegionsStatsByLinkIdAsync(int linkId)
        {
            return await this.dbContext.LinkClicks
                .Where(x => x.LinkId.Equals(linkId))
                .GroupBy(x => new
                {
                    x.CountryCode,
                })
                .Select(x => new RegionStatsDto()
                {
                    Id = x.Key.CountryCode,
                    Value = x.Count(),
                })
               .ToListAsync();
        }
    }
}
