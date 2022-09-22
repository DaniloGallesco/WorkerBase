using ClubeAss.API.Customer.Properties;
using ClubeAss.API.Customer.ViewModel.Customer;
using FluentValidation;

namespace ClubeAss.API.Customer.Validators.Customer
{
    public class CustomerDeleteRequestValidator : AbstractValidator<CustomerDeleteRequest>
    {
        public CustomerDeleteRequestValidator()
        {
            RuleFor(c => c.Id)
               .NotEmpty().WithErrorCode("ERRO-01").WithMessage(string.Format(Resources.ERRO_NotEmpty, "Id"));
        }
    }
}
