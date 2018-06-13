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
    public class Dog : ModuleBase<SocketCommandContext>
    {
        [Command("dog")]
        [Alias("rdog", "randdog")]
        public async Task RandomDog()
        {
            Console.WriteLine($"{DateTime.Now}: {Context.User.Username + "#" + Context.User.Discriminator} in {Context.Guild.Name} did a!dog");

            WebClient client = new WebClient();
            string info = client.DownloadString("https://dog.ceo/api/breeds/image/random");
            var DogImage = ((info.Replace("\" target", "").Replace("\"", "").Replace("status", "").Replace("success", "").Replace("message", "").Replace(",", "").Replace("{", "").Replace("}", "")).Replace("::", "").Replace("\\", ""));

            EmbedBuilder embedBuilder;
            embedBuilder = new EmbedBuilder();
            embedBuilder.ImageUrl = DogImage;

            await ReplyAsync("", false, embedBuilder);
            client.Dispose();
        }
    }

    public class Cat : ModuleBase<SocketCommandContext>
    {
        [Command("cat")]
        [Alias("rcat", "randcat")]
        public async Task RandomCat()
        {
            Console.WriteLine($"{DateTime.Now}: {Context.User.Username + "#" + Context.User.Discriminator} in {Context.Guild.Name} did a!cat");

            WebClient client = new WebClient();
            string info = client.DownloadString("http://theoldreader.com/kittens/600/400/js");

            await ReplyAsync(info.Split("=")[1].Replace("\" target", "").Replace("\"", ""));
            client.Dispose();
        }
    }
}