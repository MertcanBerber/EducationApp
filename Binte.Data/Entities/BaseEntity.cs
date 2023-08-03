using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }
    public class CreatableEntity:BaseEntity
    {
        public DateTime CreateDate { get; set; }
    }
}
