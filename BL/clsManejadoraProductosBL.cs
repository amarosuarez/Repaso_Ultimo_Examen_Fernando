using DAL;
using ENT;

namespace BL
{
    public class clsManejadoraProductosBL
    {
        /// <summary>
        /// Está función devuelve todos los productos
        /// </summary>
        /// <returns>Lista de productos</returns>
        public static List<clsProducto> obtenerProductosCompletoBL()
        {
            return clsManejadoraProductosDAL.obtenerProductosCompletoDAL();
        }

        /// <summary>
        /// Esta función obtiene un producto dado su id
        /// </summary>
        /// <param name="id">Id del producto</param>
        /// <returns>Producto</returns>
        public static clsProducto obtenerProductoPorIdBL(int id)
        {
            return clsManejadoraProductosDAL.obtenerProductoPorIdDAL(id);
        }

        /// <summary>
        /// Esta función añade un producto
        /// </summary>
        /// <param name="producto">Producto a añadir</param>
        /// <returns>Se ha añadido o no</returns>
        public static bool insertarProductoBL(clsProducto producto)
        {
            return clsManejadoraProductosDAL.insertarProductoDAL(producto);
        }

        /// <summary>
        /// Esta función elimina un producto de la lista dado su id<br>
        /// Pre: El ID debe ser mayor que 0</br>
        /// </summary>
        /// <param name="idProducto">Id del producto a eliminar</param>
        /// <returns>Se ha borrado o no</returns>
        public static bool eliminarProductoBL(int idProducto)
        {
            return clsManejadoraProductosDAL.eliminarProductoDAL(idProducto);
        }

        /// <summary>
        /// Esta función actualiza un producto dado su id y el producto nuevo editado
        /// </summary>
        /// <param name="idProducto">
        /// ID del producto a editar 
        /// (Puede no necesitarlo si sabemos que el ID no va a cambiar.
        /// En este caso, cogeriamos el ID del productoEditado)
        /// </param>
        /// <param name="productoEditado">Producto ya editado</param>
        /// <returns>Devuelve si se ha podido editar el producto o no</returns>
        public static bool editarProductoBL(int idProducto, clsProducto productoEditado)
        {
            return clsManejadoraProductosDAL.editarProductoDAL(idProducto, productoEditado);
        }
    }
}
