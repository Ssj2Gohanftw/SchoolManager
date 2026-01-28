using FluentValidation;
using SchoolManager.Dtos.Student;

namespace SchoolManager.Validators.Students
{
    public class UpdateStudentDtoValidator:AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            When(x => x.FirstName != null, () =>
            {
                RuleFor(x => x.FirstName!)
                .NotEmpty()
                .MaximumLength(100);
            });

            When(x => x.LastName != null, () =>
            {
                RuleFor(x => x.LastName!)
                .NotEmpty()
                .MaximumLength(100);
            });

            When(x => x.Email != null, () =>
            {
                RuleFor(x => x.Email!)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(254);
            });

            When(x => x.DateOfBirth != null, () =>
            {
                RuleFor(x => x.DateOfBirth!.Value)
                    .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
                    .WithMessage("Date of Birth can't be in the future");
            });
        }
    }
}
