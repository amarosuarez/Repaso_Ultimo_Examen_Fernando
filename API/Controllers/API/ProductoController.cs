using BL;
using ENT;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // GET: api/<ProductoController>
        /// <summary>
        /// Función que obtiene el listado completo de productos
        /// </summary>
        /// <returns>Listado de productos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult salida;
            List<clsProducto> productos = new List<clsProducto>();

            try
            {
                productos = clsManejadoraProductosBL.obtenerProductosCompletoBL();

                if (productos.Count > 0)
                {
                    salida = Ok(productos);
                }
                else
                {
                    salida = NoContent();
                }
            }
            catch
            {
                salida = BadRequest();
            }

            return salida;
        }

        // GET api/<ProductoController>/5

        /// <summary>
        /// Función que obtiene un producto dado su ID
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns>Producto</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IActionResult salida;
            clsProducto producto = null;

            try
            {
                producto = clsManejadoraProductosBL.obtenerProductoPorIdBL(id);

                if (producto != null)
                {
                    salida = Ok(producto);
                }
                else
                {
                    salida = NoContent();
                }
            }
            catch
            {
                salida = BadRequest();
            }

            return salida;
        }

        // POST api/<ProductoController>

        /// <summary>
        /// Esta función recibe un producto y lo añade
        /// </summary>
        /// <param name="producto">Producto</param>
        [HttpPost]
        public IActionResult Post(clsProducto producto)
        {
            IActionResult salida;
            bool added;

            try
            {
                added = clsManejadoraProductosBL.insertarProductoBL(producto);

                if (added)
                {
                    salida = Ok(added);
                }
                else
                {
                    salida = NotFound("No se ha podido insertar el producto. Es posible que ya exista uno con ese nombre");
                }
            }
            catch (Exception e)
            {
                salida = BadRequest(e.Message);
            }

            return salida;
        }

        // PUT api/<ProductoController>/5

        /// <summary>
        /// Esta función edita un producto dado su id y el producto modificado
        /// </summary>
        /// <param name="id">ID del producto a modificar</param>
        /// <param name="productoModificado">Producto modificado</param>
        /// <returns>El producto se ha modificado o no</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, clsProducto productoModificado)
        {
            bool modified;
            IActionResult salida;

            try
            {
                modified = clsManejadoraProductosBL.editarProductoBL(id, productoModificado);

                if (modified)
                {
                    salida = Ok(modified);
                }
                else
                {
                    salida = NotFound("No se ha podido editar. Es posible que no exista ningún producto con ese ID");
                }
            }
            catch (Exception e)
            {
                salida = BadRequest(e.Message);
            }

            return salida;
        }

        // DELETE api/<ProductoController>/5

        /// <summary>
        /// Función que elimina un producto dado un ID
        /// </summary>
        /// <param name="id">Id del producto a eliminar</param>
        /// <returns>Se ha eliminado o no</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted;
            IActionResult salida;

            try
            {
                deleted = clsManejadoraProductosBL.eliminarProductoBL(id);

                if (deleted)
                {
                    salida = Ok(deleted);
                }
                else
                {
                    salida = NotFound("No se ha encontrado ningún producto con ese ID.");
                }
            }
            catch
            {
                salida = BadRequest();
            }

            return salida;
        }
    }
}
