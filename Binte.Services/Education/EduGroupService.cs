using Binte.Data;
using Binte.Data.Entities.Account;
using Binte.Data.Entities.Education;
using Binte.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Binte.Services.Education
{
    public class EduGroupService : IEduGroupService
    {
        IRepository<EduGroup> _eduGrouprepository;
        IRepository<EduGroupStudentRelation> _EduGroupStudentRelationRepository;
        BinteUserManager _userManager;
        public EduGroupService(IRepository<EduGroup> eduGrouprepository,
            IRepository<EduGroupStudentRelation> EduGroupStudentRelationRepository,
            BinteUserManager userManager)
        {
            _eduGrouprepository= eduGrouprepository;
            _EduGroupStudentRelationRepository = EduGroupStudentRelationRepository;
            _userManager=userManager;
        }
        public bool Add(EduGroup entity)
        {
            return _eduGrouprepository.InsertData(entity);
        }

        public EduGroup Get(int id)
        {
            return _eduGrouprepository.Get(id);
        }

        public List<EduGroup> GetAll()
        {
            return _eduGrouprepository.GetAll();
        }

        public BaseResponse Update(EduGroup entity)
        {
            var studentCount = _EduGroupStudentRelationRepository.GetAll(p => p.GroupId == entity.EducationId).Count;

            if (studentCount>entity.MaxCapacity)
            {
                return BaseResponse.GetError("Kayitli ogrenci sayisindan daha fazla ogrenci kayit yapilamaz");
            }
            var obj = _eduGrouprepository.UpdateData(entity);
            return obj ? BaseResponse.GetSuccess() : BaseResponse.GetError("Bir hata olustu");
        }
        public GetResponse<EduGroup> GetData(int id)
        {
            var obj= _eduGrouprepository.Get(id);
            return new GetResponse<EduGroup>()
            {
                Status = true,
                Data = obj
            };
        }
        public int GetRegisteredStudent(int id)
        {
            var count = 0;
            var data = _EduGroupStudentRelationRepository.GetAll(p => p.GroupId == id);
            count = data.Count();
            return count;
        }

        public BaseResponse AddStudentToGroup(int groupid, int studentid)
        {
            var exists = _EduGroupStudentRelationRepository.GetAll(p => p.GroupId == groupid && p.StudentId == studentid);
            if (exists!=null && exists.Count>0)
            {
                return BaseResponse.GetError("Bu ogrenci bu guruba daha once eklenmis");
            };

            var studentCount=_EduGroupStudentRelationRepository.GetAll(p => p.GroupId==groupid).Count;

            var capacity=GetData(groupid).Data.MaxCapacity;
            if (studentCount>=capacity)
            {
                return BaseResponse.GetError("Maksimum kapasiye ulasildi");
            }
            
            var result= _EduGroupStudentRelationRepository.InsertData(new EduGroupStudentRelation()
            {
                StudentId = studentid,
                GroupId = groupid
            });

            return result ? BaseResponse.GetSuccess() : BaseResponse.GetError("Bir Hata olustu");
        }

        public GetAllResponse<BinteUser> GetStudentsForEducationGroup(int groupId) {
           var studentIdList= _EduGroupStudentRelationRepository
                .GetAll(p => p.Id == groupId)
                .Select(p => p.StudentId)
                .ToList();
            var student=_userManager.Users.Where(p => studentIdList.Contains(p.Id)).ToList();
            return new GetAllResponse<BinteUser>
            {
                Status = true,
                DataList = student
            };
        }
    }
}
