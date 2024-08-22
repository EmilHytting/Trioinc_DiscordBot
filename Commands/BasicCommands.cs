using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace TrioincBot.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        //Discord Utils Implement - Methode
        public static class DiscordUtils
        {
            public static bool TryParseMention(string mention, out ulong userId)
            {
                userId = 0;
                if (mention.StartsWith("<@!") && mention.EndsWith(">"))
                {
                    var idString = mention.Substring(3, mention.Length - 4);
                    return ulong.TryParse(idString, out userId);
                }
                return false;
            }
        }

        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = "Ping Command",
                Description = "Pong",
                Color = DiscordColor.Azure
            };

            await ctx.RespondAsync(embed: embed);
        }

        [Command("random")]
        public async Task RandomNumber(CommandContext ctx, int min, int max, DiscordMember member = null)
        {
            try
            {
                Console.WriteLine("RandomNumber command called");

                if (min >= max)
                {
                    await ctx.RespondAsync("Minimum value must be less than maximum value.");
                    return;
                }

                var random = new Random();
                int number = random.Next(min, max + 1);

                var embed = new DiscordEmbedBuilder
                {
                    Title = "Random Number Generator",
                    Description = member != null ? $"{member.Mention}, your number is: {number}" : $"Your number is: {number}",
                    Color = DiscordColor.Azure
                };

                embed.AddField("Purge: ", "This message will be purged in 10 seconds.", true);
                embed.WithFooter("Ⓒ 2024 Trioinc", ctx.Client.CurrentUser.AvatarUrl)
                     .WithTimestamp(DateTime.Now);

                var embedMessage = await ctx.RespondAsync(embed: embed);
                Console.WriteLine($"Embed Message ID: {embedMessage.Id}");

                await Task.Delay(10000);

                await embedMessage.DeleteAsync();
                Console.WriteLine("Embed message deleted");

                await ctx.Message.DeleteAsync();
                Console.WriteLine("Command message deleted");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error in RandomNumber command: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        [Command("info")]
        public async Task Info(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = "📜 Trioinc Information",
                Description = "Here is some information about the bot.",
                Color = DiscordColor.Azure,
            };

            // Tilføj emojis til felter
            embed.AddField("🤖 Bot Name:", ctx.Client.CurrentUser.Username, true);
            embed.AddField("🌐 Servers:", ctx.Guild.Name, true);
            embed.AddField("👥 Members:", ctx.Guild.MemberCount.ToString(), true);
            embed.AddField("🔢 Version:", "0.0.2", true);
            embed.AddField("🔍 Command Prefix:", "!", true);
            embed.AddField("👨‍💻 Developer:", "Your Name or Team", true);
            embed.WithThumbnail(ctx.Client.CurrentUser.AvatarUrl);

            embed.AddField("🌐 Website:", "[Visit Website](https://emilmh.netlify.app/)", true);
            embed.AddField("💬 Support Server:", "[Join Here](https://discord.gg/f9xdvSJdNQ)", true);


            embed.AddField("🛠️ Status:", "All systems operational", true);

            await ctx.RespondAsync(embed: embed);
        }

    }
}
