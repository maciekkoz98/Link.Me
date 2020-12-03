using System.ComponentModel.DataAnnotations;

namespace LinkMe.Core.Entities
{
    public class Country
    {
        [Key]
        [StringLength(15)]
        public string CountryCode { get; set; }
    }
}
