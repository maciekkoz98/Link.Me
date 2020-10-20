using LinkMe.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Data
{
    public class InMemoryLinkClickData : ILinkClickData
    {
        private readonly List<LinkClick> linkClicks;

        public InMemoryLinkClickData()
        {
            this.linkClicks = new List<LinkClick>
            {
                new LinkClick { ID = 1, IPAddress = "45.123.45.234", LinkID = 1, WhenClicked = DateTime.UtcNow.AddDays(-9) },
                new LinkClick { ID = 2, IPAddress = "21.37.69.88", LinkID = 1, WhenClicked = DateTime.UtcNow.AddDays(-4) },
            };
        }

        public LinkClick AddLinkClick(int linkID, string ipAddress)
        {
            var newLinkClick = new LinkClick
            {
                LinkID = linkID,
                IPAddress = ipAddress,
                WhenClicked = DateTime.UtcNow,
            };
            this.linkClicks.Add(newLinkClick);
            newLinkClick.ID = this.linkClicks.Max(l => l.ID) + 1;
            return newLinkClick;
        }

        public IEnumerable<LinkClick> GetLinkClicksByLinkID(int linkID)
        {
            return from l in this.linkClicks
                   where l.LinkID == linkID
                   select l;
        }
    }
}
