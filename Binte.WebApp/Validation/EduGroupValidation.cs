using Binte.Data.Entities.Education;
using FluentValidation;

namespace Binte.WebApp.Validation
{
    public class EduGroupValidation:AbstractValidator<EduGroup>
    {
        public EduGroupValidation()
        {
            RuleFor(p => p.MaxCapacity).GreaterThan(20).When(p=>p.IsOnline==true)
                .WithMessage("Online egitime 20`den az olamaz");

            RuleFor(p => p.MaxCapacity).LessThan(20).When(p => p.IsOnline == false)
                .WithMessage("Sinif ici egitime 20`den fazla olamaz");
        }
    }
}
