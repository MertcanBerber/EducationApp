using AutoMapper;
using Binte.Data;
using Binte.Data.Entities.Account;
using Binte.Services;
using Binte.WebApp.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Binte.WebApp.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<BinteUser> _signInManager;
        BinteUserManager _userManager;
        BinteContext _context;
        IUserStore<BinteUser> _userStore;
        IRoleStore<BinteRole> _roleStore;
        IMapper _mapper;
        public AccountController(SignInManager<BinteUser> signInManager,
            BinteUserManager userManager,
            BinteContext context,
            IRoleStore<BinteRole> roleStore,
            IMapper mapper,
            IUserStore<BinteUser> userStore)
        {
            _signInManager=signInManager;
            _userManager=userManager;
            _context=context;
            _userStore=userStore;
            _roleStore=roleStore;
            _mapper = mapper;
        }
        #region Login
        public async Task<IActionResult> Login()
        {
            //BinteRole role = new BinteRole();
            //await _roleStore.SetRoleNameAsync(role, "admin", new CancellationToken());
            //await _roleStore.CreateAsync(role, new CancellationToken());            

            //BinteRole role2 = new BinteRole();
            //await _roleStore.SetRoleNameAsync(role2, "student", new CancellationToken());
            //await _roleStore.CreateAsync(role2, new CancellationToken());

            //var findeduser=await _userManager.FindByEmailAsync("sd@gmail.com");

            //var findeduser=_userManager.ActiveUser;
            //await _userManager.AddToRoleAsync(findeduser, "Admin");
            await _signInManager.SignOutAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            BinteUser findedUser=await _userManager.FindByEmailAsync(model.EmailAddress);
            if (findedUser == null)
                return View(model);
            
            var result=await _signInManager.PasswordSignInAsync(findedUser, model.Password,model.RememberMe,false);

            if (result.Succeeded)
            {
                return Redirect("/Home/Index");
            }
            return View(model);
        }
        #endregion

        #region Register
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var findeduser=await _userManager.FindByEmailAsync(model.Email);
            if (findeduser!=null)
            {
                return View(model);
            }
            var user= new BinteUser();
            user.Name= model.Name;      
            user.Surname= model.Surname;
            user.IdentityNumber = "";
            user.ProfileImage = "";

            await _userManager.SetEmailAsync(user,model.Email);

            var result =await _userStore.CreateAsync(user,new CancellationToken());

            if (result.Succeeded)
            {
                await _userManager.SetUserNameAsync(user, model.Email);
                await _userManager.AddPasswordAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "Student");
                await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                return Redirect("/Home/Index");
            }
            return View(model);
        }
        #endregion

        #region Roles
        public async Task<IActionResult> Roles()
        {
            var datalist=_context.Roles.ToList().Select(p=> _mapper.Map<RoleListViewModel>(p)).ToList();
            return View(datalist);
        }
        public async Task<IActionResult> AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleAddViewModel vm)
        {
            var result=_context.Roles.Any(p => p.Name == vm.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu rol mevcuttur");
                return View(vm);
            }
            var br=_mapper.Map<BinteRole>(vm);
            br.NormalizedName = vm.Name.ToUpperInvariant();
            _context.Roles.Add(br);
            _context.SaveChanges();
            return RedirectToAction("Roles");
        }        

        [HttpPost]
        public async Task<IActionResult> DeleteRole( string id)
        {
            var findedRole = _context.Roles.FirstOrDefault(p => p.Id == Convert.ToInt32(id));            
            if (findedRole == null)
            {
            return Json(new { status=false});
            }
            _context.Roles.Remove(findedRole);
            _context.SaveChanges();
            return Json(new { status=true});
        }

        public async Task<IActionResult> UpdateRole(int id)
        {
            var findedRole = _context.Roles.FirstOrDefault(p => p.Id == id);
            if (findedRole==null)
            {
                return RedirectToAction("Roles");
            }
            var model=_mapper.Map<RoleUpdateViewModel>(findedRole);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateViewModel vm)
        {
            var findedRole = _context.Roles.FirstOrDefault(p => p.Id == vm.Id);
            findedRole.NormalizedName = vm.Name.ToUpperInvariant();
            findedRole.Name=vm.Name;
            _context.Roles.Update(findedRole);
            _context.SaveChanges();
            return RedirectToAction("Roles");
        }
        #endregion

        #region Users

        public async Task<IActionResult> Users()
        {
            var datalist = _context.Users.Select(p => _mapper.Map<UserListViewModel>(p)).ToList();
            return View(datalist);            
        }
        public async Task<IActionResult> AddUser()
        {
            var model = new UserAddViewModel();
            model.RoleList = _context.Roles.Select(p=> new SelectListItem(){
                Value=p.Name,
                Text=p.Name
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(UserAddViewModel model)
        {
            var user=_mapper.Map<BinteUser>(model);
            user.ProfileImage = "";
            await _userManager.SetEmailAsync(user, model.EmailAddress);

            var result = await _userStore.CreateAsync(user, new CancellationToken());

            if (result.Succeeded)
            {
                await _userManager.SetUserNameAsync(user, model.EmailAddress);
                await _userManager.AddPasswordAsync(user, "123123Aa--");
                await _userManager.AddToRoleAsync(user, model.Role);
                return Redirect("Users");
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateUser(int Id)
        {            
            var users=_context.Users.FirstOrDefault(p => p.Id == Id);
            
            var userrole=await _userManager.GetRolesAsync(users);            

            if (users == null)
            {
                return RedirectToAction("Users");
            }            
            var model = _mapper.Map<UserUpdateViewModel>(users);
            model.Role = string.Concat(userrole);
            model.RoleList = _context.Roles.Select(p => new SelectListItem()
            {
                Value = p.Name,
                Text = p.Name
            }).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel model)
        {
            var modelrole=model.Role;          
            
            var findedUser = _context.Users.FirstOrDefault(p => p.Id == model.Id);            
            findedUser.NormalizedUserName = model.Name.ToUpperInvariant();
            findedUser.Name = model.Name;

            IList<string> userRole =await _userManager.GetRolesAsync(findedUser);
            var result = string.Concat(userRole);
            if (result!=modelrole)
            {
                await _userManager.RemoveFromRoleAsync(findedUser, result); 
                await _userManager.AddToRoleAsync(findedUser, modelrole);
                await _userManager.UpdateAsync(findedUser);
            }


            //var oldUser = Manager.FindById(user.Id);
            //var oldRoleId = oldUser.Roles.SingleOrDefault().RoleId;
            //var oldRoleName = DB.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

            //if (oldRoleName != role)
            //{
            //    Manager.RemoveFromRole(user.Id, oldRoleName);
            //    Manager.AddToRole(user.Id, role);
            //}
            //DB.Entry(user).State = EntityState.Modified
            _context.Users.Update(findedUser);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var findedUser = _context.Users.FirstOrDefault(p => p.Id == Convert.ToInt32(id));
            if (findedUser == null)
            {
                return Json(new { status = false });
            }
            _context.Users.Remove(findedUser);
            _context.SaveChanges();
            return Json(new { status = true });
        }
        #endregion
    }
}
