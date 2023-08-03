using Microsoft.AspNetCore.Mvc.Rendering;

namespace Binte.WebApp.ViewModels.Education
{
    public class EducationGroupListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EducationName { get; set; }
        public bool IsOnline { get; set; }
        public int MaxCapacity { get; set; }
        public int TotalHour { get; set; }
        public int RegisteredStudentCount { get; set; }
        public DateTime StartDate { get; set; }
        public string TeacherName { get; set; }
    }
}
