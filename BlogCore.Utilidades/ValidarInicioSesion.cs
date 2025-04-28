using Microsoft.AspNetCore.Identity;

namespace BlogCore.Utilidades
{
    public class ValidarInicioSesion<TUser> where TUser: class
    {
        private readonly UserManager<TUser> UserManager;
        private readonly SignInManager<TUser> signInManager;

        public ValidarInicioSesion(
            UserManager<TUser> managerUser,
            SignInManager<TUser> singIn
        ) 
        {
            UserManager = managerUser;
            signInManager = singIn;
        }

        public async Task<SignInResult> PasswordSingInAsync(string correo, string password,
            bool isPersistent, bool lockoutOnFailure)
        {
            var usuario = await UserManager.FindByEmailAsync(correo);

            if ( usuario == null )
            {
                return SignInResult.Failed;
            }

            return await signInManager.PasswordSignInAsync(usuario, password, isPersistent, lockoutOnFailure);
        }

    }
}
