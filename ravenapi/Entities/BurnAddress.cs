using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ravenapi.Entities
{
    public class BurnAddress
    {
        public string burnAddress { get; set; }
        public int custId { get; set; }
        public string burnAddressRef { get; set; }
        public string burnAddressTag { get; set; }

        public DateTime updateDateTime { get; set; }
        public DateTime createDateTime { get; set; }

    }
}
