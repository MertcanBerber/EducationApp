using Binte.Data;
using Binte.Data.Entities.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Education
{
    public class EduCategoryService : IEduCategoryService
    {
        IRepository<EducationCategory> _educationCategoryRepository;
        public EduCategoryService(IRepository<EducationCategory> educationCategoryRepository)
        {
            _educationCategoryRepository= educationCategoryRepository;
        }

        public bool Add(string title, string desc, string image)
        {
            return _educationCategoryRepository.InsertData(new EducationCategory()
            {
                Description = desc,
                Title = title,
                Image = image
            });
        }

        public EducationCategory Get(int id)
        {
           return _educationCategoryRepository.Get(id);
        }

        public List<EducationCategory> GetAll()
        {
            return _educationCategoryRepository.GetAll();
        }

        public string GetName(int id)
        {
            var data= _educationCategoryRepository.Get(id);
            return data == null ? "" : data.Title;
        }

        public bool Update(int id, string title, string desc, string image)
        {
            var data=_educationCategoryRepository.Get(id);
            data.Description = desc;
            data.Title= title;
            data.Image = image;
            return _educationCategoryRepository.UpdateData(data);
        }
    }
}
