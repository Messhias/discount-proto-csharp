using Grpc.Net.Client;
using GrpcService.Protos;

// Create the channel
using var channel = GrpcChannel.ForAddress("http://localhost:5000");

// Create the client in the channel
var client = new DiscountService.DiscountServiceClient(channel);

// We make the request for discount codes.
var generateResponse = await client.GenerateDiscountAsync(new GenerateRequest { Count = 5, Length = 8 });
Console.WriteLine("Generated codes:");
foreach (var code in generateResponse.Codes)
{
	Console.WriteLine(code); // show me the codes :) 
}

// Now let's  use our codes, right?!
foreach (var code in generateResponse.Codes)
{
	var useResponse = await client.UseDiscountAsync(new UseRequest { Code = code });
	Console.WriteLine($"Code {code} used successfully: {useResponse.Success}");
}

