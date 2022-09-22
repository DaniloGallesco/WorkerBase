using ClubeAss.API.Customer.Properties;
using ClubeAss.API.Customer.ViewModel.Customer;
using FluentValidation;

namespace ClubeAss.API.Customer.Validators.Customer
{
    public class CustomerUpdateRequestValidator : AbstractValidator<CustomerUpdateRequest>
    {
        public CustomerUpdateRequestValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithErrorCode("ERRO-01").WithMessage(string.Format(Resources.ERRO_NotEmpty, "Id"))
                .MinimumLength(5).WithErrorCode("ERRO-02").WithMessage(string.Format(Resources.ERRO_MinimumLength, "Nome", 5));
        }
    }
}
