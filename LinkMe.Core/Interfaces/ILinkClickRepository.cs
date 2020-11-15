using LinkMe.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkMe.Core.Interfaces
{
    public interface ILinkClickRepository : IAsyncRepository<LinkClick>
    {
        Task<IReadOnlyList<LinkClick>> GetCicksByLinkId(int linkId);
    }
}
