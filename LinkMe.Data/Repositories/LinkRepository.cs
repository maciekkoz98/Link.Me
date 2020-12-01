using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using LinkMe.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkMe.Data.Repositories
{
    public class LinkRepository : EFRepository<Link>, ILinkRepository
    {
        public LinkRepository(LinkMeContext context)
            : base(context)
        {
        }

        public async Task<Link> GetFullLink(int id)
        {
            return await this.dbContext.Links.Include(x => x.LinkClicks).FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IReadOnlyList<Link>> GetLinksByUserId(string userId)
        {
            return await this.dbContext.Links.Where(x => x.OwnerId.Equals(userId)).ToListAsync();
        }

        public async Task<Link> GetLinkByShortLinkAsync(string shortLink)
        {
            return await this.dbContext.Links.FirstOrDefaultAsync(x => x.ShortLink.Equals(shortLink));
        }
    }
}
