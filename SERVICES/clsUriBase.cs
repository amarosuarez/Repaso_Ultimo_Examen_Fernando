using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class clsUriBase
    {
        /// <summary>
        /// Función que devuelve la ruta a la api
        /// </summary>
        /// <returns>Ruta a la API</returns>
        public static String getBase()
        {
            return "http://localhost:5127/api/";
        }

    }
}
