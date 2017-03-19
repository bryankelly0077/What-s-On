using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsOn.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string EventDescription { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; internal set; }
        public int CategoryId { get; set; }
        public bool IsEventOfTheWeek { get; set; }

        //Virtual Keyword Used for entity framework
        public virtual Category Category { get; set; }
        
    }
}
