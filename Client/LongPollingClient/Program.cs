
using System.Net;

using var client = new HttpClient();

client.BaseAddress = new Uri("https://localhost:7228");

while (true)
{
    Console.WriteLine("Checking order status...");

    var response = await client.GetAsync("/order/status");

    if (response.StatusCode == HttpStatusCode.OK)
    {
        var result = await response.Content.ReadAsStringAsync();

        Console.WriteLine(result);

        break;
    }

    if (response.StatusCode == HttpStatusCode.NoContent)
    {
        Console.WriteLine("No update yet. Trying again...");
    }
}
