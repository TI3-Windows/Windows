using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetbyCategoryId(int id);
        void Add(Category cat);
        void AddItem(Item item, int catId);
        void Update(Category cat);
        void Delete(Category cat);
        void SaveChanges();
        Item GetByItemId(int id);
        Category GetCategoryByItem(Item item);
    }
}
