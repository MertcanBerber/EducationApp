using Binte.Data;
using Binte.Data.Entities;
using Binte.Data.Entities.Account;
using Binte.Data.Entities.Settings;
using Binte.Services;
using Binte.Services.Education;
using Binte.Services.Settings;
using Binte.WebApp.ViewModels.FontendView;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Binte.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //ISettingService _repo;
        //public HomeController(ISettingService repo)
        //{
        //    _repo = repo;
        //}
        IEduGroupService _eduGroupService;
        IEducationService _eduService;
        UserManager<BinteUser> _userManager;
        RoleManager<BinteRole> _roleManager;

        public HomeController(IEduGroupService eduGroupService,
            IEducationService eduService,
            UserManager<BinteUser> userManager,
            RoleManager<BinteRole> roleManager)
        {
            _eduGroupService = eduGroupService;
            _eduService = eduService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            //var n = new SiteSetting();
            //n.SettingValue = "value";
            //n.SettingKey= "key";            

            //var userInfo=await _userManager.GetUserAsync(HttpContext.User);
            //ViewBag.UserName=userInfo;
            return View();
        }
        public async Task<IActionResult> CourseDetail(int id)
        {
            var teachers = _userManager.GetUsersInRoleAsync("Teacher").Result.ToList();

            var edugroups = (from p in _eduGroupService.GetAll()
                             join e in _eduService.GetAll() on p.EducationId equals e.Id
                             join u in _userManager.Users.ToList() on p.TeacherId equals u.Id
                             where p.Id == id
                             select new CourseDetailViewModel()
                             {
                                 Capacity = p.MaxCapacity,
                                 Description = e.Description,
                                 Image = e.Image,
                                 Hour = p.TotalHour,
                                 Name = p.Name,
                                 Price = new Random().Next(100, 200),
                                 TeacherName = u.Name + " " + u.Surname,
                                 Id = p.Id,
                                 CategoryName=e.Name
                             }
                             ).FirstOrDefault();
            return View(edugroups);
        }
    }
}
