using aplicaciontodolist.ViewModels;
using aplicaciontodolist.Models;
using Microsoft.Maui.Controls;

namespace aplicaciontodolist.Views
{
    public partial class EditarTarea : ContentPage
    {
        public EditarTarea(Tasks selectedTask)
        {
            InitializeComponent();

            var viewModel = new TaskDetailViewModel(selectedTask);
            BindingContext = viewModel;

            viewModel.OnTaskUpdated += async () => await Shell.Current.GoToAsync("..");
        }
    }
}
