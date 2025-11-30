using FluentValidation;
using OrderManagementAPI.DTOs;

namespace OrderManagementAPI.Validators
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(x => x.ProductName)
           .NotEmpty()
           .WithMessage("Product name is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Unit price cannot be negative.");
        }
    }
}
