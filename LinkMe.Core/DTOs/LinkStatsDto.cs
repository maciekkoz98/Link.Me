using System.Collections.Generic;

namespace LinkMe.Core.DTOs
{
    public class LinkStatsDto
    {
        public string OwnerId { get; set; }

        public IReadOnlyList<RegionStatsDto> RegionDtos { get; set; }

        public IReadOnlyList<DateStatsDto> DateStatsDtos { get; set; }
    }
}
