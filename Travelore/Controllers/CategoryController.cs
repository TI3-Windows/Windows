using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travelore.Model;

namespace Travelore.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepo.GetCategories();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            return _categoryRepo.GetbyCategoryId(id);
        }

        [HttpGet("GetItemById/{id}")]
        public ActionResult<Item> GetItemById(int id)
        {
            return _categoryRepo.GetByItemId(id);
        }

        [HttpPut("{id}")]
        public IActionResult updateCategory(int id)
        {
            Category categoryUpdate = _categoryRepo.GetbyCategoryId(id);
            _categoryRepo.Update(categoryUpdate);
            _categoryRepo.SaveChanges();
            return NoContent();
        }

        [HttpPost("{id}/NewItem")]
        public ActionResult NewItem(Item i, int id)
        {
            Item item = new Item()
            {
                Name = i.Name,
                Amount = i.Amount,
                DoneItem = i.DoneItem
            };
            _categoryRepo.AddItem(item, id);
            _categoryRepo.SaveChanges();
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpPost]
        public ActionResult NewCategory(Category c)
        {
            Category cat = new Category()
            {
                Name = c.Name,
                DoneCat = c.DoneCat,
                Items = c.Items
            };
            _categoryRepo.Add(cat);
            _categoryRepo.SaveChanges();
            return CreatedAtAction(nameof(GetCategoryById), new { id = cat.Id }, cat);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Category catDel = _categoryRepo.GetbyCategoryId(id);
            _categoryRepo.Delete(catDel);
            _categoryRepo.SaveChanges();
            return NoContent();
        }

        [HttpDelete("DeleteItem/{id}")]
        public IActionResult DeleteItem(int id)
        {
            Item itemDel = _categoryRepo.GetByItemId(id);
            Category cat = _categoryRepo.GetCategoryByItem(itemDel);
            _categoryRepo.DeleteItem(cat, itemDel);
            _categoryRepo.SaveChanges();
            return NoContent();
        }

        [HttpPut("UpdateItem/{id}")]
        public IActionResult UpdateItem(int id)
        {
            Item itemUpdate = _categoryRepo.GetByItemId(id);
            itemUpdate.DoneItem = !itemUpdate.DoneItem;
            Category cat = _categoryRepo.GetCategoryByItem(itemUpdate);
            _categoryRepo.Update(cat);
            _categoryRepo.SaveChanges();
            return NoContent();
        }
    }
}