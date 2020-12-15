using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ravenapi.Entities
{
    public class Tx
    {
        public string tx { get; set; }
        public int custId { get; set; }
        public string processId { get; set; }
        public DateTime createDateTime { get; set; }
    }
}
