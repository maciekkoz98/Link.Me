using LinkMe.Core.Entities;
using LinkMe.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkMe.Core.Interfaces
{
    public interface ILinkClickRepository : IAsyncRepository<LinkClick>
    {
        Task<IReadOnlyList<LinkClick>> GetCicksByLinkIdAsync(int linkId);

        Task<IReadOnlyList<RegionStatsDto>> GetRegionsStatsByLinkIdAsync(int linkId);

        Task<IReadOnlyList<DateStatsDto>> GetDateStatsByLinkIdAsync(int linkId);
    }
}
