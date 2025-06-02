//program.cs
using Grpc.Core;
using HelloWord;

await HelloWorldTest();

static async Task HelloWorldTest()
{
    int port = 5555;
    string url = $"localhost:{port}";
    // ChannelCredentials.Insecure: it disables transport security (TLS/SSL) and sends data in plaintext. 
    Channel channel = new Channel(url, ChannelCredentials.Insecure)
    {
    };

    try
    {
        await channel.ConnectAsync();
        Console.WriteLine("Connected to server");
        var client = new HelloService.HelloServiceClient(channel);
        var response = await client.SayHelloAsync(new HelloRequest() { Name = "Hasibul Hasan" });
        Console.WriteLine(response);
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error has been thrown: {ex.Message}");
    }
    finally
    {
        if (channel is not null)
        {
            Console.WriteLine("Cient is shutting down");
            await channel.ShutdownAsync();
        }
    }
}