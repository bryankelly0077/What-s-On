using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsOn.Models
{
    public class MyEvent
    {
        private readonly AppDbContext _appDbContext;
        private MyEvent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //MyEventId of type string
        public string MyEventUserId { get; set; }
        //Keep track of the MyEventItems the user has selected in the MyEventItem List
        public List<MyEventList> MyEventLists { get; set; }
        //public string Id { get; set; }

        
            //AddToEventList method adds selected event and the amount of this event!!.
        public void AddToEventList(Event events, int amount)
        {
            var MyEventList =
                    _appDbContext.MyEventLists.SingleOrDefault(
                        s => s.Event.EventId == events.EventId && s.MyEventUserId == MyEventUserId );

            //Not storing events directly to the database.Actually storing MyEventlist and these are created here
            if (MyEventList == null)
            {
                MyEventList = new MyEventList
                {
                    MyEventUserId = MyEventUserId,
                    Event = events,
                    //Id = Id
                    //Amount = 1
                };
                //Add the newly created MyEventlist to the appDbContext
                _appDbContext.MyEventLists.Add(MyEventList);
            }
            
            //save the changes to the appDbContext
            _appDbContext.SaveChanges();
        }

        //Method to return the MyEventItems
        //check if you already have the MyEventItems and if not return them from the database
        public List<MyEventList> GetMyEventLists()
        {
            //null-coalescing operator ?? returns MyEventLists but if its null returns new one
            return MyEventLists ??
                   (MyEventLists =
                       _appDbContext.MyEventLists.Where(c => c.MyEventUserId == MyEventUserId)
                           .Include(s => s.Event)
                           .ToList());
        }
       
        //Static method GetEventList to return an EventList instance. Passing IServiceProvider (saw this in startup class)
        public static MyEvent GetEventList(IServiceProvider services)
        {
            //To get access to the session use GetRequiredService passing in the IHttpContextAccessor which 
            //gives access to the context. That in turn gives access to the session 
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            //using the services dependancy injection system to get access to the appDbContext instance 
            var context = services.GetService<AppDbContext>();
            //Use the session to check if EventID string is already there and if not create a new guid which will be the EventListId.
            //In the MyEventItem class 'public string MyEventId { get; set; }' will be the session Id you're storing 
            //This links the MyEventItems with the MyEventId
            string eventListId = session.GetString("EventListId") ?? Guid.NewGuid().ToString();
            //Store the value of the EventId in the session
            session.SetString("EventListId", eventListId);
            //Return a new MyEvent which contains the AppDbContext and the MyEventId declared in this class above
            return new MyEvent(context) { MyEventUserId = eventListId };
        }

    }
}
