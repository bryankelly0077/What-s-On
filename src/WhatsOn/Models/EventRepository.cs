using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsOn.Models
{
    public class EventRepository : IEventRepository
    {
        //Event repository will be using appdbcontext field
        private readonly AppDbContext _appDbContext;

        //get an instance of appdbcontext using constructor injection
        //so pass it here as a parameter
        public EventRepository(AppDbContext appDbContext)
        {
            //save it in appdbcontext field
            _appDbContext = appDbContext;
        }
        //loads in all events and include all categories
        public IEnumerable<Event> Events
        {
            get
            {
                return _appDbContext.Events.Include(c => c.Category);
            }
        }
        //loads in all events and includes all categories
        //adds a where statement to check if the eventoftheweek is true
        public IEnumerable<Event> EventsOfTheWeek
        {
            get
            {
                return _appDbContext.Events.Include(c => c.Category).Where(p => p.IsEventOfTheWeek);
            }
        }
        //gets the value of eventID
        public Event GetEventById(int eventId)
        {
            return _appDbContext.Events.FirstOrDefault(p => p.EventId == eventId);
        }
    }
}
