using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Configuration;
using TrioincBot.Commands;

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
        public DiscordClient? Client { get; private set; }
        public CommandsNextExtension? Commands { get; private set; }

        public async Task RunAsync()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var token = configuration["Discord:Token"];

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Trioinc's token is mission..!");
                return;
            }

            var config = new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                Intents = DiscordIntents.All,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
            };

            Client = new DiscordClient(config);

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new[] { "-" }, // Prefix for bot commands
                EnableMentionPrefix = true
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            // Register Commands
            Commands.RegisterCommands<BasicCommands>();

            // Ready On message
            Console.WriteLine("Trioinc is connectd and commands are loaded!");

            await Client.ConnectAsync();

            // Prevents the application from exiting immediately
            await Task.Delay(-1);

        }
    }
}
