using FluentValidation;
using SchoolManager.Dtos.Student;

namespace SchoolManager.Validators.Students
{
    public class AddStudentDtoValidator:AbstractValidator<AddStudentDto>
    {
        public AddStudentDtoValidator()
        {
            RuleFor(s => s.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(s => s.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(s => s.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
            RuleFor(s => s.Gender)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(32);
            RuleFor(s => s.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("Date of Birth can't be in the future");

        }

    }
}
