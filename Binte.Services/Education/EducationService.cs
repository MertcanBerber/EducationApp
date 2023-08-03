using Binte.Data;
using Binte.Data.Entities;
using Binte.Data.Entities.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Education
{
    public class EducationService : IEducationService
    {
        IRepository<Data.Entities.Education.Education> _eduRepository;
        public EducationService(IRepository<Data.Entities.Education.Education> eduRepository)
        {
            _eduRepository = eduRepository;
        }

        public bool Add(Data.Entities.Education.Education entity)
        {
            return _eduRepository.InsertData(entity);
        }

        public Data.Entities.Education.Education Get(int id)
        {
            return _eduRepository.Get(id);
        }

        public List<Data.Entities.Education.Education> GetAll()
        {
            return _eduRepository.GetAll();            
        }

        public bool Update(Data.Entities.Education.Education entity)
        {
            return _eduRepository.UpdateData(entity);

        }
        public List<Data.Entities.MySelectListItem> GetSelectListItems()
        {
            return GetAll().Select(p => new MySelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();
        }

        public string GetName(int id)
        {
            var edu=Get(id);
            return edu==null? "" : edu.Name;
        }
        
    }
}
