using System;
using System.Collections.Generic;
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

    [QueryProperty(nameof(Producto), "producto")]
    public class clsEditarProductoVM : INotifyPropertyChanged
    {
        #region Atributos
        private clsProducto producto;
        private DelegateCommand editarCommand;
        private DelegateCommand volverCommand;
        private bool showError;
        private bool showContent;
        private string error;
        #endregion

        #region Propiedades
        public clsProducto Producto
        {
            get { return producto; }
            set
            {
                producto = value;
                NotifyPropertyChanged(nameof(Producto));
            }
        }

        public DelegateCommand EditarCommand
        {
            get { return editarCommand; }
        }

        public DelegateCommand VolverCommand
        {
            get { return volverCommand; }
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

        #region Constructor
        public clsEditarProductoVM()
        {
            editarCommand = new DelegateCommand(editarCommandExecuted);
            volverCommand = new DelegateCommand(volverCommandExecuted);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Función que edita un producto en la base de datos
        /// <br></br>
        /// Pre: Todos los campos rellenos
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        public async void editarCommandExecuted()
        {
            try
            {
                HttpStatusCode code = await clsManejadora.actualizarProducto(producto);

                if (code == HttpStatusCode.OK)
                {
                    Shell.Current.GoToAsync("///listadoProductos");
                }
                else
                {
                    error = "Ha ocurrido un error";
                    showError = true;
                    NotifyPropertyChanged("Error");
                    NotifyPropertyChanged("ShowError");
                }

            }
            catch (Exception ex)
            {
                showError = true;
                error = ex.Message;
                NotifyPropertyChanged("ShowError");
                NotifyPropertyChanged("Error");
            }

        }

        /// <summary>
        /// Función que vuelve al listado
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        public async void volverCommandExecuted()
        {
            await Shell.Current.GoToAsync("///listadoProductos");
        }
        #endregion

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")

        {

            PropertyChanged?.Invoke(this, new
            PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }
}
