using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sdlt.ActionFilters;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Controllers;

[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IServiceManager _service;

    private readonly IConfiguration _configuration;

    public ProductsController(IServiceManager service, IConfiguration configuration) {
        _service = service;
        _configuration = configuration;
        }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult> Post([FromBody] ProductForCreationDto model)
    {
        // if (!ModelState.IsValid)
        //     return UnprocessableEntity(ModelState);
        
        var createdProduct = await _service.ProductService.CreateProductAsync(model);
        return CreatedAtRoute("ProductById", new { id = createdProduct.Id }, createdProduct);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] ProductParameters parameters)
    {
        var pagedResult = await _service.ProductService.GetAllProductsAsync(parameters, trackChanges: true);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.products);
        
    }

    [HttpGet("{id:guid}", Name = "ProductById")]
    public async Task<IActionResult> ProductById(Guid id)
    {
        var product = await _service.ProductService.GetProductAsync(id, trackChanges: false)
            ?? throw new ProductNotFoundException(id);
        return Ok(product);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _service.ProductService.DeleteProductAsync(id, trackChanges: false);
        return NoContent();
    }
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] ProductForUpdateDto productForUpdate)
    {
        // if(string.IsNullOrEmpty(productForUpdate.ToString()))
        //     return BadRequest("Product for update object is null");    
        // if (!ModelState.IsValid)
        //     return UnprocessableEntity(ModelState);     
        await _service.ProductService.UpdateProductAsync(id, productForUpdate, trackChanges: true);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult> UpdateProductPicture(Guid id, [FromForm] ProductPictureDto model)
    {                
        await _service.ProductService.UpdateProductPictureAsync(id, model.Picture, trackChanges: true);
        return NoContent();
    }
}
//         // GET: Producto/GetProducto
//         [HttpGet]
//         [Route("Productos/GetProducto")]
//         public Producto GetComida([FromUri] int? id)
//         {
//             return db.Producto.FirstOrDefault(c => c.ProductoId == id);
//         }

//         [HttpGet]
//         [Route("Comidas/GetAllC")]
//         public IQueryable<Producto> GetAllC()
//         {

//             return db.Producto.Where(p => p.CategoriaId == 3 || p.CategoriaId == 4
//             || p.CategoriaId == 5 || p.CategoriaId == 6
//             || p.CategoriaId == 7 || p.CategoriaId == 8);
//         }

//         [HttpGet]
//         [Route("Bebidas/GetAllB")]
//         public IQueryable<Producto> GetAllB()
//         {
//             return db.Producto.Where(p => p.CategoriaId == 10);
//         }
//         [HttpGet]
//         [Route("Productos/GetByCategory")]
//         public IQueryable<ProductOutDto> GetByCategory([FromUri] string categoriaId)
//         {
//             int? categoriaIdConvertida = ConvertirDeDescripcionAId(categoriaId);
//             string nombreDelProcedimiento = "ObtenerProductoPorCategoria";
//             var result = new List<ProductOutDto>();
//             using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SDLTDb"].ToString()))
//             {
//                 connection.Open();
//                 using (SqlCommand command = new SqlCommand(nombreDelProcedimiento, connection))
//                 {
//                     command.CommandType = CommandType.StoredProcedure;
//                     command.Parameters.Add(new SqlParameter("@CategoriaId", categoriaIdConvertida));
//                     using (SqlDataReader reader = command.ExecuteReader())
//                     {
//                         ProductOutDto dtoPOut;
//                         while (reader.Read())
//                         {
//                             dtoPOut = new ProductOutDto
//                             {
//                                 ProductoId = int.Parse(reader[0].ToString()),
//                                 Nombre = reader[1].ToString(),
//                                 Descripcion = reader[2].ToString(),
//                                 Precio = decimal.Parse(reader[3].ToString()),
//                                 CategoriaId = int.Parse(reader[4].ToString()),
//                                 Categoria = reader[5].ToString(),
//                                 Stock = int.Parse(reader[6].ToString()),
//                                 EstaActivo = bool.Parse(reader[7].ToString())
//                             };
//                             result.Add(dtoPOut);
//                         }
//                     }
//                     connection.Close();
//                     return result.AsQueryable();
//                 }
//             }
//             //return db.Producto.Where(p => p.CategoriaId == categoriaIdConvertida);

//         }
//         private int? ConvertirDeDescripcionAId(string categoriaIdInt) {
//             if (!int.TryParse(categoriaIdInt, out int categoriaIdConvertido))
//             {
//                 switch (categoriaIdInt)
//                 {
//                     case "Sopas":
//                         categoriaIdConvertido = (int)CategoriaEnum.Sopa;
//                         break;
//                     case "Carnes":
//                         categoriaIdConvertido = (int)CategoriaEnum.Carne;
//                         break;
//                     case "Entradas":
//                         categoriaIdConvertido = (int)CategoriaEnum.Entrada;
//                         break;
//                     case "Ensalada":
//                         categoriaIdConvertido = (int)CategoriaEnum.Ensalada;
//                         break;
//                     case "Pescados y Mariscos":
//                         categoriaIdConvertido = (int)CategoriaEnum.PescadosYMariscos;
//                         break;
//                     case "Postres":
//                         categoriaIdConvertido = (int)CategoriaEnum.Postre;
//                         break;
//                     case "Postres y Bebidas":
//                         categoriaIdConvertido = (int)CategoriaEnum.Postre;
//                         break;
//                     default:
//                         return null;
//                 }
//             }
//             return categoriaIdConvertido;
//         }
//         private Producto ConvertirDelDtoAProducto(ProductoDto productoDto, int? categoriaIdInt)
//         {
//             Producto elProducto = new Producto()
//             {
//                 ProductoId = productoDto.ProductoId,
//                 Nombre = productoDto.Nombre,
//                 Descripcion = productoDto.Descripcion,
//                 Precio = productoDto.Precio,
//                 CategoriaId = categoriaIdInt,
//                 Stock = productoDto.Stock,
//                 EstaActivo = productoDto.EstaActivo
//             };
//             return elProducto;
//         }

//         [HttpPost]
//         [Route("Productos/Create")]
//         //[Authorize(Roles ="Administrator")]
//         public async Task<IHttpActionResult> Create(ProductoDto productoDto)
//         {
//             int? categoriaId = ConvertirDeDescripcionAId(productoDto.CategoriaId);

//             Producto producto = ConvertirDelDtoAProducto(productoDto, categoriaId);

//             db.Producto.Add(producto);
//             int? guardados = await db.SaveChangesAsync();
//             if(guardados > 0)
//             {
//                 return Content(HttpStatusCode.Created, producto);
//             }
//             else
//             {
//                 return Content(HttpStatusCode.InternalServerError
//                     , "No se pudo procesar correctamente la solicitud de creado. Intente otra vez");
//             }
         // GET: Producto/GetProducto
//         [HttpGet]
//         [Route("Productos/GetProducto")]
//         public Producto GetComida([FromUri] int? id)
//         {
//             return db.Producto.FirstOrDefault(c => c.ProductoId == id);
//         }

//         [HttpGet]
//         [Route("Comidas/GetAllC")]
//         public IQueryable<Producto> GetAllC()
//         {

//             return db.Producto.Where(p => p.CategoriaId == 3 || p.CategoriaId == 4
//             || p.CategoriaId == 5 || p.CategoriaId == 6
//             || p.CategoriaId == 7 || p.CategoriaId == 8);
//         }

//         [HttpGet]
//         [Route("Bebidas/GetAllB")]
//         public IQueryable<Producto> GetAllB()
//         {
//             return db.Producto.Where(p => p.CategoriaId == 10);
//         }
//         [HttpGet]
//         [Route("Productos/GetByCategory")]
//         public IQueryable<ProductOutDto> GetByCategory([FromUri] string categoriaId)
//         {
//             int? categoriaIdConvertida = ConvertirDeDescripcionAId(categoriaId);
//             string nombreDelProcedimiento = "ObtenerProductoPorCategoria";
//             var result = new List<ProductOutDto>();
//             using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SDLTDb"].ToString()))
//             {
//                 connection.Open();
//                 using (SqlCommand command = new SqlCommand(nombreDelProcedimiento, connection))
//                 {
//                     command.CommandType = CommandType.StoredProcedure;
//                     command.Parameters.Add(new SqlParameter("@CategoriaId", categoriaIdConvertida));
//                     using (SqlDataReader reader = command.ExecuteReader())
//                     {
//                         ProductOutDto dtoPOut;
//                         while (reader.Read())
//                         {
//                             dtoPOut = new ProductOutDto
//                             {
//                                 ProductoId = int.Parse(reader[0].ToString()),
//                                 Nombre = reader[1].ToString(),
//                                 Descripcion = reader[2].ToString(),
//                                 Precio = decimal.Parse(reader[3].ToString()),
//                                 CategoriaId = int.Parse(reader[4].ToString()),
//                                 Categoria = reader[5].ToString(),
//                                 Stock = int.Parse(reader[6].ToString()),
//                                 EstaActivo = bool.Parse(reader[7].ToString())
//                             };
//                             result.Add(dtoPOut);
//                         }
//                     }
//                     connection.Close();
//                     return result.AsQueryable();
//                 }
//             }
//             //return db.Producto.Where(p => p.CategoriaId == categoriaIdConvertida);

//         }
//         private int? ConvertirDeDescripcionAId(string categoriaIdInt) {
//             if (!int.TryParse(categoriaIdInt, out int categoriaIdConvertido))
//             {
//                 switch (categoriaIdInt)
//                 {
//                     case "Sopas":
//                         categoriaIdConvertido = (int)CategoriaEnum.Sopa;
//                         break;
//                     case "Carnes":
//                         categoriaIdConvertido = (int)CategoriaEnum.Carne;
//                         break;
//                     case "Entradas":
//                         categoriaIdConvertido = (int)CategoriaEnum.Entrada;
//                         break;
//                     case "Ensalada":
//                         categoriaIdConvertido = (int)CategoriaEnum.Ensalada;
//                         break;
//                     case "Pescados y Mariscos":
//                         categoriaIdConvertido = (int)CategoriaEnum.PescadosYMariscos;
//                         break;
//                     case "Postres":
//                         categoriaIdConvertido = (int)CategoriaEnum.Postre;
//                         break;
//                     case "Postres y Bebidas":
//                         categoriaIdConvertido = (int)CategoriaEnum.Postre;
//                         break;
//                     default:
//                         return null;
//                 }
//             }
//             return categoriaIdConvertido;
//         }
//         private Producto ConvertirDelDtoAProducto(ProductoDto productoDto, int? categoriaIdInt)
//         {
//             Producto elProducto = new Producto()
//             {
//                 ProductoId = productoDto.ProductoId,
//                 Nombre = productoDto.Nombre,
//                 Descripcion = productoDto.Descripcion,
//                 Precio = productoDto.Precio,
//                 CategoriaId = categoriaIdInt,
//                 Stock = productoDto.Stock,
//                 EstaActivo = productoDto.EstaActivo
//             };
//             return elProducto;
//         }

//         [HttpPost]
//         [Route("Productos/Create")]
//         //[Authorize(Roles ="Administrator")]
//         public async Task<IHttpActionResult> Create(ProductoDto productoDto)
//         {
//             int? categoriaId = ConvertirDeDescripcionAId(productoDto.CategoriaId);

//             Producto producto = ConvertirDelDtoAProducto(productoDto, categoriaId);

//             db.Producto.Add(producto);
//             int? guardados = await db.SaveChangesAsync();
//             if(guardados > 0)
//             {
//                 return Content(HttpStatusCode.Created, producto);
//             }
//             else
//             {
//                 return Content(HttpStatusCode.InternalServerError
//                     , "No se pudo procesar correctamente la solicitud de creado. Intente otra vez");
//             }

//         }

//         [HttpPut]
//         [Route("Productos/Modify")]
//         //[Authorize(Roles ="Administrator")]
//         public async Task<IHttpActionResult> Modify([FromUri] int id, [FromBody] ProductoDto productoDto)
//         {
//             productoDto.ProductoId = id;
//             int? categoriaId = ConvertirDeDescripcionAId(productoDto.CategoriaId);

//             Producto producto = ConvertirDelDtoAProducto(productoDto, categoriaId);

//             if (producto.ProductoId == null)
//             {
//                 return Content(HttpStatusCode.BadRequest, "Id vacío");
//             }
//             db.Entry(producto).State = EntityState.Modified;
//             int cambios = await db.SaveChangesAsync();
//             if(cambios > 0)
//             {
//                 return Ok(producto);
//             }
//             else
//             {
//                 return Content(HttpStatusCode.InternalServerError, 
//                     "No se actualizó ninguna fila. Intente otra vez");
//             }

//         }
//         [HttpGet]
//         [Route("Productos/Delete{id}")]
//         [Authorize]
//         public async Task<IHttpActionResult> Delete([FromUri] int? id)
//         {
//             if (id == null)
//             {
//                 return Content(HttpStatusCode.BadRequest, "Id vacío");
//             }
//             Producto producto = await db.Producto.FindAsync(id);
//             if (producto == null)
//             {
//                 return Content(HttpStatusCode.NotFound, "Producto no encontrado");
//             }
//             return Content(HttpStatusCode.OK, db.Producto.Remove(producto));
//         }
//     }
// }
//         }

//         [HttpPut]
//         [Route("Productos/Modify")]
//         //[Authorize(Roles ="Administrator")]
//         public async Task<IHttpActionResult> Modify([FromUri] int id, [FromBody] ProductoDto productoDto)
//         {
//             productoDto.ProductoId = id;
//             int? categoriaId = ConvertirDeDescripcionAId(productoDto.CategoriaId);

//             Producto producto = ConvertirDelDtoAProducto(productoDto, categoriaId);

//             if (producto.ProductoId == null)
//             {
//                 return Content(HttpStatusCode.BadRequest, "Id vacío");
//             }
//             db.Entry(producto).State = EntityState.Modified;
//             int cambios = await db.SaveChangesAsync();
//             if(cambios > 0)
//             {
//                 return Ok(producto);
//             }
//             else
//             {
//                 return Content(HttpStatusCode.InternalServerError, 
//                     "No se actualizó ninguna fila. Intente otra vez");
//             }

//         }
//         [HttpGet]
//         [Route("Productos/Delete{id}")]
//         [Authorize]
//         public async Task<IHttpActionResult> Delete([FromUri] int? id)
//         {
//             if (id == null)
//             {
//                 return Content(HttpStatusCode.BadRequest, "Id vacío");
//             }
//             Producto producto = await db.Producto.FindAsync(id);
//             if (producto == null)
//             {
//                 return Content(HttpStatusCode.NotFound, "Producto no encontrado");
//             }
//             return Content(HttpStatusCode.OK, db.Producto.Remove(producto));
//         }
//     }
// }