using FluentValidation;
using SchoolManager.Dtos.Fee;

namespace SchoolManager.Validators.Fee
{
    public class UpdateFeeDtoValidator:AbstractValidator<UpdateFeeDto>
    {
        public UpdateFeeDtoValidator()
        {
            var currentYear = DateTime.UtcNow.Year;
            RuleFor(f => f.Year)
                .InclusiveBetween(2000, currentYear)
                .NotEmpty();
            RuleFor(f => f.FeeType)
                .IsInEnum()
                .NotEmpty();
            RuleFor(f => f.Amount)
                .NotEmpty()
                .GreaterThanOrEqualTo(200);

        }
    }
    }

