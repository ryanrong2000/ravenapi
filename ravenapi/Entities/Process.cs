using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ravenapi.Entities
{
    public class Process
    {
        public string processId { get; set; }
        public int custId { get; set; }
        public string burnAddress { get; set; }
        public string asset { get; set; }
        public DateTime createDateTime { get; set; }

        public bool processed { get; set; }
        public DateTime processedDateTime { get; set; }

        public string error { get; set; }
    }
}
