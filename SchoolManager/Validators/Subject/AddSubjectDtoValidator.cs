using FluentValidation;
using SchoolManager.Dtos.Subject;

namespace SchoolManager.Validators.Subject
{
    public class AddSubjectDtoValidator:AbstractValidator<AddSubjectDto>
    {
        public AddSubjectDtoValidator()
        {
            RuleFor(sub => sub.Name)
                .NotEmpty().WithMessage("Subject name can't be empty!")
                .MaximumLength(100);
        }
    }
}
