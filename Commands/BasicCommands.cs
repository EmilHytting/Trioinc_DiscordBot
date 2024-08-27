using DSharpPlus;
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
            try
            {

                var embed = new DiscordEmbedBuilder
                {
                    Title = "Ping",
                    Description = $"Pong back to {ctx.User.Mention}! Latency is: {ctx.Client.Ping}ms.",
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
                Console.WriteLine($"Error in Ping command: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

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

        [Command("bot-info")]
        public async Task Info(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = "📜 Trioinc Information",
                Description = "Here is some information about the bot.",
                Color = DiscordColor.Cyan
            };


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


            // Custom Message Delete after command has been executed. 
            var embedMessage = await ctx.RespondAsync(embed: embed);
            Console.WriteLine($"Embed Message ID: {embedMessage.Id}");

            await Task.Delay(10000);

            await embedMessage.DeleteAsync();
            Console.WriteLine("Embed message deleted");

            await ctx.Message.DeleteAsync();
            Console.WriteLine("Command message deleted");
        }

        [Command("userinfo")]
        public async Task UserInfo(CommandContext ctx)
        {
            var roles = ctx.Member.Roles.Select(r => r.Name);
            var embed = new DiscordEmbedBuilder
            {
                Title = $"📜 User Information",
                Description = $"Here is some information about you, {ctx.User.Mention}",
                Color = DiscordColor.Cyan
            };

            embed.AddField("👤 Username:", ctx.User.Username, true);
            embed.AddField("🆔 User ID:", ctx.User.Id.ToString(), true);
            embed.AddField("📡 Status:", ctx.Member.Presence?.Status.ToString() ?? "Unknown", true);
            embed.AddField("🎭 Roles:", roles.Any() ? string.Join(", ", roles) : "No Roles", true);
            embed.AddField("📅 Joined Server on:", ctx.Member.JoinedAt.ToString("MMMM dd, yyyy") ?? "Unknown", true);
            embed.AddField("🎉 Joined Discord on:", ctx.User.CreationTimestamp.ToString("MMMM dd, yyyy"), true);

            embed.AddField("Purge: ", "This message will be purged in 10 seconds.", true);
            embed.WithFooter("Ⓒ 2024 Trioinc", ctx.Client.CurrentUser.AvatarUrl)
                 .WithTimestamp(DateTime.Now);

            // Custom Message Delete after command has been executed. 
            var embedMessage = await ctx.RespondAsync(embed: embed);
            Console.WriteLine($"Embed Message ID: {embedMessage.Id}");

            await Task.Delay(10000);

            await embedMessage.DeleteAsync();
            Console.WriteLine("Embed message deleted");

            await ctx.Message.DeleteAsync();
            Console.WriteLine("Command message deleted");

        }

        [Command("serverinfo")]
        public async Task ServerInfo(CommandContext ctx)
        {
            var guild = ctx.Guild;

            try
            {

                var owner = await guild.GetMemberAsync(guild.OwnerId);


                var memberCount = guild.MemberCount;
                var botCount = guild.Members.Count(m => m.Value.IsBot);


                var textChannels = guild.Channels.Values.Count(c => c.Type == ChannelType.Text);
                var voiceChannels = guild.Channels.Values.Count(c => c.Type == ChannelType.Voice);


                var creationDate = guild.CreationTimestamp.ToString("MMMM dd, yyyy");

                var embed = new DiscordEmbedBuilder
                {
                    Title = $"🌐 Server Information: {guild.Name}",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = guild.IconUrl },
                    Color = DiscordColor.Blurple
                };

                embed.AddField("👑 Owner:", owner.Username, true);
                embed.AddField("🆔 Server ID:", guild.Id.ToString(), true);
                embed.AddField("📅 Created On:", creationDate, true);
                embed.AddField("👥 Member Count:", $"{memberCount} (including {botCount} bots)", true);
                embed.AddField("💬 Text Channels:", textChannels.ToString(), true);
                embed.AddField("🔊 Voice Channels:", voiceChannels.ToString(), true);


                embed.AddField("🔒 Verification Level:", guild.VerificationLevel.ToString(), true);

                if (guild.PremiumSubscriptionCount > 0)
                {
                    embed.AddField("🚀 Boost Level:", guild.PremiumTier.ToString(), true);
                    embed.AddField("✨ Boost Count:", guild.PremiumSubscriptionCount.ToString(), true);
                }

                // Custom Message Delete after command has been executed. 
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

                await ctx.RespondAsync($"An error occurred: {ex.Message}");
            }
        }
    }
}
