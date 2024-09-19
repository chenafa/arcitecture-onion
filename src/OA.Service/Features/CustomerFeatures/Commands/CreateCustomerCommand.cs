using MediatR;
using OA.Domain.Entities;
using OA.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OA.Service.Features.CustomerFeatures.Commands;

public class CreateCustomerCommand : IRequest<int>
{
    public string CustomerName { get; set; }
    public string ContactName { get; set; }
    public string ContactTitle { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public class CreateCustomerCommandHandler(IApplicationDbContext context)
        : IRequestHandler<CreateCustomerCommand, int>
    {
        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer();
            customer.CustomerName = request.CustomerName;
            customer.ContactName = request.ContactName;

            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            return customer.Id;
        }
    }
}