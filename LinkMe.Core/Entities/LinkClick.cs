using System;
using System.ComponentModel.DataAnnotations;

namespace LinkMe.Core.Entities
{
    public class LinkClick : BaseEntity
    {
        public int LinkId { get; set; }

        [StringLength(15)]
        public string IPAddress { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(100)]
        public string CountryRegion { get; set; }

        public DateTime WhenClicked { get; set; }
    }
}
