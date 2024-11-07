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
    public class TaskViewModel : INotifyPropertyChanged
    {
        private readonly FirebaseClient firebaseClient;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler TareaGuardada;

        // Lista observable de tareas
        public ObservableCollection<Tasks> Tareas { get; set; } = new ObservableCollection<Tasks>();

        // Lista observable de categorías
        public ObservableCollection<Categoria> Categorias { get; set; } = new ObservableCollection<Categoria>();

        private string title;
        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private DateTime dateStart;
        public DateTime DateStart
        {
            get => dateStart;
            set
            {
                if (dateStart != value)
                {
                    dateStart = value;
                    OnPropertyChanged(nameof(DateStart));
                }
            }
        }

        private DateTime dateEnd;
        public DateTime DateEnd
        {
            get => dateEnd;
            set
            {
                if (dateEnd != value)
                {
                    dateEnd = value;
                    OnPropertyChanged(nameof(DateEnd));
                }
            }
        }

        private Categoria categoriaSeleccionada;
        public Categoria CategoriaSeleccionada
        {
            get => categoriaSeleccionada;
            set
            {
                if (categoriaSeleccionada != value)
                {
                    categoriaSeleccionada = value;
                    OnPropertyChanged(nameof(CategoriaSeleccionada));
                }
            }
        }

        public ICommand GuardarTareaCommand { get; }

        public TaskViewModel()
        {
            firebaseClient = new FirebaseClient("https://todolist-2f835-default-rtdb.firebaseio.com/");
            DateStart = DateTime.Today;
            DateEnd = DateTime.Today;
            GuardarTareaCommand = new Command(async () => await GuardarTarea());

            // Cargar las tareas y las categorías desde Firebase al inicializar el ViewModel
            _ = CargarTareas();
            _ = CargarCategorias();
        }

        private async Task GuardarTarea()
        {
            var nuevaTarea = new Tasks
            {
                Title = this.Title,
                Description = this.Description,
                DateStart = this.DateStart,
                DateEnd = this.DateEnd,
                IsComplete = false,
                Categoria = this.CategoriaSeleccionada
            };

            await firebaseClient.Child("Tareas").PostAsync(nuevaTarea);

            Tareas.Add(nuevaTarea);

            // Limpiar los campos después de guardar la tarea
            Title = string.Empty;
            Description = string.Empty;
            DateStart = DateTime.Today;
            DateEnd = DateTime.Today;
            CategoriaSeleccionada = null;

            // Invocar el evento para notificar que la tarea se ha guardado
            TareaGuardada?.Invoke(this, EventArgs.Empty);
        }

        private async Task CargarTareas()
        {
            var tareas = await firebaseClient.Child("Tareas").OnceAsync<Tasks>();

            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea.Object);
            }
        }

        private async Task CargarCategorias()
        {
            var categorias = await firebaseClient.Child("Categorias").OnceAsync<Categoria>();

            foreach (var categoria in categorias)
            {
                Categorias.Add(categoria.Object);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
