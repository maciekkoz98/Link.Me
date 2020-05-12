using System;
using System.Collections.Generic;
using System.Text;

namespace LinkMe.Core
{
    class Link
    {
        public int ID { get; set; }
        public string OriginalLink { get; set; }
        public string ShortLink { get; set; }
        public int OwnerID { get; set; }
        public string ValidTo { get; set; } //lub DateTime
    }
}
