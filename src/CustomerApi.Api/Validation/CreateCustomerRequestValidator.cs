using CustomerApi.Api.Models;
using FluentValidation;

namespace CustomerApi.Api.Validation
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(req => req.Email)
                .Must(e => e.Contains("@")).WithMessage("Email must contains an @");

        }

    }
}
