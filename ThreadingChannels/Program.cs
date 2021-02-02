using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ThreadingChannels
{
    class Program
    {
       
            static async Task Main(string[] args)
            {
                await SingleProducerSingleConsumer();
                Console.ReadKey();
            }
            public static async Task SingleProducerSingleConsumer()
            {
                var channel = Channel.CreateUnbounded<int>();
                var reader = channel.Reader;
                for (int i = 0; i < 10; i++)
                {
                    await channel.Writer.WriteAsync(i + 1);
                }
                while (await reader.WaitToReadAsync())
                {
                    if (reader.TryRead(out var number))
                    {
                        Console.WriteLine(number);
                    }
                }
            }


    }
    public class Mail
    {
        public Mail(int id, string content)
        {
            Id = id;
            Content = content;
        }

        public int Id { get; }
        public string Content { get; }
    }
}
