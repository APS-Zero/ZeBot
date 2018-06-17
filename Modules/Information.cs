using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeBot.Modules
{
    public class ServerInfo : ModuleBase<SocketCommandContext>
    {
        [Command("serverinfo")]
        [Alias("sinfo", "servinfo", "serverstats")]
        [Remarks("Info about a server")]
        public async Task GuildInfo()
        {
            Console.WriteLine($"{DateTime.Now}: {Context.User.Username + "#" + Context.User.Discriminator} in {Context.Guild.Name} in {Context.Channel.Id} did {Context.Message.Content}");

            EmbedBuilder embedBuilder;
            embedBuilder = new EmbedBuilder();
            embedBuilder.WithColor(new Color(0, 71, 171));

            var gld = Context.Guild as SocketGuild;
            var client = Context.Client as DiscordSocketClient;

            int BotCount = 0;
            foreach (SocketUser x in Context.Guild.Users)
            {
                if (x.IsBot)
                {
                    BotCount += 1;
                }
            }

            var humans = Context.Guild.MemberCount - BotCount;
            double humanRatio = ((double)humans / (double)Context.Guild.MemberCount);
            var humanPercentage = Math.Round((humanRatio * 100), 2);
            double botRatio = ((double)BotCount / (double)Context.Guild.MemberCount);
            var botPercentage = Math.Round((botRatio * 100), 2);

            /*if (!string.IsNullOrWhiteSpace(gld.IconUrl))
                embedBuilder.ThumbnailUrl = gld.IconUrl;
            var O = gld.Owner.Username;

            var V = gld.VoiceRegionId;
            var C = gld.CreatedAt;
            var N = gld.DefaultMessageNotifications;
            var R = gld.Roles;
            var VL = gld.VerificationLevel;
            var XD = gld.Roles.Count;
            var X = gld.MemberCount;
            var Z = client.ConnectionState;*/

            if (!string.IsNullOrWhiteSpace(gld.IconUrl))
                embedBuilder.ThumbnailUrl = gld.IconUrl;

            var Name = gld.Name;
            var Owner = gld.Owner.Username + "#" + gld.Owner.Discriminator;
            var Region = gld.VoiceRegionId;
            var CreatedDate = gld.CreatedAt;
            var Notifications = gld.DefaultMessageNotifications;
            var VerificationLevel = gld.VerificationLevel;
            var RoleCount = gld.Roles.Count;
            var MemberCount = gld.MemberCount;
            var ConnectionState = client.ConnectionState;
            var ServerId = gld.Id;
            var ChannelCount = gld.Channels.Count;

            embedBuilder.Title = $"{gld.Name} Server Information";
            embedBuilder.Description = $"Server Owner: **{Owner}\n**" +
                $"Server Region: **{Region}\n**" +
                $"Created At: **{CreatedDate}\n**" +
                $"MsgNtfc: **{Notifications}\n**" +
                $"Verification: **{VerificationLevel}\n**" +
                $"Role Count: **{RoleCount}\n**" +
                $"Users: **{humans} ({humanPercentage}%)\n**" +
                $"Bots: **{BotCount} ({botPercentage}%)\n**" +
                $"Channels:** {ChannelCount}\n**" +
                $"Connection state:** {ConnectionState}\n**" +
                $"Server ID:** {ServerId}\n\n**";

            await ReplyAsync("", false, embedBuilder);
        }
    }

    public class UserInfo : ModuleBase<SocketCommandContext>
    {
        [Command("userinfo")]
        [Alias("uinfo", "userstats")]
        [Name("userinfo `<user>`")]
        public async Task PaxInfo(SocketGuildUser user = null)
        {
            Console.WriteLine($"{DateTime.Now}: {Context.User.Username + "#" + Context.User.Discriminator} in {Context.Guild.Name} in {Context.Channel.Id} did a!userinfo {user}");

            if (user == null) user = (Context.Message.Author as SocketGuildUser);

            var application = await Context.Client.GetApplicationInfoAsync();
            var thumbnailurl = user.GetAvatarUrl();
            var date = $"{user.CreatedAt.Month}/{user.CreatedAt.Day}/{user.CreatedAt.Year}";
            var auth = new EmbedAuthorBuilder()
            {
                Name = user.Username,
                IconUrl = thumbnailurl,
            };

            var embed = new EmbedBuilder()

            {
                Color = new Color(29, 140, 209),
                Author = auth
            };

            var us = user as SocketGuildUser;

            if (!string.IsNullOrWhiteSpace(thumbnailurl))
                embed.ThumbnailUrl = thumbnailurl;

            var D = us.Username;

            var A = us.Discriminator;
            var T = us.Id;
            var S = date;
            var C = us.Status;
            var CC = us.JoinedAt;
            var O = us.Game;
            //var R = us.Roles;

            embed.Title = $"**{us.Username}** Information";
            embed.Description = $"Username: **{D}**" +
                $"\nDiscriminator: **{A}**" +
                $"\nUser ID: **{T}**" +
                $"\nCreated at: **{S}**" +
                $"\nCurrent Status: **{C}**" +
                $"\nJoined server at: **{CC}**" +
                $"\nPlaying: **{O}**";
            //$"\nRoles: **{R}**";

            await ReplyAsync("", false, embed.Build());
        }
    }
}
