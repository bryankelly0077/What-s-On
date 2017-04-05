using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatsOn.Models;
using WhatsOn.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatsOn.Controllers
{
    public class MyEventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly MyEvent _myEvent;

        public MyEventController(IEventRepository eventRepository, MyEvent myEvent)
        {
            _eventRepository = eventRepository;
            _myEvent = myEvent;
        }
        //Index action method
        public ViewResult Index()
        {
            //call GetMyEventItems() in MyEvent class. This method checks if you already have the 
            //Events and if not return the from the database
            var list = _myEvent.GetMyEventLists();
            _myEvent.MyEventLists = list;

            //create a new MyEventViewModel that will be returned to the view
            var myEventViewModel = new MyEventViewModel
            {
                MyEvent = _myEvent
            };

            return View(myEventViewModel);
        }

        //Add Event to list
        public RedirectToActionResult AddToMyEvent(int eventId)
        {
            var selectedEvent = _eventRepository.Events.FirstOrDefault(p => p.EventId == eventId);

            if (selectedEvent != null)
            {
                _myEvent.AddToEventList(selectedEvent, 1);
            }
            return RedirectToAction("Index");
        }
    }
}
