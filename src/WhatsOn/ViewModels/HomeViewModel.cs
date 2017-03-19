using System.Collections.Generic;
using WhatsOn.Models;

namespace WhatsOn.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Event> EventsOfTheWeek { get; set; }
    }
}
