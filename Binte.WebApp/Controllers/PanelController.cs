using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Binte.WebApp.Controllers
{
    [Authorize(Roles = "admin")]

    public class PanelController : Controller
    {
        
    }
    [Authorize]
    public class AuthorizedController : Controller
    {

    }
}
