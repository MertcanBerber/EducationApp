using AutoMapper;
using Binte.Data.Entities;
using Binte.Data.Entities.Account;
using Binte.Data.Entities.Education;
using Binte.WebApp.ViewModels.Account;
using Binte.WebApp.ViewModels.Education;
using Binte.WebApp.ViewModels.MyAccount;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Binte.WebApp.Mapper
{
    public class BinteProfile:Profile
    {
        public BinteProfile()
        {
            CreateMap<BinteRole, RoleListViewModel>();
            CreateMap<BinteRole, RoleAddViewModel>().
                ReverseMap();
            CreateMap<BinteRole, RoleUpdateViewModel>().
               ReverseMap();
            CreateMap<BinteUser, UserAddViewModel>().
               ReverseMap();
            CreateMap<BinteUser, UserUpdateViewModel>().
               ReverseMap();

            CreateMap<BinteUser, UserListViewModel>().
               ForMember(p => p.EmailAddress, p => p.MapFrom(p => p.Email)).
               ForMember(p => p.NameSurname, p => p.MapFrom(p => p.Name + " " + p.Surname)).
            ReverseMap().
               ForMember(p => p.Email, p => p.MapFrom(p => p.EmailAddress));
           
            CreateMap<BinteUser, ProfileViewModel>().
               ReverseMap();
            CreateMap<EducationCategory, EducationCategoryListViewModel>().
               ReverseMap();
            CreateMap<EducationCategory, EducationCategoryAddViewModel>().
               ReverseMap();
            CreateMap<EducationCategory, EducationCategoryEditViewModel>().
               ReverseMap();

            CreateMap<Binte.Data.Entities.Education.Education, EducationListViewModel>().
               ReverseMap();
            CreateMap<Binte.Data.Entities.Education.Education, EducationAddViewModel>().
               ReverseMap();
            CreateMap<Binte.Data.Entities.Education.Education, EducationEditViewModel>().
               ReverseMap();

            CreateMap<EduGroup, EducationGroupListViewModel>().
               ReverseMap();
            CreateMap<EduGroup, EducationGroupAddViewModel>().
               ReverseMap();
            CreateMap<EduGroup, EducationGroupEditViewModel>().
               ReverseMap();
            //CreateMap<List<EduGroup>, List<EducationGroupListViewModel>>().
               //ReverseMap();
               //listeyi map yapamiyoruz.Hata verir.

            CreateMap<SelectListItem, MySelectListItem>().
               ReverseMap();

        }
    }
}
