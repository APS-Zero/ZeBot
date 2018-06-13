using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace ZeBot.Modules
{
    public class EightBall : ModuleBase<SocketCommandContext>
    {
        string[] predictionsTexts = new string[]
        {
            "Without a doubt :ok_hand:",
            "Outlook good :upside_down:",
            "As I see it, yes :eyes:",
            "Most likely :smirk:",
            "Yes definitely :smile:",
            "It is certain :smiley:",
            "Outlook not so good :sob:",
            "Cannot predict now :weary:",
            "Very doubtful :unamused:",
            "My sources say no :head_bandage:",
            "Yes :thumbsup:",
            "Reply hazy try again :disappointed_relieved:",
            "Better not tell you now :speak_no_evil:",
            "Signs point to yes :sunglasses:",
            "Don't count on it :x:",
            "You may rely on it :blush:",
            "Ask again later :arrows_counterclockwise:",
            "My reply is no :angry:",
            "It is decidedly so :grin:",
            "Concentrate and ask again :slight_frown:",
        };

        Random rand = new Random();

        [Command("8ball")]
        [Alias("eightball")]
        [Summary("Gives a prediction")]
        public async Task EightBallReply([Remainder] string input = null)
        {
            Console.WriteLine($"{DateTime.Now}: {Context.User.Username + "#" + Context.User.Discriminator} in {Context.Guild.Name} did z!8ball {input}");

            if (string.IsNullOrWhiteSpace(input))
            {
                await ReplyAsync("Ask something for me to answer!! :grimacing:");
            }
            else
            {
                int randomIndex = rand.Next(predictionsTexts.Length);
                string text = predictionsTexts[randomIndex];
                await ReplyAsync(Context.User.Mention + ", " + text);
            }
        }
    }

    [Group("diceroll")]
    [Alias("dice", "d")]
    public class DiceRolling : ModuleBase<SocketCommandContext>
    {
        string[] predictRoll = new string[]
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
        };

        Random rand = new Random();

        [Command]
        public async Task DrReply()
        {
            Console.WriteLine($"{DateTime.Now}: {Context.User.Username + "#" + Context.User.Discriminator} in {Context.Guild.Name} did z!diceroll");

            var Dice = new Emoji("🎲");

            await Context.Message.AddReactionAsync(Dice);

            int randomIndex = rand.Next(predictRoll.Length);
            string drText = predictRoll[randomIndex];
            await ReplyAsync(Context.User.Mention + "\nYou rolled a **" + drText + "**! :game_die:");

            /*if (Context.Message.GetReactionUsersAsync(Dice))
            {
                await ReplyAsync(Context.User.Mention + "\nYou rolled a **" + drText + "**! :game_die:");
            }*/
        }
    }
}