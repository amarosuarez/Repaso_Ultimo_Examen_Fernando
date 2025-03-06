namespace ENT
{
    public class clsProducto
    {
        #region Atributos
        private int id;
        private String nombre;
        private float precio;
        private int stock;
        #endregion

        #region Propiedades
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public String Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    nombre = value;
                }
            }
        }

        public float Precio
        {
            get
            {
                return precio;
            }

            set
            {
                precio = value;
            }
        }

        public int Stock
        {
            get
            {
                return stock;
            }
            set
            {
                stock = value;
            }
        }
        #endregion

        #region Constructores
        public clsProducto() { }

        public clsProducto(String nombre, float precio, int stock)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                this.nombre = nombre;
            }

            this.precio = precio;
            this.stock = stock;
        }

        public clsProducto(int id, String nombre, float precio, int stock) : this(nombre, precio, stock) { 
            this.id = id;
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Esta función comprueba si dos productos son iguales por su nombre.
        /// Necesita el override.
        /// </summary>
        /// <param name="obj">Objeto a comparar</param>
        /// <returns>Son iguales o no</returns>
        override
        public bool Equals(Object obj)
        { 
            // ¡IMPORTANTE! Castearlo
            clsProducto prod = (clsProducto) obj;

            // Compara ambos nombres en minusculas y devuelve true o false
            return prod.nombre.ToLower().Equals(this.nombre.ToLower());
        }
        #endregion
    }
}
