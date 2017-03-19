using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsOn.Models
{
    public class MockEventRepository : IEventRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();

        public IEnumerable<Event> Events
        {
            get
            {
                return new List<Event>
                {
                    new Event {EventId = 1, Title="Dancing", EventDescription="Lorem Ipsum", StartDateTime="12 Nov 2017", EndDateTime="dec", Category = _categoryRepository.Categories.ToList()[0],ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg", IsEventOfTheWeek=false, ImageThumbnailUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg"},
                    new Event {EventId = 2, Title="Eating", EventDescription="Lorem Ipsum", StartDateTime="13 Nov 2017", EndDateTime="oct", Category = _categoryRepository.Categories.ToList()[0],ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg", IsEventOfTheWeek=false, ImageThumbnailUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg"},
                    new Event {EventId = 3, Title="Strawberry Pie", EventDescription="Lorem Ipsum", StartDateTime="14 Nov 2017", EndDateTime="jan", Category = _categoryRepository.Categories.ToList()[0],ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg", IsEventOfTheWeek=true, ImageThumbnailUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg"},
                    new Event {EventId = 4, Title="Strawberry Pie", EventDescription="Lorem Ipsum", StartDateTime="15 Nov 2017", EndDateTime="10pm", Category = _categoryRepository.Categories.ToList()[0],ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg", IsEventOfTheWeek=false, ImageThumbnailUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg"},
                };
            }
        }

        public IEnumerable<Event> EventsOfTheWeek { get; }
        

        public Event GetEventById(int eventId)
        {
            throw new System.NotImplementedException();
        }
    }
}
