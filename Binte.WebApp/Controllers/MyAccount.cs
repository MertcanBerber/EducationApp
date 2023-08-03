using AutoMapper;
using Binte.Data;
using Binte.Data.Entities;
using Binte.Services;
using Binte.WebApp.ViewModels.MyAccount;
using CuttingEdge.Conditions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Binte.WebApp.Controllers
{
    public class MyAccount : AuthorizedController
    {
        BinteContext _context;
        BinteUserManager _usermanager;
        IMapper _mapper;
        public MyAccount(BinteContext context,BinteUserManager usermanager,IMapper mapper)
        {
            _context= context;
            _usermanager= usermanager;
            _mapper= mapper;
        }
        public IActionResult Index()
        {
            var userInfo=_context.Users.FirstOrDefault(p => p.Id == _usermanager.ActiveUser.Id);
            var user=_mapper.Map<ProfileViewModel>(userInfo);
            return View(user);
        }
        [HttpPost]
        public IActionResult SetProfileImage()
        {            
            if (Request.Form.Files.Count < 1)
            {
                return RedirectToAction("Index");
            }

            var file=Request.Form.Files[0];
            var path ="Uploads/ProfileImage/"+_usermanager.ActiveUser.Id+"_user_"+" "+file.FileName;
            var fs=System.IO.File.OpenWrite("wwwroot/"+path);
            file.CopyTo(fs);
            fs.Close();

            var user=_context.Users.FirstOrDefault(p => p.Id == _usermanager.ActiveUser.Id);
            user.ProfileImage = "/" + path;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(ProfileViewModel model)
        {            
            if  (model.NewPassword != model.NewPasswordAgain)
            {
                ModelState.AddModelError("Password", "Yeni sifreyi tekrar giriniz");
                RedirectToAction("Index");
            }

            var passwordIsValid = await _usermanager.CheckPasswordAsync(_usermanager.ActiveUser, model.OldPassword);
            if (!passwordIsValid)
            {
                ModelState.AddModelError("Password", "Sifre hatali");
                RedirectToAction("Index");
            }

            var ps=await _usermanager.ChangePasswordAsync(_usermanager.ActiveUser, model.OldPassword,model.NewPassword);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangeProfileInfo(ProfileViewModel model)
        {
            try
            {
                Condition.Requires(model.Name).IsNotNull();
                Condition.Requires(model.Surname).IsNotNull();
                Condition.Requires(model.Email).IsNotNull();

                var user = await _usermanager.FindByIdAsync(_usermanager.ActiveUser.Id.ToString());
                user.Name= model.Name;
                user.Surname= model.Surname;
                user.PhoneNumber= model.PhoneNumber;
                user.Birthdate = model.Birthdate;
                user.GenderCode=model.GenderCode;
                user.EducationCode=model.EducationCode;

                await _usermanager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return RedirectToAction("Index",model);
        }
    }
}
