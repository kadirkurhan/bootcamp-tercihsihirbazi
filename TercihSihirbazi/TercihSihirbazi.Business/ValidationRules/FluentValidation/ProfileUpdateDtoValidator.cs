using FluentValidation;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Dtos.ProfileDtos;

namespace TercihSihirbazi.Business.ValidationRules.FluentValidation
{
    public class ProfileUpdateDtoValidator : AbstractValidator<ProfileUpdateDto>
    {
        public ProfileUpdateDtoValidator()
        {
            RuleFor(I => I.Id).InclusiveBetween(0, int.MaxValue);
            RuleFor(I => I.Name).NotEmpty().WithMessage("Ad alanı boş bırakılamaz");
        }
    }
}
