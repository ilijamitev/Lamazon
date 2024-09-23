using FluentValidation;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.ViewModelValidators
{
    public class ProductCategoryViewModelValidator : AbstractValidator<ProductCategoryViewModel>
    {
        public ProductCategoryViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(255);
        }
    }
}
