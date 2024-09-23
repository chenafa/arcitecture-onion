namespace OA.Test.Integration;

internal static class HttpClientAuthExtensions
{
    const string AuthorizationKey = "Authorization";

    // JWT generated for 'superadmin@gmail.com' with max expiry date
    const string Jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzdXBlcmFkbWluIiwianRpIjoiNDIzNzhlNjktMmI0YS00ZTVhLWEyZjUtM2RjMTI4YTFhNGFiIiwiZW1haWwiOiJzdXBlcmFkbWluQGdtYWlsLmNvbSIsInVpZCI6Ijk3Y2Y0ZDkwLWYyY2EtNGEwZi04MjdhLWU2ZmVkZTE2ODQyYSIsImlwIjoiMTkyLjE2OC40NS4xMzUiLCJyb2xlcyI6WyJTdXBlckFkbWluIiwiQWRtaW4iLCJCYXNpYyIsIk1vZGVyYXRvciJdLCJleHAiOjI1MzQwMjI4ODIwMCwiaXNzIjoiSWRlbnRpdHkiLCJhdWQiOiJJZGVudGl0eVVzZXIifQ.sYDCw6R-HtNfC8xJYENmq39iYJtXiVrAh5dboTrGlX8";

    public static HttpClient AddBearerTokenAuthorization(this HttpClient client)
    {
        // Check if the Authorization header is already present
        if (client.DefaultRequestHeaders.Any(p => p.Key == AuthorizationKey))
            return client;

        client.DefaultRequestHeaders.Add(AuthorizationKey, $"Bearer {Jwt}");

        return client;

    }
}