using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Data.Entities.Education
{
    public class EduGroup:BaseEntity
    {
        public int EducationId { get; set; }
        public bool IsOnline { get; set; }
        public int MaxCapacity { get; set; }
        public int TotalHour{ get; set; }
        public DateTime StartDate { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
    }
}
