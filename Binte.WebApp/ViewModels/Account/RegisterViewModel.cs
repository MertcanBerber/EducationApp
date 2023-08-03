using System.ComponentModel.DataAnnotations;

namespace Binte.WebApp.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name="Adınız",Prompt ="Adınızı Giriniz")]
        public string Name { get; set; }
        [Display(Name = "Soyadınız", Prompt = "Soyadınızı Giriniz")]
        public string Surname{ get; set; }
        [Display(Name = "Email", Prompt = "Email Adresinizi Giriniz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Şifreniz", Prompt = "Şifrenizi Giriniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Şifreniz", Prompt = "Şifrenizi Yeniden Giriniz")]
        public string ReTypePassword { get; set; }
        public bool isConfirmed { get; set; }


    }
}
