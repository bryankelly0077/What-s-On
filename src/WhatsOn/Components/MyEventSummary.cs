using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WhatsOn.Models;
using WhatsOn.ViewModels;

namespace WhatsOn.Components
{
    //Inherits from ViewComponent using Microsoft.AspNetCore.Mvc
    public class MyEventSummary:ViewComponent
    {
        //Counting the amount of items in the eventList so need a reference to MyEvent
        private readonly MyEvent _myEvent;
        //constructor
        public MyEventSummary(MyEvent myEvent)
        {
            _myEvent = myEvent;
        }
        //Invoke method means code in here will be called automatically
        public IViewComponentResult Invoke()
        {
            //Call GetMyEventItems to make sure items have been loaded from the database
            var items = _myEvent.GetMyEventItems();
            //create 2 mock items and add them to the eventsList
            //var items = new List<MyEventItem>() { new MyEventItem(), new MyEventItem() };
            _myEvent.MyEventItems = items;

            //Create a shoppingCartViewModel pass in the shoppingcart and the total
            var myEventViewModel = new MyEventViewModel
            {
                MyEvent = _myEvent,
                //MyEventTotal = _myEvent.GetMyEventTotal()
            };
            //return a view that passes myEventViewModel to that view
            return View(myEventViewModel);
        }
    }
}
