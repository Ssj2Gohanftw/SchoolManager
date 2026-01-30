using FluentValidation;
using SchoolManager.Dtos.Class;

namespace SchoolManager.Validators.Class
{
    public class UpdateClassDtoValidator:AbstractValidator<UpdateClassDto>
    {
        public UpdateClassDtoValidator()
        {
            When(c => c.Name != null, () => {
                RuleFor(c => c.Name)
                .NotEmpty();
            });
            When(c => c.Branch != null, () => {
                RuleFor(c => c.Branch)
                .NotEmpty().WithMessage("Branch must be specified!")
                .IsInEnum().WithMessage("Enter a valid branch!");
            });
        }
    }
}
