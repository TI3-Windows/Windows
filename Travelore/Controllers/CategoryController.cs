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

        [HttpPut("{id}")]
        public IActionResult updateCategory(int id)
        {
            Category categoryUpdate = _categoryRepo.GetbyCategoryId(id);
            _categoryRepo.Update(categoryUpdate);
            _categoryRepo.SaveChanges();
            return NoContent();
        }
    }
}