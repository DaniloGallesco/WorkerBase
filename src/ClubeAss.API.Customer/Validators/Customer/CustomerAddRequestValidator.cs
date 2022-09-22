using ClubeAss.API.Customer.Properties;
using ClubeAss.API.Customer.ViewModel.Customer;
using FluentValidation;

namespace ClubeAss.API.Customer.Validators.Customer
{
    public class CustomerAddRequestValidator : AbstractValidator<CustomerAddRequest>
    {
        public CustomerAddRequestValidator()
        {
            RuleFor(c => c.Nome)
                 .NotEmpty().WithErrorCode("ERRO-01").WithMessage(string.Format(Resources.ERRO_NotEmpty, "Nome"))
                 .MinimumLength(5).WithErrorCode("ERRO-02").WithMessage(string.Format(Resources.ERRO_MinimumLength, "Nome", 5));
        }
    }
}
