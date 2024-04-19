using FluentValidation;
using InterRedBE.DAL.DTO;

namespace InterRedBE.BAL.Validator
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El usuario es requerido")
                .EmailAddress().WithMessage("Debe ser una dirección de correo electrónico válida");

            RuleFor(x => x.Contrasena).NotEmpty().WithMessage("La contraseña es requerida");
        }
    }
}
