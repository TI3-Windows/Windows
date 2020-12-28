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
        private readonly DbSet<Item> _items;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _categories = context.Categories;
            _items = context.Items;
        }

        public void Add(Category cat)
        {
            _categories.Add(cat);
        }

        public void AddItem(Item item, int catId)
        {
            _items.Add(item);
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
            return _items.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categories.Include(c => c.Items);
        }

        public Category GetCategoryByItem(Item item)
        {
            foreach(Category cat in _categories)
            {
                if (cat.Items.Contains(item))
                {
                    return cat;
                }
            }
            return null;
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
