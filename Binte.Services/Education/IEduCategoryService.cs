using Binte.Data.Entities.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Education
{
    public interface IEduCategoryService
    {
        List<EducationCategory> GetAll();
        EducationCategory Get(int id);
        string GetName(int id);

        bool Add(string title,string desc,string image);
        bool Update(int id,string title, string desc, string image);


    }
}
