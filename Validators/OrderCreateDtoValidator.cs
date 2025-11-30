using FluentValidation;
using OrderManagementAPI.DTOs;

namespace OrderManagementAPI.Validators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name cant be empty");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("Price must be positive");
        }
    }
}
