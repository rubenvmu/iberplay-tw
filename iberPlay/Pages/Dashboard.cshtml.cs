using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class DashboardModel : PageModel
    {
        [BindProperty] // Para enlazar datos del formulario
        public int Seguridad { get; set; } = 0; // Inicializa la seguridad en 0

        public void OnGet()
        {
            // Si ya existe un valor en la sesión, lo cargamos
            Seguridad = HttpContext.Session.GetInt32("seguridad") ?? 0;
        }

        public IActionResult OnPost(string accion)
        {
            // Obtener el valor actual de seguridad desde la sesión
            Seguridad = HttpContext.Session.GetInt32("seguridad") ?? 0;

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
    }
}