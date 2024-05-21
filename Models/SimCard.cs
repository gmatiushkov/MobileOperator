using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperator.Models
{
    public class SimCard
    {
        public string SimNumber { get; set; }
        public string Tariff { get; set; }
        public int ReleaseYear { get; set; }
        public bool IsAvailable { get; set; }
    }
}
