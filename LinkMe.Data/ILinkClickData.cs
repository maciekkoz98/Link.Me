using LinkMe.Core;
using System.Collections.Generic;

namespace LinkMe.Data
{
    public interface ILinkClickData
    {
       LinkClick AddLinkClick(int linkID, string ipAddress, string country, string countryRegion);

       IEnumerable<LinkClick> GetLinkClicksByLinkID(int linkID);
    }
}
