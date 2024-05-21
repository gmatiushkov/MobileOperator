using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperator.Models
{
    public class Client
    {
        public string PassportNumber { get; set; }
        public string IssuePlaceAndDate { get; set; }
        public string FullName { get; set; }
        public int BirthYear { get; set; }
        public string Address { get; set; }
    }
}
