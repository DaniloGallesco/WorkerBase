using ClubeAss.Domain.Commands;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ClubeAss.API.Customer.Extensions.Fluentvalidation
{
    public static class FluentValidation
    {
        public static Task<BaseResponse> Errors(List<ValidationFailure> failures)
        {
            var response = new BaseResponse(HttpStatusCode.BadRequest, null, failures.Select(p => p.ErrorCode + " - " + p.ErrorMessage));

            return Task.FromResult(response);
        }
    }
}

