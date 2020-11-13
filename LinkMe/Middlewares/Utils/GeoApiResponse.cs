using Newtonsoft.Json;

namespace LinkMe.Middlewares.Utils
{
    public class GeoApiResponse
    {
        public string Ip { get; set; }

        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "country_name")]
        public string CountryName { get; set; }

        [JsonProperty(PropertyName = "region_code")]
        public string RegionCode { get; set; }

        [JsonProperty(PropertyName = "region_name")]
        public string RegionName { get; set; }

        public string City { get; set; }
    }
}
