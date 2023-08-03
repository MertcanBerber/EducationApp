using Binte.Data.Entities.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Education
{
    public interface IEducationService
    {
        List<Data.Entities.Education.Education> GetAll();
        bool Add(Data.Entities.Education.Education entity);
        bool Update(Data.Entities.Education.Education entity);

        List<Data.Entities.MySelectListItem> GetSelectListItems();
        Data.Entities.Education.Education Get(int id);
        string GetName(int id);



    }
}
