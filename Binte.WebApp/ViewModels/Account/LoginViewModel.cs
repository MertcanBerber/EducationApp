using System.ComponentModel.DataAnnotations;

namespace Binte.WebApp.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name ="Email Adresi",Prompt ="Mail Adresini Giriniz")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Display(Name = "Şifre", Prompt = "Şifrenizi Giriniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
