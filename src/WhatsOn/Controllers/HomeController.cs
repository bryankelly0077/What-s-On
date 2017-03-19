
using Microsoft.AspNetCore.Mvc;
using WhatsOn.Models;
using WhatsOn.ViewModels;

namespace BethonysPieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public HomeController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                EventsOfTheWeek = _eventRepository.EventsOfTheWeek
            };

            return View(homeViewModel);
        }
    }
}
