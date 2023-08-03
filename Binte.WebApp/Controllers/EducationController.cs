using Binte.Data.Entities.Education;
using Binte.Services.Education;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Binte.WebApp.ViewModels.Education;
using Binte.WebApp.Extensions;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Binte.Services;
using Binte.WebApp.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Binte.WebApp.Controllers
{
    public class EducationController : PanelController
    {
        IEducationService _educationServices;
        IEduCategoryService _educationCategory;
        IMapper _mapper;
        IEduGroupService _eduGroupService;
        BinteUserManager _binteUserManager;
        public EducationController(IEduCategoryService educationCategory, IMapper mapper, IEducationService educationServices, IEduGroupService eduGroupService, BinteUserManager binteUserManager)
        {
            _educationCategory = educationCategory;
            _mapper = mapper;
            _educationServices = educationServices;
            _eduGroupService = eduGroupService;
            _binteUserManager = binteUserManager;

        }

        #region EducationCategories
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EducationCategories()
        {
            var model = _educationCategory.
                GetAll().
                Select(p => _mapper.Map<EducationCategoryListViewModel>(p)).
                ToList();
            return View(model);
        }

        public IActionResult AddEduCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEduCategory(EducationCategoryAddViewModel model)
        {
            string imagepath = "/EducationCategory/noImage.jpg";
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                imagepath = UploadHelper.Upload(file, "EducationCategory");
            }
            var result = _educationCategory.Add(model.Title, model.Description, imagepath);
            if (result)
                return RedirectToAction("EducationCategories");
            else
                return View(model);
        }

        public IActionResult UpdateEduCategory(int id)
        {
            var model = _mapper.Map<EducationCategoryEditViewModel>(_educationCategory.Get(id));
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateEduCategory(EducationCategoryEditViewModel model)
        {
            string imagepath = model.Image;
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                imagepath = UploadHelper.Upload(file, "EducationCategory");
            }
            var result = _educationCategory.Update(model.Id, model.Title, model.Description, imagepath);
            if (result)
                return RedirectToAction("EducationCategories");
            else
                return View(model);
        }
        #endregion

        #region Education

        public async Task<IActionResult> Educations()
        {
            //metodlarla select atma ( cevap verme suresi 500ms orn)

            //var model = _educationServices.
            //    GetAll().
            //    Join(_educationCategory.GetAll(), p => p.CategoryId, c => c.Id, (p, c) =>
            //    {
            //        return new { p, c };
            //    }).Select(p => new EducationListViewModel
            //    {
            //        Id=p.p.Id,
            //        CategoryName=p.c.Title,
            //        Name=p.p.Name                    
            //    }).
            //    ToList();

            //Linq ile select atma ( cevap verme suresi 100ms orn)

            //var model = (
            //    from e in _educationServices.GetAll()
            //    join ec in _educationCategory.GetAll()
            //    on e.CategoryId equals ec.Id
            //    select new EducationListViewModel
            //    {
            //        CategoryName=ec.Title,
            //        Name=e.Name,
            //        Id=e.Id
            //    }            
            //    ).ToList();

            //Linq ile select atma ( cevap verme suresi 10ms orn-enhizli cvp veren bu)

            var model = _educationServices.
                GetAll().
                Select(p => new EducationListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = _educationCategory.GetName(p.CategoryId)
                }).ToList();
            return View(model);
        }
        public async Task<IActionResult> AddEducation()
        {
            var model = new EducationAddViewModel();
            model.Educations = _educationServices.GetAll().Select(p => new SelectListItem()
            {
                Selected = false,
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();

            model.Categories = _educationCategory.GetAll().Select(p => new SelectListItem()
            {
                Selected = false,
                Text = p.Title,
                Value = p.Id.ToString()
            }).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEducation(EducationAddViewModel model)
        {
            try
            {
                var edu = _mapper.Map<Data.Entities.Education.Education>(model);
                var imageName = "/Education/noimage.jpg";
                if (Request.Form.Files.Count > 0)
                    UploadHelper.Upload(Request.Form.Files[0], "Education");

                edu.Image = imageName;
                _educationServices.Add(edu);
                return RedirectToAction("Educations");
            }
            catch
            {
                model.Educations = _educationServices.GetAll().Select(p => new SelectListItem()
                {
                    Selected = false,
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();

                model.Categories = _educationCategory.GetAll().Select(p => new SelectListItem()
                {
                    Selected = false,
                    Text = p.Title,
                    Value = p.Id.ToString()
                }).ToList();

                return View(model);
            }

        }

        public async Task<IActionResult> UpdateEducation(int id)
        {
            var data = _educationServices.Get(id);
            var model = _mapper.Map<EducationEditViewModel>(data);
            model.Educations = _educationServices.GetAll().Select(p => new SelectListItem()
            {
                Selected = false,
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();

            model.Categories = _educationCategory.GetAll().Select(p => new SelectListItem()
            {
                Selected = false,
                Text = p.Title,
                Value = p.Id.ToString()
            }).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEducation(EducationEditViewModel model)
        {
            try
            {
                var edu = _mapper.Map<Data.Entities.Education.Education>(model);
                var imageName = model.Image;
                if (Request.Form.Files.Count > 0)
                    UploadHelper.Upload(Request.Form.Files[0], "Education");

                edu.Image = imageName;
                var result = _educationServices.Update(edu);
                if (result == false)
                {
                    throw new Exception("Guncellenemedi");
                }
                return RedirectToAction("Educations");
            }
            catch
            {
                model.Educations = _educationServices.GetAll().Select(p => new SelectListItem()
                {
                    Selected = false,
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();

                model.Categories = _educationCategory.GetAll().Select(p => new SelectListItem()
                {
                    Selected = false,
                    Text = p.Title,
                    Value = p.Id.ToString()
                }).ToList();

                return View(model);
            }

        }
        #endregion

        #region EducationGroups
        public async Task<IActionResult> EducationGroups()
        {
            var model = _eduGroupService.GetAll().Select(p => new EducationGroupListViewModel()
            {
                Name = p.Name,
                IsOnline = p.IsOnline,
                StartDate = p.StartDate,
                TotalHour = p.TotalHour,
                Id = p.Id,
                MaxCapacity = p.MaxCapacity,
                EducationName = _educationServices.GetName(p.Id),
                TeacherName = _binteUserManager.GetUserName(p.Id),
                RegisteredStudentCount = _eduGroupService.GetRegisteredStudent(p.Id)
            }).ToList();

            ViewBag.OgrenciListesi = _binteUserManager.GetUsersWithRole("Student");
            
            return View(model);
        }
        public async Task<IActionResult> AddEducationGroups()
        {
            var model = new EducationGroupAddViewModel();
            model.Educations = _educationServices
                .GetSelectListItems()
                .Select(p => _mapper.Map<SelectListItem>(p))
                .ToList();
            model.Teachers = _binteUserManager.Users.Select(u => new SelectListItem()
            {
                Text = u.Name + " " + u.Surname,
                Value = u.Id.ToString()
            }).ToList();

            var modell=_mapper.Map<EduGroup>(model);
            EduGroupValidation validate = new EduGroupValidation();
            ValidationResult result = validate.Validate(modell);
            if (result.IsValid)
            {
                // Geçerli öğrenci modeli, validation’dan geçti.
            }
            else
            {
                // Geçersiz öğrenci modeli, validation’a takıldı.
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEducationGroups(EducationGroupAddViewModel model)
        {
            var entity = _mapper.Map<EduGroup>(model);
            _eduGroupService.Add(entity);
            return RedirectToAction("EducationGroups");
        }
        public async Task<IActionResult> UpdateEducationGroups(int id)
        {
            var result = _eduGroupService.Get(id);
            var model = _mapper.Map<EducationGroupEditViewModel>(result);
            model.Educations = _educationServices
                .GetSelectListItems()
                .Select(p => _mapper.Map<SelectListItem>(p))
                .ToList();
            model.Teachers = _binteUserManager.Users.Select(u => new SelectListItem()
            {
                Text = u.Name + " " + u.Surname,
                Value = u.Id.ToString()
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEducationGroups(EducationGroupEditViewModel model)
        {
            var entity = _mapper.Map<EduGroup>(model);
            var res = _eduGroupService.Update(entity);
            if (res.Status)
            {
                return RedirectToAction("EducationGroups");
            }
            ModelState.AddModelError("General", res.Message);
            model.Educations = _educationServices
               .GetSelectListItems()
               .Select(p => _mapper.Map<SelectListItem>(p))
               .ToList();
            model.Teachers = _binteUserManager.Users.Select(u => new SelectListItem()
            {
                Text = u.Name + " " + u.Surname,
                Value = u.Id.ToString()
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudentForEducationGroup(string eid, string sid)
        {
            var result = _eduGroupService.AddStudentToGroup(Convert.ToInt32(eid), Convert.ToInt32(sid));
            //var a=new
            //{
            //    status= result ? "success" : "error",
            //    message=""
            //};
            return Json(result);
        }

        public async Task<IActionResult> GetStudentsForEducationGroup(int groupId)
        {
            var result=_eduGroupService.GetStudentsForEducationGroup(groupId);
            return Json(result);            
        }
        #endregion
    }
}
