using LinkMe.Core.Entities;
using System.Collections.Generic;

namespace LinkMe.Core.Interfaces
{
    public interface ILinkData
    {
        Link GetLinkByID(int id);

        Link Update(Link updatedLink);

        Link Add(Link newLink);

        Link Delete(int id);

        Link GetLinkByShortLink(string shortLink);

        IEnumerable<Link> GetLinksByOwnerID(string ownerID);

        int GetCountLinks();

        int Commit();
    }
}
