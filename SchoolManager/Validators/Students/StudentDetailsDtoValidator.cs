using FluentValidation;
using SchoolManager.Dtos.Student;
using SchoolManager.Models;

namespace SchoolManager.Validators.Students
{
    public class StudentDetailsDtoValidator:AbstractValidator<StudentDetailsDto>
    {

        public StudentDetailsDtoValidator()
        {
            RuleFor(s => s.FirstName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(s => s.LastName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
            RuleFor(s => s.Gender)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(s => s.DateOfBirth)
                .LessThan(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Date of Birth can't be in the future");
            RuleFor(s => s.Branch).IsEnumName(typeof(Branch));
        }
    }
}
