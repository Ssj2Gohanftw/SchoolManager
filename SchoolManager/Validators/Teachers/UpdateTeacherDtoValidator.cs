using FluentValidation;
using SchoolManager.Dtos.Teacher;

namespace SchoolManager.Validators.Teachers
{
    public class UpdateTeacherDtoValidator:AbstractValidator<UpdateTeacherDto>
    {
        public UpdateTeacherDtoValidator()
        {
            When(t => t.FirstName!=null, () =>
            {
                RuleFor(t => t.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            });
            When(t => t.LastName != null, () =>
            {
                RuleFor(t => t.LastName)
                .NotEmpty()
                .MaximumLength(100);

            });
            When(t => t.Email!= null, () =>
            {
                RuleFor(t => t.Email)
                .EmailAddress()
                .NotEmpty()
                .MaximumLength(100);

            });
        }
    }
}
