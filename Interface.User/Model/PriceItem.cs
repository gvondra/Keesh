using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Model
{
    public class PriceItem
    {
        public DateTime? Timestamp { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public int? Volume { get; set; }
    }
}
