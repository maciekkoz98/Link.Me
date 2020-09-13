using LinkMe.Core;
using System.Collections.Generic;

namespace LinkMe.Data
{
    public interface ILinkData
    {
        Link GetLinkByID(int id);

        Link Update(Link updatedLink);

        Link Add(Link newLink);

        Link Delete(int id);

        IEnumerable<Link> GetLinksByOwnerID(string ownerID);

        int GetCountLinks();

        int Commit();
    }
}
