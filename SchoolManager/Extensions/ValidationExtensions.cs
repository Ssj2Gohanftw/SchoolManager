using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace SchoolManager.Extensions
{
    public static class ValidationExtensions
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
        //public async static Task ValidateModel<TDto>(this IValidator<TDto> validator, TDto entity)
        //{
        //    var validationResult = await validator.ValidateAsync(entity);

        //    if (!validationResult.IsValid)
        //    {
        //        validationResult.AddToModelState(ModelState);
        //        return ValidationProblem(ModelState);
        //    }
        //}

    }
}
