using System.Collections.Generic;

namespace WhatsOn.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
