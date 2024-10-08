using NUnit.Framework;
using OA.Service.Features.CustomerFeatures.Commands;
using System.Net;

namespace OA.Test.Integration.ApiEndpoints;

public class ApiCustomerTest : TestClientProvider
{
    [TestCase("api/v1/Customer", HttpStatusCode.OK)]
    [TestCase("api/v1/Customer/100", HttpStatusCode.NoContent)]
    public async Task GetAllCustomerTestAsync(string url, HttpStatusCode result)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        var response = await Client
            .AddBearerTokenAuthorization()
            .SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(result));
    }

    [Test]
    public async Task CreateCustomerTestAsync()
    {
        // Create a customer model
        var model = new CreateCustomerCommand
        {
            CustomerName = "John Wick",
            ContactName = "John Wick",
            ContactTitle = "Manager",
            Address = "123 Main St",
            City = "New York",
            Region = "NY",
            PostalCode = "10001",
            Country = "USA",
            Phone = "123-456-7890",
            Fax = "987-654-3210"
        };

        // Create POST request
        var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/Customer")
        {
            Content = HttpClientExtensions.SerializeAndCreateContent(model)
        };

        // Send request and check response
        var response = await Client
            .AddBearerTokenAuthorization()
            .SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task UpdateCustomerTestAsync()
    {
        // First, create a new customer
        var createModel = new CreateCustomerCommand
        {
            CustomerName = "John Wick",
            ContactName = "John Wick",
            ContactTitle = "Manager",
            Address = "123 Main St",
            City = "New York",
            Region = "NY",
            PostalCode = "10001",
            Country = "USA",
            Phone = "123-456-7890",
            Fax = "987-654-3210"
        };

        var createRequest = new HttpRequestMessage(HttpMethod.Post, "api/v1/Customer")
        {
            Content = HttpClientExtensions.SerializeAndCreateContent(createModel)
        };

        var createResponse = await Client
            .AddBearerTokenAuthorization()
            .SendAsync(createRequest);

        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        // Now update the new customer
        var updateModel = new UpdateCustomerCommand
        {
            Id = int.Parse(await createResponse.Content.ReadAsStringAsync()),
            CustomerName = "Updated John Wick",
            ContactName = "Jane Wick",
            ContactTitle = "Director",
            Address = "456 Another St",
            City = "Los Angeles",
            Region = "CA",
            PostalCode = "90001",
            Country = "USA",
            Phone = "987-654-3210",
            Fax = "123-456-7890"
        };

        var updateRequest = new HttpRequestMessage(HttpMethod.Put, $"api/v1/Customer/{updateModel.Id}")
        {
            Content = HttpClientExtensions.SerializeAndCreateContent(updateModel)
        };

        // Send update request and check response status
        var updateResponse = await Client
            .AddBearerTokenAuthorization()
            .SendAsync(updateRequest);

        Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task DeleteCustomerTestAsync()
    {
        // First, create a new customer
        var createModel = new CreateCustomerCommand
        {
            CustomerName = "John Wick",
            ContactName = "John Wick",
            ContactTitle = "Manager",
            Address = "123 Main St",
            City = "New York",
            Region = "NY",
            PostalCode = "10001",
            Country = "USA",
            Phone = "123-456-7890",
            Fax = "987-654-3210"
        };

        var createRequest = new HttpRequestMessage(HttpMethod.Post, "api/v1/Customer")
        {
            Content = HttpClientExtensions.SerializeAndCreateContent(createModel)
        };

        var createResponse = await Client
            .AddBearerTokenAuthorization()
            .SendAsync(createRequest);

        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        // Get the created customer's ID
        int customerId = int.Parse(await createResponse.Content.ReadAsStringAsync());

        // Now delete the customer
        var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"api/v1/Customer/{customerId}");

        // Send delete request and check response status
        var deleteResponse = await Client
            .AddBearerTokenAuthorization()
            .SendAsync(deleteRequest);

        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        // Optionally, you can check if the customer has been actually deleted by trying to retrieve it
        var getRequest = new HttpRequestMessage(HttpMethod.Get, $"api/v1/Customer/{customerId}");
        var getResponse = await Client.AddBearerTokenAuthorization().SendAsync(getRequest);
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

}
