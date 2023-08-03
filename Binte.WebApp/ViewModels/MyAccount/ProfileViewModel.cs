using Binte.Data.Entities.Account;
using Binte.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Binte.WebApp.ViewModels.MyAccount
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }


        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
        public string ProfileImage { get; set; }
        public Gender GenderCode { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
        public Binte.Data.Entities.Account.Education EducationCode { get; set; }
        public IEnumerable<SelectListItem> EducationList { get; set; }
        public string PhoneNumber { get; set; }
    }
}
