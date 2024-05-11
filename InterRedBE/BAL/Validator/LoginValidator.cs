using FluentValidation;
using InterRedBE.DAL.DTO;

namespace InterRedBE.BAL.Validator
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.NombreUsuario)
                .NotEmpty().WithMessage("El usuario es requerido")
                .MaximumLength(20).WithMessage("El usuario no puede tener más de 20 caracteres")
                .Matches(@"[A-Z]").WithMessage("El usuario debe contener al menos una letra mayúscula")
                .Matches(@"[a-z]").WithMessage("El usuario debe contener al menos una letra minúscula");

            RuleFor(x => x.Contrasena)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
                .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula")
                .Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número")
                .Matches(@"[!@#$%^&*()_+}{:|?><]").WithMessage("La contraseña debe contener al menos un carácter especial");

        }
    }
}
