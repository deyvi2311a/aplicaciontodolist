namespace aplicaciontodolist.Views;

public partial class HomePageLogin : ContentPage
{
	public HomePageLogin()
	{
		InitializeComponent();
	}

    private async void ButtonLogin_Clicked(object sender, EventArgs e)
    {



        string email = emailEntry.Text;
        string password = passwordEntrada.Text;

        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Por favor, ingrese un correo electrónico.", "Ok");
            return;
        }

        // Validación: Verificar si el campo de contraseña está vacío
        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor, ingrese una contraseña.", "Ok");
            return;
        }

        try
        {
            Login conexionFirebase = new Login();
            var Credenciales = await conexionFirebase.CargarUsuario(email, password);
            var Uid = Credenciales.User.Uid;

            await DisplayAlert("Inicio de Sesión Exitoso", null, "Ok");

            //navegar a la página de home
            await Navigation.PushAsync(new Views.MainPage());
        }
        catch (Exception ex)
        {
            // Manejo de excepciones: Mostrar mensaje de error si ocurre una excepción
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Ok");
        }
    }

    private async void ButtonRegister_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.HomePageRegister());
    }
}