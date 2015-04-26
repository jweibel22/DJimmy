using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJimmy.Domain
{
    public abstract class Event
    {
        public string Id { get; set; }

        public EventType EventType { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
