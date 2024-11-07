using aplicaciontodolist.Models;
using aplicaciontodolist.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace aplicaciontodolist.Views
{
    public partial class DetallesTarea : ContentPage
    {
        private readonly TaskDetailViewModel viewModel;

        public DetallesTarea(Tasks selectedTask)
        {
            InitializeComponent();
            // Inicializa el ViewModel con la tarea seleccionada
            viewModel = new TaskDetailViewModel(selectedTask);
            BindingContext = viewModel;
        }

        public async void OnButtonEditar(object sender, EventArgs e)
        {
            var viewModel = BindingContext as TaskDetailViewModel;
            if (viewModel != null && viewModel.Task != null)
            {
                await Navigation.PushAsync(new EditarTarea(viewModel.Task));
            }
        }
    

        public async void OnButtonEliminar(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); 
        }
    }
}
