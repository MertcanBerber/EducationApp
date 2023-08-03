using Binte.Data.Entities.Account;
using Binte.WebApp.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Binte.WebApp.Components
{
    public class PanelUserInfoViewComponent:ViewComponent
    {
        UserManager<BinteUser> _userManager;
        public PanelUserInfoViewComponent(UserManager<BinteUser> userManager)
        {
            _userManager= userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = new PanelUserInfoViewModel();
            var userinfo=await _userManager.GetUserAsync(HttpContext.User);
            user.Image = userinfo.ProfileImage;
            user.Name= userinfo.Name+" "+userinfo.Surname;
            user.Id=userinfo.Id;
            return View(user);
        }
    }
}
