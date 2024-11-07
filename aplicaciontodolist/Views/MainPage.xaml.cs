using aplicaciontodolist.ViewModels;
using aplicaciontodolist.Models;
using Microsoft.Maui.Controls;
using System.Reflection.Metadata;
using Firebase.Database;
using System.Collections.ObjectModel;

namespace aplicaciontodolist.Views
{
    public partial class MainPage : ContentPage
    {

        private readonly MainPageViewModel mainViewModel;

        public MainPage()
        {
            InitializeComponent();

            mainViewModel = new MainPageViewModel();
            BindingContext = mainViewModel;

        }

        private async void OnClickedNavigate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarTarea());
        }

        private async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Tasks selectedTask)
            {
                await Task.Delay(100); // Espera breve antes de navegar
                await Navigation.PushAsync(new DetallesTarea(selectedTask));
            }
             ((CollectionView)sender).SelectedItem = null;

        }

    }
}
