using System.ComponentModel.DataAnnotations;

namespace Binte.WebApp.ViewModels.Account
{
    public class RoleAddViewModel
    {
        [Display(Name="Rol Adı",Prompt ="Rol Adını giriniz")]
        public string Name { get; set; }
    }
}
