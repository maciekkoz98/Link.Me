using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using LinkMe.Data.Context;

namespace LinkMe.Data.Repositories
{
    public class LinkRepository : EFRepository<Link>, ILinkRepository
    {
        public LinkRepository(LinkMeContext context)
            : base(context)
        {
        }
    }
}
