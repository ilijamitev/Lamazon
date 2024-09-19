using FluentValidation;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.ViewModelValidators
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter the name of the product");
            RuleFor(x => x.Name).MaximumLength(255);

            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageUrl).NotEmpty();
            RuleFor(x => x.ProductCategoryId).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();

            RuleFor(x => x.DiscountPercentage).InclusiveBetween(0, 100);

            RuleFor(x => x.Price).LessThan(100000000).WithMessage("Please enter value less than 10000000");
        }
    }
}
