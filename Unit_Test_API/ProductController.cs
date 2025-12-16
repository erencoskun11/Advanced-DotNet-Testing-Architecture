using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Unit_Test_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IRepository<Product> repository) : ControllerBase
    {
        // 1. Primary Constructor (C# 12): No need for explicit field assignment
        // 2. Expression-bodied members (=>): No need for return/brackets for simple logic

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
            => Ok(repository.GetAll());

       
        [HttpDelete("{id}")]
        public NoContentResult Delete(int id)
        {
            repository.Delete(id);
            return NoContent();
        }
    }
}