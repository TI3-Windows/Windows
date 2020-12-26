using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travelore.Model;

namespace Travelore.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Category> _categories;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _categories = context.Categories;
        }

        public void Add(Category cat)
        {
            _categories.Add(cat);
        }

        public void AddItem(Item item, int catId)
        {
            Category cat = _categories.Where(c => c.Id == catId).FirstOrDefault();
            cat.AddItem(item);
        }

        public void Delete(Category cat)
        {
            _categories.Remove(cat);
        }

        public Category GetbyCategoryId(int id)
        {
            return _categories.Include(c => c.Items).SingleOrDefault(c => c.Id == id);
        }

        public Item GetByItemId(int id)
        {
            Item item = null;
            foreach(Category cat in _categories)
            {
                foreach(Item i in cat.Items)
                {
                    if(i.Id == id)
                    {
                        item = i;
                    }
                }
            }
            return item;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categories.Include(c => c.Items);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Category cat)
        {
            _categories.Update(cat);
        }
    }
}
