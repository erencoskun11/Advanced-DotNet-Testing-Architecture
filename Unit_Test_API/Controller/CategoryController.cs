using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Unit_Test_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IRepository<Category> repository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var categories = await repository.GetAllAsync(cancellationToken);
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var category = await repository.GetByIdAsync(id, cancellationToken);

            if (category is null)
                return NotFound(new { message = $"Category with id {id} not found." });

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateAsync(
            [FromBody] Category category,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await repository.AddAsync(category, cancellationToken);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = category.Id },
                category
            );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existing = await repository.GetByIdAsync(id, cancellationToken);

            if (existing is null)
                return NotFound(new { message = $"Category with id {id} not found." });

            await repository.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}