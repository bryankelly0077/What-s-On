using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsOn.Models
{
    //Class to store the MyEvent data
    public class MyEventList
    {
        //Create an instance of MyEventItem for each event that gets added to the list 
        public int MyEventListId { get; set; }
        //the event that is selected
        public Event Event { get; set; }
        //link the Item to the EventList
        public string MyEventUserId { get; set; }
    }
}
