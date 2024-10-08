using MediatR;
using Microsoft.EntityFrameworkCore;
using OA.Domain.Entities;
using OA.Persistence;

namespace OA.Service.Features.CustomerFeatures.Queries;

public class GetCustomerByIdQuery : IRequest<Customer>
{
    public int Id { get; set; }
}

public class GetCustomerByIdQueryHandler(IApplicationDbContext context)
        : IRequestHandler<GetCustomerByIdQuery, Customer>
{
    public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Customers.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);
    }
}
