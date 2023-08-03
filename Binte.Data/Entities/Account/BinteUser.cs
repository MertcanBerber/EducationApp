using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Data.Entities.Account
{
    public class BinteUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender GenderCode { get; set; }
        public Education EducationCode { get; set; }
        public string IdentityNumber { get; set; }
        public string ProfileImage { get; set; }


    }
}
