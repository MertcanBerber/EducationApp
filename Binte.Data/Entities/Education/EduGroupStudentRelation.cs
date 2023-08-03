using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Data.Entities.Education
{
    public class EduGroupStudentRelation:BaseEntity
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }

    }
}
