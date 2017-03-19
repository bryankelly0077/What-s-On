using System.Collections.Generic;
using WhatsOn.Models;

namespace WhatsOn.ViewModels
{
    public class EventsListViewModel
    {
        //displays the list of events
        public IEnumerable<Event> Events { get; set; }
        //displays the category that event belongs to
        public string CurrentCategory { get; set; }
    }
}
