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
        public string MyEventId { get; set; }
        //Keep track of the MyEventItems the user has selected in the MyEventItem List
        public List<MyEventItem> MyEventItems { get; set; }

        //AddToEventList method adds selected event and the amount of this event!!.
        public void AddToEventList(Event events, int amount)
        {
            var myEventItem =
                    _appDbContext.MyEventItems.SingleOrDefault(
                        s  => s.Event.EventId == events.EventId && s.MyEventId == MyEventId);

            //Not storing events directly to the database.Actually storing MyEventIems and these are created here
            if (myEventItem == null)
            {
                myEventItem = new MyEventItem
                {
                    MyEventId = MyEventId,
                    Event = events,
                    Amount = 1
                };
                //Add the newly created MyEventIems to the appDbContext
                _appDbContext.MyEventItems.Add(myEventItem);
            }
            else
            {
                myEventItem.Amount++;
            }
            //save the changes to the appDbContext
            _appDbContext.SaveChanges();
        }


        //RemoveFromEventList method
        public int RemoveFromEventList(Event events)
        {
            var myEventItem =
                    _appDbContext.MyEventItems.SingleOrDefault(
                        s => s.Event.EventId == events.EventId && s.MyEventId == MyEventId);

            var localAmount = 0;

            if (myEventItem != null)
            {
                if (myEventItem.Amount > 1)
                {
                    myEventItem.Amount--;
                    localAmount = myEventItem.Amount;
                }
                else
                {
                    _appDbContext.MyEventItems.Remove(myEventItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }


        //Method to return the MyEventItems
        //check if you already have the MyEventItems and if not return them from the database
        public List<MyEventItem> GetMyEventItems()
        {
            return MyEventItems ??
                   (MyEventItems =
                       _appDbContext.MyEventItems.Where(c => c.MyEventId == MyEventId)
                           .Include(s => s.Event)
                           .ToList());
        }


        //ClearEventList method
        public void ClearEventList()
        {
            var eventListItems = _appDbContext
                .MyEventItems
                .Where(eventList => eventList.MyEventId == MyEventId);

            _appDbContext.MyEventItems.RemoveRange(eventListItems);

            _appDbContext.SaveChanges();
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
            return new MyEvent(context) { MyEventId = eventListId };
        }
        
    }
}
