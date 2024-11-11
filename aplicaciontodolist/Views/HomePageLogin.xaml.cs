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
            await DisplayAlert("Error", "Por favor, ingrese un correo electr�nico.", "Ok");
            return;
        }

        // Validaci�n: Verificar si el campo de contrase�a est� vac�o
        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor, ingrese una contrase�a.", "Ok");
            return;
        }

        try
        {
            Login conexionFirebase = new Login();
            var Credenciales = await conexionFirebase.CargarUsuario(email, password);
            var Uid = Credenciales.User.Uid;

            await DisplayAlert("Inicio de Sesi�n Exitoso", null, "Ok");

            //navegar a la p�gina de home
            await Navigation.PushAsync(new Views.MainPage());
        }
        catch (Exception ex)
        {
            // Manejo de excepciones: Mostrar mensaje de error si ocurre una excepci�n
            await DisplayAlert("Error", $"Ocurri� un error: {ex.Message}", "Ok");
        }
    }

    private async void ButtonRegister_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.HomePageRegister());
    }
}