using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
namespace aplicaciontodolist
{
    internal class Login
    {
        public static FirebaseAuthClient ConectarFirebase()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyCiquMTr2zaw0YfuIliruubbXqZ56Z5Az4",
                AuthDomain = "bdnosql-f1195.web.app",
                Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                },
            };

            var client = new FirebaseAuthClient(config);

            return client;
        }

        public async Task<UserCredential> CargarUsuario(string Email, string Password)
        {
            var cliente = ConectarFirebase();
            var userCredential = await cliente.SignInWithEmailAndPasswordAsync(Email, Password);
            return userCredential;
        }

        public async Task<UserCredential> CrearUsuario(string Email, string Password, string Nombre)
        {
            var cliente = ConectarFirebase();
            var userCredential = await cliente.CreateUserWithEmailAndPasswordAsync(Email, Password, Nombre);
            return userCredential;
        }
    }
}
