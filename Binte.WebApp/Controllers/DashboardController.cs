using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Binte.WebApp.Controllers
{
    public class DashboardController : PanelController
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
