using FluentValidation;

namespace MyAwsApp.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100).WithMessage("Description cannot be blank and cannot exceed 100 characters");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("Name cannot be blank and cannot exceed 50 characters");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
            RuleFor(x => x.ProductId).NotEmpty().MaximumLength(40).WithMessage("Id cannot be blank and cannot exceed 40 characters");
        }
    }
}
