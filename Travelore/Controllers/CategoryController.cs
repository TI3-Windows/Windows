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
    }
}