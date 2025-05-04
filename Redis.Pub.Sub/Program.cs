// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;

Console.WriteLine("Hello, World!");


ConnectionMultiplexer connection=await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber= connection.GetSubscriber();

while (true)
{
    Console.WriteLine("Mesaj : ");
    string message = Console.ReadLine();
    await subscriber.PublishAsync("mychannel", message);

}