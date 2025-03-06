using System.Net;
using ENT;
using Newtonsoft.Json;

namespace SERVICES
{
    public class clsManejadora
    {
        /// <summary>
        /// Función que se conecta a la API y devuelve un listado con todos los productos
        /// </summary>
        /// <returns>Listado de productos</returns>
        public static async Task<List<clsProducto>> obtenerListadoProductosCompletos()
        {
            //Pido la cadena de la Uri al método estático

            string miCadenaUrl = clsUriBase.getBase();

            Uri miUri = new Uri($"{miCadenaUrl}producto");

            List<clsProducto> listadoProductos = new List<clsProducto>();

            HttpClient mihttpClient;

            HttpResponseMessage miCodigoRespuesta;

            string textoJsonRespuesta;

            //Instanciamos el cliente Http

            mihttpClient = new HttpClient();

            try

            {

                miCodigoRespuesta = await mihttpClient.GetAsync(miUri);

                if (miCodigoRespuesta.IsSuccessStatusCode)

                {

                    textoJsonRespuesta = await mihttpClient.GetStringAsync(miUri);

                    mihttpClient.Dispose();

                    //JsonConvert necesita using Newtonsoft.Json;

                    //Es el paquete Nuget de Newtonsoft

                    listadoProductos =
                    JsonConvert.DeserializeObject<List<clsProducto>>(textoJsonRespuesta);

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return listadoProductos;
        }

        /// <summary>
        /// Función que devuelve un producto
        /// </summary>
        /// <returns>Producto</returns>
        public static async Task<clsProducto> obtenerProductoPorId(int idProducto)
        {
            //Pido la cadena de la Uri al método estático

            string miCadenaUrl = clsUriBase.getBase();

            Uri miUri = new Uri($"{miCadenaUrl}producto/{idProducto}");

            clsProducto producto = new clsProducto();

            HttpClient mihttpClient;

            HttpResponseMessage miCodigoRespuesta;

            string textoJsonRespuesta;

            //Instanciamos el cliente Http

            mihttpClient = new HttpClient();

            try

            {

                miCodigoRespuesta = await mihttpClient.GetAsync(miUri);

                if (miCodigoRespuesta.IsSuccessStatusCode)

                {

                    textoJsonRespuesta = await mihttpClient.GetStringAsync(miUri);

                    mihttpClient.Dispose();

                    //JsonConvert necesita using Newtonsoft.Json;

                    //Es el paquete Nuget de Newtonsoft

                    producto =
                    JsonConvert.DeserializeObject<clsProducto>(textoJsonRespuesta);

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return producto;
        }

        /// <summary>
        /// Esta función añade un nuevo producto
        /// </summary>
        /// <param name="producto">Producto a añadir</param>
        /// <returns>Devuelve si se ha añadido el producto o no</returns>
        public static async Task<HttpStatusCode> insertaProducto(clsProducto producto)

        {

            HttpClient mihttpClient = new HttpClient();

            string datos;

            HttpContent contenido;

            string miCadenaUrl = clsUriBase.getBase();

            Uri miUri = new Uri($"{miCadenaUrl}producto");

            //Usaremos el Status de la respuesta para comprobar si ha borrado

            HttpResponseMessage miRespuesta = new HttpResponseMessage();

            try

            {

                datos = JsonConvert.SerializeObject(producto);

                contenido = new StringContent(datos, System.Text.Encoding.UTF8, "application/json");

                miRespuesta = await mihttpClient.PostAsync(miUri, contenido);

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return miRespuesta.StatusCode;

        }

        /// <summary>
        /// Función que elimina un producto por su ID
        /// </summary>
        /// <param name="idProducto">ID del producto a eliminar</param>
        /// <returns>Devuelve el código de respuesta HTTP</returns>
        public static async Task<HttpStatusCode> borrarProducto(int idProducto)
        {
            string miCadenaUrl = clsUriBase.getBase();
            Uri miUri = new Uri($"{miCadenaUrl}producto/{idProducto}");

            using (HttpClient mihttpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage miRespuesta = await mihttpClient.DeleteAsync(miUri);
                    return miRespuesta.StatusCode;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Función que actualiza un producto
        /// </summary>
        /// <param name="producto">Producto con los datos actualizados</param>
        /// <returns>Devuelve el código de respuesta HTTP</returns>
        public static async Task<HttpStatusCode> actualizarProducto(clsProducto producto)
        {
            string miCadenaUrl = clsUriBase.getBase();
            Uri miUri = new Uri($"{miCadenaUrl}producto/{producto.Id}"); // Asegúrate de que clsProducto tenga una propiedad "Id"

            using (HttpClient mihttpClient = new HttpClient())
            {
                try
                {
                    string datos = JsonConvert.SerializeObject(producto);
                    HttpContent contenido = new StringContent(datos, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage miRespuesta = await mihttpClient.PutAsync(miUri, contenido);
                    return miRespuesta.StatusCode;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

}
