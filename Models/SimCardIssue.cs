using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperator.Models
{
    public class SimCardIssue
    {
        public string PassportNumber { get; set; }
        public string SimNumber { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
    }
}
