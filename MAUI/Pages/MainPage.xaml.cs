using MAUI.ViewModels;

namespace MAUI
{
    public partial class MainPage : ContentPage
    {
        private bool primeraVez = true;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is clsListadoVM viewModel)
            {
                if (primeraVez)
                {
                    primeraVez = false; // La primera vez no recarga
                }
                else
                {
                    await viewModel.RecargarProductos(); // Solo recarga si ya estuvo antes en la vista
                }
            }
        }


    }

}
