using FluentValidation;
using SchoolManager.Dtos.University;

namespace SchoolManager.Validators.Universities
{
    public class UniversityQueryDtoValidator:AbstractValidator<UniversityQueryDto>
    {
        public UniversityQueryDtoValidator()
        {
            RuleFor(u => u)
                .Must(u => !
                string.IsNullOrWhiteSpace(u.Country) ||
                string.IsNullOrWhiteSpace(u.Name) ||
                string.IsNullOrWhiteSpace(u.StateProvince)
                )
                .WithMessage("Provide at least one of: country, name, state-province.");

            When(u => u.Limit != null, () =>
            {
                RuleFor(u => u.Limit!.Value)
                .InclusiveBetween(1, 200);
            });

            When(u => u.Country != null, () =>
            {
                RuleFor(u => u.Country!)
                .MaximumLength(100);
            });

            When(u => u.StateProvince != null, () =>
            {
                RuleFor(u => u.StateProvince!)
                .MaximumLength(100);
            });

            When(u => u.Name!= null, () =>
            {
                RuleFor(u => u.Name!)
                .MaximumLength(200);
            });

        }
    }
}
