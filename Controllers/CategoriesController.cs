using Microsoft.AspNetCore.Mvc;
using sdlt.DataTransferObjects;
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
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryForCreationDto categoryForCreation){
        var createdCatetgory = await _service.CategoryService.CreateCategory(categoryForCreation, trackChanges: false);
        return CreatedAtRoute("CategoryById", new {id = createdCatetgory.Id}, createdCatetgory);
    }



    [HttpGet("{id:guid}", Name ="CategoryById")]
    public async Task<IActionResult> GetCategory(Guid id){
        var category = await _service.CategoryService.GetCategoryAsync(id, trackChanges: false)
            ?? throw new CategoryNotFoundException(id);
        return Ok(category);
    }

    [HttpGet("{id:guid}/products")]
    public async Task<IActionResult> GetProductsForCategory([FromQuery] ProductParameters parameters, Guid id){
        // la carga retrasada (lazy loading) necesita que se rastree la entidad de la bd por eso true
        var productsList = await _service.ProductService.GetProductsForCategory(parameters, id, trackChanges: true);
        return Ok(productsList);
    }
}