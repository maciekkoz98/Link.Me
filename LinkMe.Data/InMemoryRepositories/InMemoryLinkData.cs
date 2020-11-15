using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Data.InMemoryRepositories
{
    public class InMemoryLinkData : ILinkData
    {
        private readonly List<Link> links;

        public InMemoryLinkData()
        {
            this.links = new List<Link>()
            {
                new Link { Id = 1, OriginalLink = "https://facebook.com", ShortLink = "asd4q21", OwnerId = "-1", ValidTo = DateTime.UtcNow.AddDays(4), ShownSummary = false },
                new Link { Id = 2, OriginalLink = "https://google.com", ShortLink = "jhf435a", OwnerId = "0", ValidTo = DateTime.UtcNow.AddDays(3), ShownSummary = true },
                new Link { Id = 3, OriginalLink = "https://wp.pl", ShortLink = "kjsd9or3", OwnerId = "1", ValidTo = DateTime.UtcNow.AddDays(20), ShownSummary = true },
            };
        }

        public Link Add(Link newLink)
        {
            this.links.Add(newLink);
            newLink.Id = this.links.Max(l => l.Id) + 1;
            return newLink;
        }

        public int Commit()
        {
            return 0;
        }

        public Link Delete(int id)
        {
            var link = this.links.FirstOrDefault(l => l.Id == id);
            if (link != null)
            {
                this.links.Remove(link);
            }

            return link;
        }

        public int GetCountLinks()
        {
            return this.links.Count();
        }

        public Link GetLinkByID(int id)
        {
            return this.links.SingleOrDefault(l => l.Id == id);
        }

        public Link GetLinkByShortLink(string shortLink)
        {
            return this.links.SingleOrDefault(l => l.ShortLink.Equals(shortLink));
        }

        public IEnumerable<Link> GetLinksByOwnerID(string ownerID)
        {
            return from l in this.links
                   where l.OwnerId == ownerID
                   select l;
        }

        public Link Update(Link updatedLink)
        {
            Link link = this.links.SingleOrDefault(l => l.Id == updatedLink.Id);
            if (link != null)
            {
                link.OriginalLink = updatedLink.OriginalLink;
                link.OwnerId = updatedLink.OwnerId;
                link.ShortLink = updatedLink.ShortLink;
                link.ValidTo = updatedLink.ValidTo;
            }

            return link;
        }
    }
}
