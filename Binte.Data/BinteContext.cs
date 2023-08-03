using Binte.Data.Entities.Account;
using Binte.Data.Entities.Education;
using Binte.Data.Entities.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Data
{
    public class BinteContext:IdentityDbContext<BinteUser,BinteRole,int>

    {
        public BinteContext(DbContextOptions o) : base(o) { } 
        public DbSet<SiteSetting> Sitesettings { get; set; }
        public DbSet<Entities.Education.Education> Educations { get; set; }
        public DbSet<EducationCategory> EducationCategories { get; set; }
        public DbSet<EduGroup> EduGroups { get; set; }
        public DbSet<EduGroupStudentRelation> EduGroupStudentRelations { get; set; }
        public DbSet<EduSubjectRelation> EduSubjectRelations { get; set; }
        public DbSet<Subject> Subjects { get; set; }


    }
}
