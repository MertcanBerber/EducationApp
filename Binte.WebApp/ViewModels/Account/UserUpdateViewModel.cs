using Binte.Data.Entities.Account;
using Binte.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Binte.WebApp.ViewModels.Account
{
    public class UserUpdateViewModel
    {
        public UserUpdateViewModel()
        {
            GenderList = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(v => new SelectListItem()
                {
                    Text = v.ToString(),
                    Value = Convert.ToString(v)
                })
                .ToList();
            EducationList = EnumExtensions.ToSelectList<Binte.Data.Entities.Account.Education>();
        }
        [Display(Name = "Ad", Prompt = "Adınızı giriniz")]
        public string Name { get; set; }
        public int Id { get; set; }

        [Display(Name = "Soyad", Prompt = "Soyadınızı giriniz")]

        public string Surname { get; set; }
        [Display(Name = "Dogum Tarihi", Prompt = "Doğum tarihinizi giriniz")]

        public DateTime Birthdate { get; set; }
        [Display(Name = "Email", Prompt = "Email Adresinizi giriniz")]

        public string EmailAddress { get; set; }
        [Display(Name = "Cinsiyet")]

        public Gender GenderCode { get; set; }
        [Display(Name = "Eğitim")]
        public Binte.Data.Entities.Account.Education EducationCode { get; set; }
        public string Role { get; set; }

        [Display(Name = "TC kimlik No", Prompt = "TC kimlik numarasını giriniz")]

        public string IdentityNumber { get; set; }
        public string ProfileImage { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
        public IEnumerable<SelectListItem> EducationList { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
