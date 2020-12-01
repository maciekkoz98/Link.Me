using LinkMe.Core.DTOs;
using System.Threading.Tasks;

namespace LinkMe.Core.Interfaces
{
    public interface ILinkStatsService
    {
        Task<LinkStatsDto> GetLinkStatsAsync(int linkId);
    }
}
