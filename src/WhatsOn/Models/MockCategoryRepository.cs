using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsOn.Models
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories
        {
            get
            {
                return new List<Category>
                {
                    new Category{CategoryId=1, CategoryName="Entertainment", Description="Party everyday"},
                    new Category{CategoryId=2, CategoryName="Food", Description="Best places to eat"},
                    new Category{CategoryId=3, CategoryName="Tour", Description="See the City"}
                };
            }
        }
    }
}
