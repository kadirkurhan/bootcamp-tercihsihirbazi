using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.Entities.Dtos.AppUserDtos;

namespace TercihSihirbazi.Business.ValidationRules.FluentValidation
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(I => I.UserName).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(I => I.Password).NotEmpty().WithMessage("Şifre boş geçilemez");
        }
    }
}
