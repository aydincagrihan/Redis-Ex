using StackExchange.Redis;

Console.WriteLine("Hello, World!");


ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber = connection.GetSubscriber();
subscriber.SubscribeAsync("mychannel", (channel, message) =>
{
    Console.WriteLine(message);
});//dinleyici

Console.Read();//console kapanmaması için sabitliyoruz.