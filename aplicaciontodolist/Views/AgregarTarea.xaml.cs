using aplicaciontodolist.ViewModels;
using Microsoft.Maui.Controls;

namespace aplicaciontodolist.Views
{
    public partial class AgregarTarea : ContentPage
    {
        private TaskViewModel viewModel;

        public AgregarTarea()
        {
            InitializeComponent();
            viewModel = new TaskViewModel();
            this.BindingContext = viewModel;
            viewModel.TareaGuardada += OnTareaGuardada;
        }

        private async void OnTareaGuardada(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
