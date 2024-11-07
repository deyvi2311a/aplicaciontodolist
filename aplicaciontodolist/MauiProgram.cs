using aplicaciontodolist.Models;
using Firebase.Database;
using Microsoft.Extensions.Logging;
using Firebase.Database.Query;

namespace aplicaciontodolist
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            Registrar();
            return builder.Build();
        }
        public static void Registrar()
        {
            FirebaseClient client = new FirebaseClient("https://todolist-2f835-default-rtdb.firebaseio.com/");
            var categorias = client.Child("Categorias").OnceAsync<Categoria>();
            if (categorias.Result.Count == 0)
            {
                client.Child("Categorias").PostAsync(new Categoria { Nombre = "Trabajo" });
                client.Child("Categorias").PostAsync(new Categoria { Nombre = "Escuela" });
                client.Child("Categorias").PostAsync(new Categoria { Nombre = "Personal" });
                client.Child("Categorias").PostAsync(new Categoria { Nombre = "Otro" });
            }
        }
}
}