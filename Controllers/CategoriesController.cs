
using Microsoft.AspNetCore.Mvc;

namespace sdlt.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase{
    private readonly IServiceManager _service;

    public CategoriesController(IServiceManager service) => _service = service;

    [HttpGet]
    public IActionResult GetCompanies(){
        try{
            var companies = _service.CategoryService.GetAllCategories(trackChanges: false);
            return Ok(companies);
        }catch{
            return StatusCode(500, "Internal server error");
        }
    }
}