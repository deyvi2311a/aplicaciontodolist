using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using aplicaciontodolist.Models;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace aplicaciontodolist.ViewModels
{
    public class TaskDetailViewModel : INotifyPropertyChanged
    {
        private readonly FirebaseClient firebaseClient;
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnTaskUpdated; // Evento para notificar actualización de la lista

        public ObservableCollection<Tasks> Tareas { get; set; } = new ObservableCollection<Tasks>();

        private Tasks _task;
        public Tasks Task
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged(nameof(Task));
            }
        }

        public ICommand GuardarCambiosCommand { get; }
        public ICommand EliminarTareaCommand { get; }

        public TaskDetailViewModel(Tasks selectedTask)
        {
            firebaseClient = new FirebaseClient("https://todolist-2f835-default-rtdb.firebaseio.com/");
            Task = selectedTask;

            GuardarCambiosCommand = new Command(async () => await GuardarCambiosAsync());
            EliminarTareaCommand = new Command(async () => await EliminarTareaAsync());

            // Cargar tareas desde Firebase al inicializar el ViewModel
            _ = CargarTareas();
        }

        private async Task GuardarCambiosAsync()
        {
            // Actualiza la tarea en Firebase
            await firebaseClient
                .Child("Tasks")
                .Child(Task.Id) // Asegúrate de que Task tenga un Id
                .PutAsync(Task);

            // Notifica que la tarea ha sido actualizada para que la lista se refresque
            OnTaskUpdated?.Invoke();

            // Regresa a la MainPage
            await Shell.Current.GoToAsync("..");
        }

        private async Task EliminarTareaAsync()
        {
            try
            {
                // Elimina la tarea en Firebase
                await firebaseClient
                    .Child("Tareas") // Asegúrate de que coincida con el nodo en Firebase
                    .Child(Task.Id)  // Usa el identificador correcto de la tarea
                    .DeleteAsync();

                // Notifica que la tarea ha sido eliminada para actualizar la lista
                OnTaskUpdated?.Invoke();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al eliminar la tarea: " + ex.Message);
            }

            // Regresa a la MainPage
            await Shell.Current.GoToAsync("..");
        }


        private async Task CargarTareas()
        {
            var tareas = await firebaseClient.Child("Tasks").OnceAsync<Tasks>();

            Tareas.Clear(); // Limpiar la lista antes de cargar las tareas
            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea.Object);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
