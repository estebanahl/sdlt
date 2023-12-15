// using sdlt.DTOs;
// using sdlt.Models;
// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Data.Entity;
// using System.Data.SqlClient;
// using System.Linq;
// using System.Net;
// using System.Threading.Tasks;
// using System.Web;
// using System.Web.Http;

// namespace sdlt.Controllers
// {
//     [RoutePrefix("api")]
//     public class ProductosController : ApiController
//     {
//         readonly SDLTDb db = new SDLTDb();

//         [HttpGet]
//         [Route("Productos/GetAll")]
//         public IQueryable<ProductOutDto> GetAll()
//         {
//             string nombreDelProcedimiento = "ObtenerProductos";
//             var result = new List<ProductOutDto>();
//             using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SDLTDb"].ToString()))
//             {
//                 connection.Open();
//                 using (SqlCommand command = new SqlCommand(nombreDelProcedimiento, connection))
//                 {
//                     command.CommandType = CommandType.StoredProcedure;
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
//         }
//     }
// }
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