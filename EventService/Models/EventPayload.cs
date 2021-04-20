using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventService.Models
{
    public class EventPayload
    {      
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PersonName { get; set; }
        public string PersonDOB { get; set; }
    }
}
