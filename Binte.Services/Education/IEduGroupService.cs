using Binte.Data.Entities.Account;
using Binte.Data.Entities.Education;
using Binte.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Education
{
    public interface IEduGroupService
    {
        bool Add(EduGroup entity);
        BaseResponse Update(EduGroup entity);
        List<EduGroup> GetAll();
        EduGroup Get(int id);
        int GetRegisteredStudent(int id);
        BaseResponse AddStudentToGroup(int groupid, int studentid);
        GetResponse<EduGroup> GetData(int id);
        GetAllResponse<BinteUser> GetStudentsForEducationGroup(int groupId);


    }
}
