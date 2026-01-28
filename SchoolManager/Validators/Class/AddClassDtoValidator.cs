using FluentValidation;
using SchoolManager.Dtos.Class;

namespace SchoolManager.Validators.Class
{
    public class AddClassDtoValidator:AbstractValidator<AddClassDto>
    {
        public AddClassDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Classname is required!")
                .MaximumLength(100);
            RuleFor(c => c.Branch)
                .NotEmpty().WithMessage("Branch must be specified!")
                .IsInEnum().WithMessage("Enter a valid branch!");
        }
    }
}
