using FluentValidation;
using SchoolManager.Dtos.Subject;

namespace SchoolManager.Validators.Subject
{
    public class UpdateSubjectDtoValidator:AbstractValidator<UpdateSubjectDto>
    {
        public UpdateSubjectDtoValidator()
        {
            When(sub=>sub.Name!=null, () =>
            {
                RuleFor(sub => sub.Name)
                .NotEmpty();
            });
        }
    }
}
