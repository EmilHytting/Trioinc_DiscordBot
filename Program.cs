using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace TrioincBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }

    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            var token = Environment.GetEnvironmentVariable("DISCORD TOKEN");
        }
    }
}
