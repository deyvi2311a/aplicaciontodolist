using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using aplicaciontodolist.Models;
using System.Reactive.Linq; // Asegúrate de agregar esta referencia para el manejo de observables
using System;
using Firebase.Database.Streaming;

namespace aplicaciontodolist.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly FirebaseClient firebaseClient;
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Tasks> tareas;
        public ObservableCollection<Tasks> Tareas
        {
            get => tareas;
            set
            {
                if (tareas != value)
                {
                    tareas = value;
                    OnPropertyChanged(nameof(Tareas));
                }
            }
        }

        public MainPageViewModel()
        {
            firebaseClient = new FirebaseClient("https://todolist-2f835-default-rtdb.firebaseio.com/");
            Tareas = new ObservableCollection<Tasks>();

            // Llamamos a CargarLista para suscribirnos a cambios en tiempo real
            CargarLista();
        }

        // Método para suscribirnos a los cambios en tiempo real en Firebase
        private void CargarLista()
        {
            firebaseClient
                .Child("Tareas")
                .AsObservable<Tasks>()
                .Subscribe(tarea =>
                {
                    if (tarea.Object != null)
                    {
                        var tareaExistente = Tareas.FirstOrDefault(t => t.Id == tarea.Object.Id); // Usa Id para comparar tareas

                        if (tarea.EventType == FirebaseEventType.InsertOrUpdate)
                        {
                            if (tareaExistente == null)
                            {
                                // Agregar nueva tarea si no existe
                                Tareas.Add(tarea.Object);
                            }
                            else
                            {
                                // Actualizar las propiedades de la tarea existente
                                tareaExistente.Title = tarea.Object.Title;
                                tareaExistente.Description = tarea.Object.Description;
                                tareaExistente.IsComplete = tarea.Object.IsComplete;
                                // O cualquier otra propiedad que necesites actualizar
                            }
                        }
                        else if (tarea.EventType == FirebaseEventType.Delete)
                        {
                            if (tareaExistente != null)
                            {
                                Tareas.Remove(tareaExistente); // Eliminar tarea de la lista
                            }
                        }
                    }
                });
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
