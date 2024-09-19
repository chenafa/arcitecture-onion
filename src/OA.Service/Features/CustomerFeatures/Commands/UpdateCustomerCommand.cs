using MediatR;
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
    public class UpdateCustomerCommandHandler(IApplicationDbContext context)
        : IRequestHandler<UpdateCustomerCommand, int>
    {
        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var cust = context.Customers.Where(a => a.Id == request.Id).FirstOrDefault();

            if (cust == null)
            {
                return default;
            }
            else
            {
                cust.CustomerName = request.CustomerName;
                cust.ContactName = request.ContactName;
                cust.ContactTitle = request.ContactTitle;
                cust.Address = request.Address;
                cust.City = request.City;
                cust.Region = request.Region;
                cust.PostalCode = request.PostalCode;
                cust.Country = request.Country;
                cust.Fax= request.Fax;
                cust.Phone= request.Phone;

                context.Customers.Update(cust);
                await context.SaveChangesAsync();
                return cust.Id;
            }
        }
    }
}