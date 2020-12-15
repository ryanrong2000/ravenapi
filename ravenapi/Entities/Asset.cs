using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ravenapi.Entities
{
    public class Asset
    {
        public int custId { get; set; }
        public string asset { get; set; }
        public string assetRef { get; set; }
        public string tag { get; set; }
        public DateTime createDateTime { get; set; }

        public int status { get; set; }
    }
}
