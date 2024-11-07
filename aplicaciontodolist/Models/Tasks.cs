using System;
using System.ComponentModel;
using aplicaciontodolist.Models;

public class Tasks : INotifyPropertyChanged
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    private bool isComplete;
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }

    // Implementación de IsComplete con notificación de cambio
    public bool IsComplete
    {
        get => isComplete;
        set
        {
            if (isComplete != value)
            {
                isComplete = value;
                OnPropertyChanged(nameof(IsComplete));
            }
        }
    }

    public Categoria Categoria { get; set; }

    // Propiedad para mostrar solo el nombre de la categoría
    public string CategoriaNombre => Categoria?.Nombre ?? "Sin Categoría";

    // Evento de INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    // Método para notificar cambios de propiedad
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
