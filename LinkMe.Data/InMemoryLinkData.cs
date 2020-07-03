using LinkMe.Core;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Data
{
    public class InMemoryLinkData : ILinkData
    {
        readonly List<Link> links;

        public InMemoryLinkData()
        {
            links = new List<Link>()
            {
                new Link {ID=1, OriginalLink="https://facebook.com", ShortLink="https://Link.Me/asd4q21", OwnerID="-1", ValidTo="data", ShownSummary=false},
                new Link {ID=2, OriginalLink="https://google.com", ShortLink="https://Link.Me/jhf435a", OwnerID="0", ValidTo="data", ShownSummary=true},
                new Link {ID=3, OriginalLink="https://wp.pl", ShortLink="https://Link.Me/kjsd9or3", OwnerID="1", ValidTo="data", ShownSummary=true}
            };
        }

        public Link Add(Link newLink)
        {
            links.Add(newLink);
            newLink.ID = links.Max(l => l.ID) + 1;
            return newLink;
        }

        public int Commit()
        {
            return 0;
        }

        public Link Delete(int id)
        {
            var link = links.FirstOrDefault(l => l.ID == id);
            if (link != null)
            {
                links.Remove(link);
            }
            return link;
        }

        public int GetCountLinks()
        {
            return links.Count();
        }

        public Link GetLinkByID(int id)
        {
            return links.SingleOrDefault(l => l.ID == id);
        }

        public IEnumerable<Link> GetLinksByOwnerID(string ownerID)
        {
            return from l in links
                   where l.OwnerID == ownerID
                   select l;
        }

        public Link Update(Link updatedLink)
        {
            Link link = links.SingleOrDefault(l => l.ID == updatedLink.ID);
            if (link != null)
            {
                link.OriginalLink = updatedLink.OriginalLink;
                link.OwnerID = updatedLink.OwnerID;
                link.ShortLink = updatedLink.ShortLink;
                link.ValidTo = updatedLink.ValidTo;
            }
            return link;
        }
    }
}
