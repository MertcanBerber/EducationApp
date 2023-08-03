using Binte.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Binte.WebApp.Extensions
{
    public class EnumExtensions
    {
        public static List<SelectListItem> ToSelectList<T>() where T : Enum 
        {
            return
                Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(v => new SelectListItem()
                {
                    Text = v.ToString(),
                    Value = Convert.ToString(v)
                })
                .ToList();
        }
    }
}
