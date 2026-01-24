namespace CleanFirmeza.Application.Validators;

using CleanFirmeza.Application.DTOs;
using FluentValidation;
public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor<string>(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .MaximumLength(150);

        RuleFor<string>(x => x.Description)
            .NotEmpty()
            .WithMessage("Price must be greater than 0")
            .MaximumLength(255);
            
        RuleFor<decimal>(x => x.UnitCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("cannot have negative stock");
    }
}
