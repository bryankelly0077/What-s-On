using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace WhatsOn.Models
{
    public class DbInitializer
    {
        //Static method seed which accepts IApplication Builder which is also in the startup class
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            //use the applicationBuilder in startup to get access to AppDbContext instance
            AppDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();
            //check if there are any categories already in the context 
            if (!context.Categories.Any())
            {
                //if not, add all categories which are in the dictionary at the end of this class
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }
            //check if there are any categories already in the context
            if (!context.Events.Any())
            {
                //if there are no events in there add add the events to the context
                context.AddRange
                (
                   new Event { Title = "Saw Doctors", EventDescription = "One Last Session", StartDateTime = "12 June 2017. 8pm", EndDateTime = "12 June 2017. 11pm", Category = Categories["Music"], ImageUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/saw-doctors.jpg", IsEventOfTheWeek = true, ImageThumbnailUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/saw-doctors.jpg" },
                   new Event { Title = "Christy Moore", EventDescription = "Irelands finest folk singer", StartDateTime = "14 June 2017. 9pm", EndDateTime = "14 June 2017. 11pm", Category = Categories["Music"], ImageUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/christy%20moore.jpg", IsEventOfTheWeek = false, ImageThumbnailUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/christy%20moore.jpg" },
                   new Event { Title = "Galway Bus Tour", EventDescription = "See the sites of Galway in an Open Top Bus", StartDateTime = "2 June 2017. 10am", EndDateTime = "2 June 2017. 11am", Category = Categories["Tours"], ImageUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/Galway_City_Tour.jpg", IsEventOfTheWeek = true, ImageThumbnailUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/Galway_City_Tour.jpg" },
                   new Event { Title = "Cookes Restaurant", EventDescription = "Celebrity Chef Gordon Ramsey cooking Demonstration", StartDateTime = "12 June 2017. 7pm", EndDateTime = "12 June 2017. 9pm", Category = Categories["Food"], ImageUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/cookes-restaurant.jpg", IsEventOfTheWeek = false, ImageThumbnailUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/cookes-restaurant.jpg" },
                   new Event { Title = "McSwiggans", EventDescription = "GalwayBay Seafood Tasting", StartDateTime = "20 June 2017. 6pm", EndDateTime = "20 June 2017. 8pm", Category = Categories["Tours"], ImageUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/McSwiggans.jpg", IsEventOfTheWeek = true, ImageThumbnailUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/McSwiggans.jpg" },
                   new Event { Title = "Galway Walking Tour", EventDescription = "Explore the Citys' hidden Features", StartDateTime = "15 June 2017. 4pm", EndDateTime = "15 June 2017. 6pm", Category = Categories["Food"], ImageUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/walking-tours.jpg", IsEventOfTheWeek = false, ImageThumbnailUrl = "http://danu6.it.nuigalway.ie/BKelly/Images/walking-tours.jpg" }
                    );
            }
            //commit the data to the database which lies behind the context
            context.SaveChanges();
        }
        //initial list of categories
        //Dictionary in C# is a collection of Keys and Values
        private static Dictionary<string, Category> categories;

        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { CategoryName = "Music" },
                        new Category { CategoryName = "Tours" },
                        new Category { CategoryName = "Food" }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }
    }
}
