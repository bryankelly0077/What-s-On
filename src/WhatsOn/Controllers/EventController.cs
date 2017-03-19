using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WhatsOn.Models;
using WhatsOn.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatsOn.Controllers
{
    public class EventController : Controller
    {
        //implementing the IPieRepository and ICategoryRepository interfaces and
        //getting back the real database and not the mock
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;

        //constructor
        public EventController(IEventRepository eventRepository, ICategoryRepository categoryRepository)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(string category)
        {
            IEnumerable<Event> events;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                events = _eventRepository.Events.OrderBy(p => p.EventId);
                currentCategory = "All events";
            }
            else
            {
                events = _eventRepository.Events.Where(p => p.Category.CategoryName == category)
                   .OrderBy(p => p.EventId);
                currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category).CategoryName;
            }

            return View(new EventsListViewModel
            {
                Events = events,
                CurrentCategory = currentCategory
            });
        }
    }
}
/*
//Action method List
public ViewResult List()
{
    EventsListViewModel eventsListViewModel = new EventsListViewModel();
    eventsListViewModel.Events = _eventRepository.Events;

    //eventsListViewModel.CurrentCategory = "Comedy";

    //View to show
    //return View(_eventRepository.Events);
    return View(eventsListViewModel);

}
*/
