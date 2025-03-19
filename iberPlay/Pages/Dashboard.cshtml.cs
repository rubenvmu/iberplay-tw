using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class DashboardModel : PageModel
    {
        [BindProperty] // Para enlazar datos del formulario
        public int Seguridad { get; set; } = 100; // Inicializa la seguridad en 0

        public int Ibercoins { get; set; } = 100; // Inicializa la seguridad en 0

        public void OnGet()
        {
            // Cargar Seguridad de sesión si existe
            Seguridad = HttpContext.Session.GetInt32("seguridad") ?? 0;

            // Forzar Ibercoins a 100 en cada carga de la página
            Ibercoins = 100;
        }

        public IActionResult OnPost(string accion)
        {
            // Obtener el valor actual de seguridad desde la sesión
            Seguridad = HttpContext.Session.GetInt32("seguridad") ?? 0;
            Ibercoins = HttpContext.Session.GetInt32("seguridad") ?? 0;

            // Actualizar el valor de seguridad según la acción
            if (accion == "clicar-enlace")
            {
                Seguridad = Math.Max(0, Seguridad - 10); // Pierde 10% de seguridad
            }
            else if (accion == "desechar-sms")
            {
                Seguridad = Math.Min(100, Seguridad + 10); // Gana 10% de seguridad
            }

            // Guardar el nuevo valor en la sesión
            HttpContext.Session.SetInt32("seguridad", Seguridad);

            // Redirigir a la misma página para actualizar el valor
            return RedirectToPage();
        }

        // Método aparte para manejar Ibercoins
        public IActionResult OnPostIbercoins(string accion)
        {
            // Cargar Ibercoins, o iniciar en 100 si no hay nada
            Ibercoins = HttpContext.Session.GetInt32("ibercoins") ?? 100;

            // Ajustar Ibercoins según la acción
            if (accion == "ganar")
            {
                Ibercoins += 10; // Gana 10 Ibercoins
            }
            else if (accion == "perder")
            {
                Ibercoins = Math.Max(0, Ibercoins - 5); // Pierde 5 Ibercoins
            }

            // Guardar en sesión y recargar la página
            HttpContext.Session.SetInt32("ibercoins", Ibercoins);
            return RedirectToPage();
        }
    }
}