using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Data.Entities.Settings
{
    public class SiteSetting:CreatableEntity
    {
        public string SettingValue { get; set; }
        public string SettingKey { get; set; }
    }
}
