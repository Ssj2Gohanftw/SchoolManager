using FluentValidation;
using SchoolManager.Dtos.Student;

namespace SchoolManager.Validators.Students
{
    public class UpdateStudentDtoValidator:AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            When(s => s.FirstName != null, () =>
            {
                RuleFor(s => s.FirstName!)
                .NotEmpty()
                .MaximumLength(100);
            });

            When(s => s.LastName != null, () =>
            {
                RuleFor(s => s.LastName!)
                .NotEmpty()
                .MaximumLength(100);
            });

            When(s => s.Email != null, () =>
            {
                RuleFor(s => s.Email!)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);
            });

            When(s => s.DateOfBirth != null, () =>
            {
                RuleFor(s => s.DateOfBirth!)
                    .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
                    .WithMessage("Date of Birth can't be in the future");
            });
        }
    }
}
