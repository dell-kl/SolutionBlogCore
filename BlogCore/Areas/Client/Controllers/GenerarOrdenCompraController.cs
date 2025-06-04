using BlogCore.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class GenerarOrdenCompraController : Controller
    {
        private const string HOST = "https://api-m.sandbox.paypal.com/";
        private readonly IHttpContextAccessor _httpContextAccesor;

        public GenerarOrdenCompraController(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public IActionResult GenerarOrden()
        {
            string hostAplicacion = _httpContextAccesor.HttpContext.Request.Host.ToString();

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
                            return_url = "https://localhost:44300/Client/GenerarOrdenCompra/ConfirmarOrden",
                            cancel_url = "https://localhost:44300/"
                        }
                    }
                }
            };

            return null;
        }
    }
}
