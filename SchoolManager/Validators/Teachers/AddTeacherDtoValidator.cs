using FluentValidation;
using SchoolManager.Dtos.Teacher;

namespace SchoolManager.Validators.Teachers
{
    public class AddTeacherDtoValidator:AbstractValidator<AddTeacherDto>
    {
        public AddTeacherDtoValidator()
        {
            RuleFor(t => t.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First Name is required!")
                .MaximumLength(100);
            RuleFor(t => t.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last Name is required!")
                .MaximumLength(100);
            RuleFor(t => t.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress().WithMessage("Enter a valid email address!")
                .MaximumLength(100);
        }
    }
}
