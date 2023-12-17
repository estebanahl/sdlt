
using Microsoft.AspNetCore.Mvc;
using sdlt.Entities.Exceptions;

namespace sdlt.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase{
    private readonly IServiceManager _service;
    public CategoriesController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll(){
        // try{
            var companies = await _service.CategoryService.GetAllCategoriesAsync(trackChanges: false);
            return Ok(companies);
        // }catch{
        //     return StatusCode(500, "Internal server error");
        // }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategory(Guid id){
        var category = await _service.CategoryService.GetCategoryAsync(id, trackChanges: false)
            ?? throw new CategoryNotFoundException(id);
        return Ok(category);
    }
}