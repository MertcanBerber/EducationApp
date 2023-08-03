using Microsoft.AspNetCore.Mvc.Rendering;

namespace Binte.WebApp.ViewModels.Education
{
    public class EducationGroupEditViewModel
    {
        public int Id { get; set; }
        public List<SelectListItem> Educations { get; set; }
        public List<SelectListItem> Teachers { get; set; }
        public int EducationId { get; set; }
        public string Name{ get; set; }

        public bool IsOnline { get; set; }
        public int MaxCapacity { get; set; }
        public int TotalHour { get; set; }
        public DateTime StartDate { get; set; }
        public int TeacherId { get; set; }
    }
}
