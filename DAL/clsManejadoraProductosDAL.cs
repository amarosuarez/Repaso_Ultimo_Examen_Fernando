using ENT;

namespace DAL
{
    public class clsManejadoraProductosDAL
    {

        public static List<clsProducto> listaProductos = new List<clsProducto>()
        {
            new clsProducto(1, "Coca-Cola", 1.2f, 20),
            new clsProducto(2, "Doritos", 1.5f, 50),
            new clsProducto(3, "Filipinos", 1.9f, 100),
            new clsProducto(4, "Panini", 0.8f, 200),
            new clsProducto(5, "Pepsi", 0.79f, 15),
        };

        /// <summary>
        /// Está función devuelve todos los productos
        /// </summary>
        /// <returns>Lista de productos</returns>
        public static List<clsProducto> obtenerProductosCompletoDAL()
        {
            return listaProductos;
        }

        /// <summary>
        /// Esta función obtiene un producto dado su id
        /// </summary>
        /// <param name="id">Id del producto</param>
        /// <returns>Producto</returns>
        public static clsProducto obtenerProductoPorIdDAL(int id)
        {
            return listaProductos.Find(p => p.Id == id);
        }

        /// <summary>
        /// Esta función añade un producto
        /// </summary>
        /// <param name="producto">Producto a añadir</param>
        /// <returns>Se ha añadido o no</returns>
        public static bool insertarProductoDAL(clsProducto producto)
        {
            bool insertado = false;
            int id = 1;

            // Comprobamos que no exista ya un producto con el mismo nombre
            if (!listaProductos.Contains(producto)) {
                // Busco el último índice
                int last = listaProductos.Count - 1;

                // Si hay al menos un producto, le sumo 1
                if (last > -1)
                {
                    // Obtengo el id del último producto y le sumo 1
                    id = listaProductos[last].Id + 1;
                }

                // Le añado el id al producto
                producto.Id = id;
                
                // Añado el producto a la lista
                listaProductos.Add(producto);
                insertado = true;
            }

            return insertado;
        }

        /// <summary>
        /// Esta función elimina un producto de la lista dado su id<br>
        /// Pre: El ID debe ser mayor que 0</br>
        /// </summary>
        /// <param name="idProducto">Id del producto a eliminar</param>
        /// <returns>Se ha borrado o no</returns>
        public static bool eliminarProductoDAL(int idProducto)
        {
            // Obtenemos el producto, ya que lo necesitamos para la función remove
            clsProducto producto = obtenerProductoPorIdDAL(idProducto);

            // Remove, primero busca si el producto está en la lista y si está, lo borra y devuelve true
            // En caso contrario, devuelve false
            return listaProductos.Remove(producto);
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
        public static bool editarProductoDAL(int idProducto, clsProducto productoEditado)
        {
            bool editado = false;

            // Comprobamos que el nuevo producto no sea null
            if (productoEditado != null) {
                // Si el id del productoEditado es 0, le ponemos el idProducto
                if (productoEditado.Id == 0)
                {
                    productoEditado.Id = idProducto;
                }

                int index = listaProductos.FindIndex(p => p.Id == idProducto);

                // Si no lo ha encontrado, el indice es -1
                if (index != -1)
                {
                    listaProductos[index] = productoEditado;
                    editado = true;
                }
            }

            return editado;
        }
    }
}
