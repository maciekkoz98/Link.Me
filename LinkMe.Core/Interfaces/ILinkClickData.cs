using LinkMe.Core.Entities;
using System.Collections.Generic;

namespace LinkMe.Core.Interfaces
{
    public interface ILinkClickData
    {
       LinkClick AddLinkClick(int linkID, string ipAddress, string country, string countryRegion);

       IEnumerable<LinkClick> GetLinkClicksByLinkID(int linkID);
    }
}
