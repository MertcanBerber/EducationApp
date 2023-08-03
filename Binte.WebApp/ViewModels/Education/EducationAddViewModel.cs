using Microsoft.AspNetCore.Mvc.Rendering;

namespace Binte.WebApp.ViewModels.Education
{
    public class EducationAddViewModel
    {
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Educations { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DependentSubjectId { get; set; }
    }
}
