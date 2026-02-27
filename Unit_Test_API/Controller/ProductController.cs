using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Unit_Test_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IRepository<Product> repository) : ControllerBase
    {
        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await repository.GetAllAsync(cancellationToken);
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await repository.GetByIdAsync(id, cancellationToken);

            if (product is null)
                return NotFound(new { message = $"Product with id {id} not found." });

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateAsync(
            [FromBody] Product product,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await repository.AddAsync(product, cancellationToken);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = product.Id },
                product
            );
        }

        // PUT: api/products/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(
            int id,
            [FromBody] Product updatedProduct,
            CancellationToken cancellationToken)
        {
            if (id != updatedProduct.Id)
                return BadRequest(new { message = "Id mismatch." });

            var existing = await repository.GetByIdAsync(id, cancellationToken);
            if (existing is null)
                return NotFound(new { message = $"Product with id {id} not found." });

            await repository.UpdateAsync(updatedProduct, cancellationToken);

            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existing = await repository.GetByIdAsync(id, cancellationToken);
            if (existing is null)
                return NotFound(new { message = $"Product with id {id} not found." });

            await repository.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}