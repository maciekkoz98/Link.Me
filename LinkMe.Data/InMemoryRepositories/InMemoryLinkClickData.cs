using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Data.InMemoryRepositories
{
    public class InMemoryLinkClickData : ILinkClickData
    {
        private readonly List<LinkClick> linkClicks;

        public InMemoryLinkClickData()
        {
            this.linkClicks = new List<LinkClick>
            {
                new LinkClick { Id = 1, IPAddress = "45.123.45.234", LinkId = 1, WhenClicked = DateTime.UtcNow.AddDays(-9) },
                new LinkClick { Id = 2, IPAddress = "21.37.69.88", LinkId = 1, WhenClicked = DateTime.UtcNow.AddDays(-4) },
            };
        }

        public LinkClick AddLinkClick(int linkID, string ipAddress, string country, string countryRegion)
        {
            var newLinkClick = new LinkClick
            {
                LinkId = linkID,
                IPAddress = ipAddress,
                WhenClicked = DateTime.UtcNow,
                Country = country,
                CountryRegion = countryRegion,
            };
            this.linkClicks.Add(newLinkClick);
            newLinkClick.Id = this.linkClicks.Max(l => l.Id) + 1;
            return newLinkClick;
        }

        public IEnumerable<LinkClick> GetLinkClicksByLinkID(int linkID)
        {
            return from l in this.linkClicks
                   where l.LinkId == linkID
                   select l;
        }
    }
}
