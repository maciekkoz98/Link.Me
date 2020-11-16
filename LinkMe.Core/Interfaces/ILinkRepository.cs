using LinkMe.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkMe.Core.Interfaces
{
    public interface ILinkRepository : IAsyncRepository<Link>
    {
        Task<IReadOnlyList<Link>> GetLinksByUserId(string userID);

        Task<Link> GetLinkByShortLinkAsync(string shortLink);
    }
}
