using MediatR;
using Microsoft.EntityFrameworkCore;
using OA.Persistence;

namespace OA.Service.Features.CustomerFeatures.Commands;

public class DeleteCustomerByIdCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeleteCustomerByIdCommandHandler(IApplicationDbContext context)
    : IRequestHandler<DeleteCustomerByIdCommand, int>
{
    public async Task<int> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);
        if (customer == null) return default;
        context.Customers.Remove(customer);
        await context.SaveChangesAsync();
        return customer.Id;
    }
}