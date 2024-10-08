using MediatR;
using Microsoft.EntityFrameworkCore;
using OA.Persistence;

namespace OA.Service.Features.CustomerFeatures.Commands;

public class UpdateCustomerCommand : IRequest<int>
{
    public int Id { get; set; }
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

}

public class UpdateCustomerCommandHandler(IApplicationDbContext context)
    : IRequestHandler<UpdateCustomerCommand, int>
{
    public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);

        if (customer == null)
            return default;

        customer.CustomerName = request.CustomerName;
        customer.ContactName = request.ContactName;
        customer.ContactTitle = request.ContactTitle;
        customer.Address = request.Address;
        customer.City = request.City;
        customer.Region = request.Region;
        customer.PostalCode = request.PostalCode;
        customer.Country = request.Country;
        customer.Fax = request.Fax;
        customer.Phone = request.Phone;

        context.Customers.Update(customer);
        await context.SaveChangesAsync();

        return customer.Id;
    }
}
