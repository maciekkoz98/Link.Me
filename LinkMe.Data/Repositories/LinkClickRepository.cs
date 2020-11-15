using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using LinkMe.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkMe.Data.Repositories
{
    public class LinkClickRepository : EFRepository<LinkClick>, ILinkClickRepository
    {
        public LinkClickRepository(LinkMeContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<LinkClick>> GetCicksByLinkId(int linkId)
        {
            return await this.dbContext.LinkClicks.Where(x => x.LinkId.Equals(linkId)).ToListAsync();
        }
    }
}
