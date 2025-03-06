using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ENT;
using SERVICES;
using ViewModels.Utils;

namespace MAUI.ViewModels
{
    public class clsListadoVM : INotifyPropertyChanged
    {
        #region Atributos
        private clsProducto productoSeleccionado;
        private ObservableCollection<clsProducto> productos; 
        private DelegateCommand insertarCommand;
        private DelegateCommand editarCommand;
        private DelegateCommand borrarCommand;
        private bool showError;
        private bool showContent;
        private string error;
        #endregion

        #region Propiedades
        public clsProducto ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set
            {
                if (value != null)
                {
                    productoSeleccionado = value;

                    NotifyPropertyChanged();
                    editarCommand.RaiseCanExecuteChanged();
                    borrarCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public ObservableCollection<clsProducto> Productos { 
            get { return productos; } 
        }

        public DelegateCommand InsertarCommand
        {
            get { return insertarCommand; }
        }
        public DelegateCommand EditarCommand
        {
            get { return editarCommand; }
        }
        public DelegateCommand BorrarCommand
        {
            get { return borrarCommand; }
        }
        public bool ShowError
        {
            get { return showError; }
        }
        public bool ShowContent
        {
            get { return !showError; }
        }
        public string Error
        {
            get { return error; }
        }
        #endregion

        #region Constructores
        public clsListadoVM()
        {
            insertarCommand = new DelegateCommand(insertarCommandExecuted);
            editarCommand = new DelegateCommand(editarCommandExecuted, editarCommandCanExecute);
            borrarCommand = new DelegateCommand(borrarCommandExecuted, borrarCommandCanExecute);

            Task.Run(async () =>
            {
                //await MainThread.InvokeOnMainThreadAsync(async () => await _connection.StartAsync());
                productos = await cargarListado();
                NotifyPropertyChanged(nameof(Productos));
            });
        }
        #endregion

        #region Commands
        /// <summary>
        /// Función que va a la pantalla insertar
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        public async void insertarCommandExecuted()
        {
            await Shell.Current.GoToAsync("///insertarProducto");
        }

        /// <summary>
        /// Función que va a la pantalla editar
        /// <br></br>
        /// Pre: Producto no null
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        public async void editarCommandExecuted()
        {
            if (productoSeleccionado != null)
            {
                clsProducto producto = await clsManejadora.obtenerProductoPorId(productoSeleccionado.Id);
                var queryParams = new Dictionary<string, object>
                {
                    { "producto", producto}
                };

                await Shell.Current.GoToAsync("///editarProducto", queryParams);
            }
        }

        /// <summary>
        /// Función que comprueba cuando puede mostrarse el command
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        /// <returns>Devuelve si puede mostrarse o no</returns>
        public bool editarCommandCanExecute()
        {
            bool canExecute = false;

            if (productoSeleccionado != null)
            {
                canExecute = true;
            }

            return canExecute;
        }

        /// <summary>
        /// Función que elimina a un producto de la BD
        /// <br></br>
        /// Pre: Producto no null
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        public async void borrarCommandExecuted()
        {
            // Mostrar un cuadro de confirmación
            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirmar Borrado",
                "¿Estás seguro de que deseas borrar a " + productoSeleccionado.Nombre + "?",
                "Sí",
                "No");

            // Si el usuario confirma, ejecutar la lógica de borrado
            if (isConfirmed)
            {
                HttpStatusCode code = await clsManejadora.borrarProducto(productoSeleccionado.Id);

                if (code == HttpStatusCode.OK)
                {
                    RecargarProductos();
                }
                else
                {
                    error = "Ha ocurrido un error";
                    showError = true;
                    NotifyPropertyChanged("Error");
                    NotifyPropertyChanged("ShowError");
                }
            }
        }

        /// <summary>
        /// Función que comprueba cuando puede mostrarse el command
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        /// <returns>Devuelve si puede mostrarse o no</returns>
        public bool borrarCommandCanExecute()
        {
            bool canExecute = false;

            if (productoSeleccionado != null)
            {
                canExecute = true;
            }

            return canExecute;
        }
        #endregion

        #region Metodos
        private async Task<ObservableCollection<clsProducto>> cargarListado()
        {
            ObservableCollection<clsProducto> productosObs = null;

            try
            {
                productosObs = new ObservableCollection<clsProducto>(await clsManejadora.obtenerListadoProductosCompletos());
            }
            catch (Exception e)
            {
                showError = true;
                error = "No se ha podido cargar el listado";
                NotifyPropertyChanged("ShowError");
                NotifyPropertyChanged("Error");
            }

            return productosObs;
        }

        public async Task RecargarProductos()
        {
            productos.Clear();  // Limpia la colección antes de recargar
            var nuevosProductos = await cargarListado();
            foreach (var producto in nuevosProductos)
            {
                productos.Add(producto);
            }
        }

        #endregion

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
