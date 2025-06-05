using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.Dto;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BlogCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class GenerarOrdenCompraController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IDataSecurityRepository _dataSecurityRepository;
        private readonly IUnitofWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;

        public GenerarOrdenCompraController(
            IHttpContextAccessor httpContextAccesor,
            IDataSecurityRepository dataSecurityRepository,
            IUnitofWork unitOfWork,
            IHttpClientFactory httpClientFactory
        )
        {
            _dataSecurityRepository = dataSecurityRepository;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index(string codigoCompra)
        {
            TempData["codigo"] = codigoCompra;

            return View();
        }

        [HttpGet]
        public IActionResult ConfirmarOrden(string token, string PayerID)
        {
            //vamos a tener que realizar la respectiva confirmacion final de la orden.

            string Username = "AXCaV2bYjRIa8--aCrZLFg4O3axzuPasObeFYg3ehbKIL4hU0FPZBlfJrQEjuijGoRtQJ1vxAh2QhJQv";
            string Password = "EInJZQzBCYygNGmm0NFbVsHlnqAMs1HJMWcJhhiZ3kkFVkDNw8_lTMh2o7cdx0x0tOyR2Tok_5tl18MB";

            var cliente = _httpClientFactory.CreateClient("Paypal");

            var credencialesBasicAuthentication = Encoding.ASCII.GetBytes($"{Username}:{Password}");

            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credencialesBasicAuthentication));

            using StringContent json = new(
            "{}",
            Encoding.UTF8,
            "application/json");

            var resultado = cliente.PostAsync($"v2/checkout/orders/{token}/capture", json);

            if (resultado.Result.IsSuccessStatusCode)
            {
                var informacionRespuesta = resultado.Result.Content.ReadAsStringAsync().Result;

                ConfirmationPayDTO confirmationPayDTO = JsonConvert.DeserializeObject<ConfirmationPayDTO>(informacionRespuesta)!;

                string codigoTransacccion = confirmationPayDTO.purchase_units.First().payments.captures.First().id;

                return RedirectToAction("Index", "GenerarOrdenCompra", new { codigoCompra = codigoTransacccion });
            }

            TempData["error"] = "No se pudo confirmar la orden, intentalo en otro momento.";

            return RedirectToAction("Index", "CarritoCompras");
        }

        [HttpPost]
        public IActionResult GenerarOrden()
        {
            //tenemos que validar que el cliente este logueado para poder realizar el respectivo cobro.
            //no traeremos nada del front-end... con ayuda del token que lo tenemos encriptado vamos 
            //a poder usarlo como punto de partida para realizar el cobro. 
            if (!this.User.Identity!.IsAuthenticated)
                return RedirectToAction("Identity/Account/Login");

            //sacarnos nuestro cookie
            string? cookieEncriptado = _httpContextAccesor.HttpContext!.Request.Cookies[DefinitionKeyCookies.keyShoppingCart];

            if ( cookieEncriptado is not null )
            {
                string idSessionDesencriptado = _dataSecurityRepository.desencriptarDatos(cookieEncriptado);
                Carrito? carritoRegistrado = _unitOfWork.Carrito.GetFirstOrDefault(n => n.carrito_sessionId.Equals(idSessionDesencriptado));

                if ( carritoRegistrado is not null )
                {
                    string hostAplicacion = _httpContextAccesor!.HttpContext!.Request.Host.ToString();
                    string hostProtocol = _httpContextAccesor!.HttpContext.Request.Scheme;
                    OrdersDTO orden = new OrdersDTO()
                    {
                        intent = "CAPTURE",
                        payment_source = new OrdersDTO.Payment_Source() { 
                            paypal = new OrdersDTO.Paypal() { 
                                experience_context = new OrdersDTO.Experience_Context()
                                {
                                    payment_method_preference = "IMMEDIATE_PAYMENT_REQUIRED",
                                    landing_page = "LOGIN",
                                    shipping_preference = "GET_FROM_FILE",
                                    user_action = "PAY_NOW",
                                    return_url = $"{hostProtocol}://{hostAplicacion}/Client/GenerarOrdenCompra/ConfirmarOrden",
                                    cancel_url = $"{hostProtocol}://{hostAplicacion}/"
                                }
                            }
                        },
                        purchase_units = new OrdersDTO.Purchase_Units[]
                        {
                            new OrdersDTO.Purchase_Units()
                            {
                                invoice_id = Guid.NewGuid().ToString(),
                                amount = new OrdersDTO.Amount()
                                {
                                    currency_code = "USD",
                                    value = "800.00" // Este valor debe ser calculado según el total del carrito
                                }
                            }
                        }
                    };

                    string Username = "AXCaV2bYjRIa8--aCrZLFg4O3axzuPasObeFYg3ehbKIL4hU0FPZBlfJrQEjuijGoRtQJ1vxAh2QhJQv";
                    string Password = "EInJZQzBCYygNGmm0NFbVsHlnqAMs1HJMWcJhhiZ3kkFVkDNw8_lTMh2o7cdx0x0tOyR2Tok_5tl18MB";

                    var cliente = _httpClientFactory.CreateClient("Paypal");

                    var credencialesBasicAuthentication = Encoding.ASCII.GetBytes($"{Username}:{Password}");
                    
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credencialesBasicAuthentication));


                    string bodyJson = JsonConvert.SerializeObject(orden);

                    using StringContent json = new(
                    bodyJson,
                    Encoding.UTF8,
                    "application/json");

                    var resultado = cliente.PostAsync("v2/checkout/orders", json); 
                    
                    if ( resultado.Result.IsSuccessStatusCode)
                    {
                        var informacionRespuesta = resultado.Result.Content.ReadAsStringAsync().Result;

                        ResponseOrdesDTO contenido = JsonConvert.DeserializeObject<ResponseOrdesDTO>(informacionRespuesta)!;

                        string rutaPagoPaypal = contenido.links.Where(n => n.rel.Equals("payer-action")).Select(n => n.href).ToList().First();

                        return StatusCode(200, new { data = rutaPagoPaypal } );
                    }

                }

            }

            return StatusCode(500, new { data = "", mensaje = "No se pudo continuar con el pago, intentalo en otro momento" });
        }
    }
}
