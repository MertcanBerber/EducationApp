using Binte.Data.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Settings
{
    public interface ISettingService
    {
        string GetValue(string key);
        SiteSetting Get(string key);
        List<SiteSetting> GetAll();
        bool Insert(string key, string value);
        bool Update(string key, string value);

    }
}
