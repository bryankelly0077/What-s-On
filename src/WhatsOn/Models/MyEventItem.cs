using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsOn.Models
{
    //Class to store the MyEvent data
    public class MyEventItem
    {
        //Create an instance of MyEventItem for each event that gets added to the cart 
        public int MyEventItemId { get; set; }
        //the event that is selected
        public Event Event { get; set; }
        //the amount that the user wants to order
        public int Amount { get; set; }
        //link the Item to the EventList
        public string MyEventId { get; set; }
        
    }
}
