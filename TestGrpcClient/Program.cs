using Grpc.Net.Client;
using RandomNumber;

namespace TestGrpcClient1;

class Program
{
    static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress($"https://localhost:{args[0]}");
        var client = new RandomNumberGenerator.RandomNumberGeneratorClient(channel);
        var reply = await client.GetRandomNumberAsync(new RandomNumberRequest());
        Console.WriteLine("Random Number: " + reply.Number);
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}