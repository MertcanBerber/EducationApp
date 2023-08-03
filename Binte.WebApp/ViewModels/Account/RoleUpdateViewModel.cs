using System.ComponentModel.DataAnnotations;

namespace Binte.WebApp.ViewModels.Account
{
    public class RoleUpdateViewModel
    {
        [Display(Name="Rol Adı",Prompt ="Rol Adını giriniz")]
        public string Name { get; set; }
        [Display(Name = "Rol Adı", Prompt = "Rol Adını giriniz")]
        public string NormalizedName { get; set; }
        public int Id { get; set; }

    }
}
