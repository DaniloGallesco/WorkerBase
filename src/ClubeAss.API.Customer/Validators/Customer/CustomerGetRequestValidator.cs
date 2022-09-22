using ClubeAss.API.Customer.Properties;
using ClubeAss.API.Customer.ViewModel.Customer;
using FluentValidation;

namespace ClubeAss.API.Customer.Validators.Customer
{
    public class CustomerGetRequestValidator : AbstractValidator<CustomerGetRequest>
    {
        public CustomerGetRequestValidator()
        {
            RuleFor(c => c.Id)
               .NotEmpty().WithErrorCode("ERRO-01").WithMessage(string.Format(Resources.ERRO_NotEmpty, "Id"));
        }
    }
}
