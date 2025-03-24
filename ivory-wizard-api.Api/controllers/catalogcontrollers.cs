using Microsoft.AspNetCore.Mvc;
using Jet.Piranha.Domain.Catalog;

namespace Jet.Piranha.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok("hello world.");
        }
    }
}
