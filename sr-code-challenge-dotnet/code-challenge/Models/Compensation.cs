using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        public string compensationID {get;set;}
        public float salary {get;set;}
        public string employeeId {get;set;}
        public DateTime effectiveDate {get;set;}
    }
}