using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OA.Domain.Entities;
using OA.Persistence;

namespace OA.Test.Unit.Persistence
{
    public class ApplicationDbContextTest
    {
        [Test]
        public void CanInsertCustomerIntoDatabasee()
        {

            using var context = new ApplicationDbContext();
            var customer = new Customer();
            context.Customers.Add(customer);
            Assert.That(context.Entry(customer).State, Is.EqualTo(EntityState.Added));
        }
    }
}
