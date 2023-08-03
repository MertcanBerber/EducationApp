using Microsoft.AspNetCore.Mvc.Rendering;

namespace Binte.WebApp.ViewModels.Education
{
    public class EducationEditViewModel
    {
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Educations { get; set; }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int DependentSubjectId { get; set; }
    }
}
