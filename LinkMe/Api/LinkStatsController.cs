using LinkMe.Core.DTOs;
using LinkMe.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LinkMe.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LinkStatsController : ControllerBase
    {
        private readonly ILinkStatsService linkStatsService;

        public LinkStatsController(ILinkStatsService linkStatsService)
        {
            this.linkStatsService = linkStatsService;
        }

        [HttpGet("{linkId}")]
        public async Task<ActionResult<LinkStatsDto>> GetLinkStatsDto(int linkId)
        {
            var result = await this.linkStatsService.GetLinkStatsAsync(linkId);
            if (result == null)
            {
                return this.BadRequest();
            }

            if (!result.OwnerId.Equals(this.User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return this.Forbid();
            }

            return this.Ok(result);
        }
    }
}
