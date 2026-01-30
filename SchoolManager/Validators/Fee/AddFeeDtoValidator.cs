using FluentValidation;
using SchoolManager.Dtos.Fee;

namespace SchoolManager.Validators.Fee
{
    public class AddFeeDtoValidator:AbstractValidator<AddFeeDto>
    {
        public AddFeeDtoValidator()
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
