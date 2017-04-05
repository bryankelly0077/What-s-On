using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhatsOn.ViewModels;

namespace WhatsOn.Models
{
    //extends the dbcontent base class which comes with entity core.
    //add statement 'using Microsoft.EntityFrameworkCore'
    ////extends the IdentityDbContext base class which gives it support for the identity related features in ASP
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {
        //constructor. this AppDbContext is the intermediate class keeps track of the 
        //data between the database and the code view
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //dbset for the current model classes
        //access point for values in events table
        public DbSet<Event> Events { get; set; }
        //access point for values in Categories table
        public DbSet<Category> Categories { get; set; }
        //access point for values in myeventlist table
        public DbSet<MyEventList> MyEventLists { get; set; }
        //public DbSet<LoginViewModel> LoginViewModels { get; set; }
    }
}
