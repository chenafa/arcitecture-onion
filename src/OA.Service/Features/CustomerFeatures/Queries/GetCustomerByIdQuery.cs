using MediatR;
using OA.Domain.Entities;
using OA.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OA.Service.Features.CustomerFeatures.Queries;

public class GetCustomerByIdQuery : IRequest<Customer>
{
    public int Id { get; set; }
    public class GetCustomerByIdQueryHandler(IApplicationDbContext context)
        : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = context.Customers.Where(a => a.Id == request.Id).FirstOrDefault();
            if (customer == null) return null;
            return customer;
        }
    }
}