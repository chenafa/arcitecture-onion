using MediatR;
using Microsoft.EntityFrameworkCore;
using OA.Domain.Entities;
using OA.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OA.Service.Features.CustomerFeatures.Queries;

public class GetAllCustomerQuery : IRequest<IEnumerable<Customer>>
{

    public class GetAllCustomerQueryHandler(IApplicationDbContext context)
        : IRequestHandler<GetAllCustomerQuery, IEnumerable<Customer>>
    {
        public async Task<IEnumerable<Customer>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var customerList = await context.Customers.ToListAsync();
            if (customerList == null)
            {
                return null;
            }
            return customerList.AsReadOnly();
        }
    }
}