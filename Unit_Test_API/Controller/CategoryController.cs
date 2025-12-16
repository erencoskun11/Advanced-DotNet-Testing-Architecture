using Microsoft.AspNetCore.Mvc;

namespace Unit_Test_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    // C# 12 Primary Constructor: Injects the repository directly into the class scope
    public class CategoryController(IRepository<Category> repository) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
            => Ok(repository.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
            => repository.GetById(id) switch
            {
                null => NotFound(),
                var category => Ok(category)
            };

        [HttpPost]
        public ActionResult<Category> Create(Category category)
        {
            repository.Add(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repository.Delete(id);
            return NoContent();
        }
    }
}