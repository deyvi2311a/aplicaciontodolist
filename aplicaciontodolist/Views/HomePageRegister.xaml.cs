namespace aplicaciontodolist.Views;

public partial class HomePageRegister : ContentPage
{
	public HomePageRegister()
	{
		InitializeComponent();
	}

    private async void ButtonLogin_Clicked(object sender, EventArgs e)
    {
        string name = nameEntrada.Text;
        string email = emailEntry.Text;
        string password = passwordEntrada.Text;

        try
        {
            Login conexionFirebase = new Login();
            var Credenciales = await conexionFirebase.CrearUsuario(email, password, name);
            var Uid = Credenciales.User.Uid;

            await DisplayAlert("Registro Exitoso", null, "Ok");

            //navegar a la página de home
            await Navigation.PushAsync(new Views.HomePageLogin());
        }
        catch (Exception ex)
        {
            // Manejo de excepciones: Mostrar mensaje de error si ocurre una excepción
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Ok");
        }
    }

    private async void ButtonRegister_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.MainPage());
    }

    private async void ButtonLoginNav_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.HomePageLogin());
    }
}