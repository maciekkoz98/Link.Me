using LinkMe.Core;
using System.Collections.Generic;

namespace LinkMe.Data
{
    public interface ILinkClickData
    {
       LinkClick AddLinkClick(int linkID, string ipAddress);

       IEnumerable<LinkClick> GetLinkClicksByLinkID(int linkID);
    }
}
