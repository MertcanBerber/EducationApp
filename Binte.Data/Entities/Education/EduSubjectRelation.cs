using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Data.Entities.Education
{
    public class EduSubjectRelation:BaseEntity
    {
        public int EducationId { get; set; }
        public int SubjectId { get; set; }

    }
}
