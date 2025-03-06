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
    public class clsInsertarProductoVM : INotifyPropertyChanged
    {
        #region Atributos
        private string nombre;
        private float precio;
        private int stock;
        private DelegateCommand insertarCommand;
        private DelegateCommand volverCommand;
        private bool showError;
        private bool showContent;
        private string error;
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    nombre = value;
                    insertarCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public float Precio
        {
            get { return precio; }
            set
            {
                precio = value;
                insertarCommand.RaiseCanExecuteChanged();
            }
        }

        public int Stock
        {
            get { return stock; }
            set
            {
                stock = value;
                insertarCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand InsertarCommand
        {
            get { return insertarCommand; }
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
        public clsInsertarProductoVM()
        {
            insertarCommand = new DelegateCommand(insertarCommandExecuted, insertarCommandCanExecute);
            volverCommand = new DelegateCommand(volverCommandExecuted);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Función que inserta un producto en la base de datos
        /// <br></br>
        /// Pre: Todos los campos rellenos
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        public async void insertarCommandExecuted()
        {
            try
            {
                clsProducto producto = new clsProducto(nombre, precio, stock);

                HttpStatusCode code = await clsManejadora.insertaProducto(producto);

                if (code == HttpStatusCode.OK) { 
                    Shell.Current.GoToAsync("///listadoProductos");
                } else
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
        /// Función que comprueba cuando puede mostrarse el command
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        /// <returns></returns>
        public bool insertarCommandCanExecute()
        {
            bool canExecute = false;

            if (!string.IsNullOrEmpty(nombre) && precio >= 0 && stock >= 0)
            {
                canExecute = true;
            }

            return canExecute;
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
