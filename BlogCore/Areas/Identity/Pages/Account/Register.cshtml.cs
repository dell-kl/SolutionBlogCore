// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using BlogCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BlogCore.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        //tenemos la parte de RoleManager para la parte de nuestro roles
        private readonly RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        public InputModel Input { get; set; }

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            
            [Required(ErrorMessage = "Debes introducir un nombre de usuario")]
            [Display(Name = "Nombre de Usuario")]
            public string UserName { set; get; }

            [Required(ErrorMessage = "Debes introducir un nombre y apellido")]
            [Display(Name = "Introduce tu nombre")]
            public string Nombre { set; get; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Debes introducir un correo electronico")]
            [EmailAddress(ErrorMessage = "Introduce un correo electrónico válido")]
            [Display(Name = "Email")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Correo electrónico inválido")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Introduce una contraseña para tu perfil")]
            [StringLength(100, ErrorMessage = "La contraseña debe tener un maximo de 100 carácteres", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Debes ingresar nuevamente tu contraseña para confirmar")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar Contraseña")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden, intentalo otra vez.")]
            public string ConfirmPassword { get; set; }


            [StringLength(maximumLength: 10,MinimumLength = 10, ErrorMessage = "Debes introducir una cedula valida")]
            [Required(ErrorMessage = "Introduce un numero de cedula")]
            [Display(Name = "Cedula Identidad")]
            public string Cedula { set; get; }

            
            [Display(Name = "Rol")]
            public int? IDRol { set; get; }

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            bool autenticado = this.User.Identity.IsAuthenticated;
            Input.IDRol = !autenticado ? 3 : Input.IDRol;

            //tenemos que realizar la validacion para la parte del ROl que se debe introducir 

            if (ModelState.IsValid && Input.IDRol.HasValue)
            {
                
                var user = CreateUser();

                user.Nombres = Input.Nombre;
                user.Cedula = Input.Cedula;
                
                await _userManager.SetUserNameAsync(user, Input.UserName);
                await _userManager.SetEmailAsync(user, Input.Email);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    /* Nos verificamos si existen dicho roles del cual podemos encargarnos de crear 
                     * ==============================================
                     *  Usar -> RoleManager() para creacion de Roles
                     * ==============================================
                     */

                    if (!await _roleManager.RoleExistsAsync("Administrador"))
                        await _roleManager.CreateAsync(new IdentityRole("Administrador"));

                    if (!await _roleManager.RoleExistsAsync("Registrado"))
                        await _roleManager.CreateAsync(new IdentityRole("Registrado"));
                    
                    if (!await _roleManager.RoleExistsAsync("Cliente")) 
                        await _roleManager.CreateAsync(new IdentityRole("Cliente"));


                    //vamos a asignarle un rol que ya estan registrado en la base de datos. 
                    string Rol = Input.IDRol.Equals(1) ? "Administrador" : Input.IDRol.Equals(1) ? "Registrado" : "Cliente";
                    IdentityResult resultado = await _userManager.AddToRoleAsync(user, Rol);

                    if (resultado.Succeeded)
                        _logger.LogInformation("Se ha podido asignar un rol a la cuenta");
                    else
                        _logger.LogError("No se pudo asignar el respectivo rol ");

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                ModelState.AddModelError("IDRol", "Debes introducir un rol para el usuario");
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
