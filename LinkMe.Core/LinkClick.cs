using System;

namespace LinkMe.Core
{
    public class LinkClick
    {
        public int ID { get; set; }

        public int LinkID { get; set; }

        public string IPAddress { get; set; }

        public DateTime WhenClicked { get; set; }
    }
}
