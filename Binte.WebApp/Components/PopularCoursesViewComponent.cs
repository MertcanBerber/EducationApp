using Binte.Data.Entities.Account;
using Binte.Services.Education;
using Binte.WebApp.ViewModels.Account;
using Binte.WebApp.ViewModels.FontendView;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Security.Cryptography.Xml;

namespace Binte.WebApp.Components
{
    public class PopularCoursesViewComponent:ViewComponent
    {
        IEduGroupService _eduGroupService;
        IEducationService _eduService;
        UserManager<BinteUser> _userManager;
        RoleManager<BinteRole> _roleManager;

        public PopularCoursesViewComponent(IEduGroupService eduGroupService, 
            IEducationService eduService,
            UserManager<BinteUser> userManager,
            RoleManager<BinteRole> roleManager)
        {
            _eduGroupService = eduGroupService;
            _eduService = eduService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            PopularCoursesViewModel n = new PopularCoursesViewModel();

            var teachers = _userManager.GetUsersInRoleAsync("Teacher").Result.ToList();

            var edugroups = (from p in _eduGroupService.GetAll()
                             join e in _eduService.GetAll() on p.EducationId equals e.Id
                             join u in _userManager.Users.ToList() on p.TeacherId equals u.Id

                             select new PopularCoursesViewModel()
                             {
                                 Capacity=p.MaxCapacity,
                                 Description=e.Description,
                                 Image=e.Image,
                                 Hour=p.TotalHour,
                                 Name=p.Name,
                                 Price=new Random().Next(100,200),
                                 TeacherName=u.Name+" "+u.Surname,
                                 Id=p.Id
                             }
                             ).ToList();
            return View(edugroups);
        }
    }
}
