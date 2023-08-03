using Binte.Data;
using Binte.Data.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Settings
{
    public class SettingSevice : ISettingService
    {
        IRepository<SiteSetting> _settingRepository;
        public SettingSevice(IRepository<SiteSetting> _settingRepository)
        {
            _settingRepository = _settingRepository;
        }

        public SiteSetting Get(string key)
        {
            return _settingRepository.GetAll(p=>p.SettingKey==key).FirstOrDefault();
        }

        public List<SiteSetting> GetAll()
        {
            return _settingRepository.GetAll();
        }

        public string GetValue(string key)
        {
            var result=_settingRepository.GetAll(p=>p.SettingKey==key).FirstOrDefault();
            return result == null ? "" : result.SettingValue;
        }

        public bool Insert(string key, string value)
        {
           var r=GetValue(key);
            if (r !=null && r !="")
            {
                return false;
            }
            var s=new SiteSetting();
            s.SettingValue=value;
            s.SettingKey=key;
            var result = _settingRepository.InsertData(s);
            return result;
        }

        public bool Update(string key, string value)
        {
            var result=Get(key);
            if (result != null)
            {
                result.SettingValue=value;
                return _settingRepository.UpdateData(result);
            }
            else
            {
                return Insert(key, value);
            }
        }

    }
}
