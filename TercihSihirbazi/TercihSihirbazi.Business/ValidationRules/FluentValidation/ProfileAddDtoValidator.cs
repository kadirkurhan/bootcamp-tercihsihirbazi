using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.Entities.Dtos.ProfileDtos;

namespace TercihSihirbazi.Business.ValidationRules.FluentValidation
{
    public class ProfileAddDtoValidator : AbstractValidator<ProfileAddDto>
    {
        public ProfileAddDtoValidator()
        {
            RuleFor(I => I.Name).NotEmpty().WithMessage("ad alanı boş geçilemez");
        }
    }
}
