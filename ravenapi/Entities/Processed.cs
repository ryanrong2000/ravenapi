using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ravenapi.Entities
{
    public class Processed
    {
        public string processId { get; set; }
        public int custId { get; set; }
        public string burnAddress { get; set; }
        public string burnAddressRef { get; set; }

        public string burnAddressTag { get; set; }

        public string asset { get; set; }
        public string assetRef { get; set; }
        public string assetTag { get; set; }

        public int assetStatus { get; set; }

        public DateTime processedDateTime { get; set; }

        public string tx { get; set; }
    }
}
