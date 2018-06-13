using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord;
using SkiaSharp;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace ZeBot.Modules {

    public class Meme : ModuleBase<SocketCommandContext> {
        [Command ("meme")]
        public async Task Tett (string url, [Remainder] string Text) {

            Console.WriteLine($"{DateTime.Now}: {Context.User.Username + "#" + Context.User.Discriminator} in {Context.Guild.Name} did z!meme {url} {Text}");

            if (3 >= Text.Length) {
                await Context.Channel.SendMessageAsync ("**Whoops**, sorry but I can't work with only 3 letters");
                return;
            }
            await Context.Channel.SendMessageAsync ("**Notice**: This bot was made using a bot called BotMaker check the command : `links`");
            await Context.Channel.SendMessageAsync ("**Please** wait while I create an image for you....");

            WebClient web = new WebClient ();
            web.DownloadFile (url, @".\BG.png");
            Console.WriteLine ("downloaded");
            SKBitmap bbg = SKBitmap.Decode (@".\BG.png");
            Console.WriteLine ("decoded");
            var info = new SKImageInfo (bbg.Width, bbg.Height);
            using (var surface = SKSurface.Create (info)) {
                var canvas = surface.Canvas;
                var font = SKTypeface.FromFamilyName ("Impact");
                

                var paint = new SKPaint {
                    Color = SKColors.White,
                    IsAntialias = true,
                    StrokeCap = SKStrokeCap.Round,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Center,
                    TextSize = (info.Width / info.Height) * (info.Width / (Text.Length / 2)),
                    Typeface = font

                };
                var paint2 = new SKPaint {
                    Color = SKColors.Black,
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    TextAlign = SKTextAlign.Center,
                    TextSize = (info.Width / info.Height) * (info.Width / (Text.Length / 2)),

                    Typeface = font
                };

                var coord = new SKPoint (0, 0);
                var textcoord = new SKPoint (info.Width / 2, info.Height - 20);
                canvas.DrawBitmap (bbg, coord);
                Console.WriteLine ("Drawn");
                canvas.DrawText (Text.ToUpper (), textcoord, paint);
                canvas.DrawText (Text.ToUpper (), textcoord, paint2);

                using (var image = surface.Snapshot ())
                using (var data = image.Encode (SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite (@".\output.png")) {
                    data.SaveTo (stream);
                }
                await Context.Channel.SendFileAsync (@".\output.png");

            }
        }
    }

    public class Dog : ModuleBase<SocketCommandContext> {
        [Command ("dog")]
        [Alias ("rdog", "randdog")]
        public async Task RandomDog () {
            Console.WriteLine ($"{DateTime.Now}: {Context.User.Username + "#" + Context.User.Discriminator} in {Context.Guild.Name} did z!dog");

            WebClient client = new WebClient ();
            string info = client.DownloadString ("https://dog.ceo/api/breeds/image/random");
            var DogImage = ((info.Replace ("\" target", "").Replace ("\"", "").Replace ("status", "").Replace ("success", "").Replace ("message", "").Replace (",", "").Replace ("{", "").Replace ("}", "")).Replace ("::", "").Replace ("\\", ""));

            EmbedBuilder embedBuilder;
            embedBuilder = new EmbedBuilder ();
            embedBuilder.ImageUrl = DogImage;

            await ReplyAsync ("", false, embedBuilder);
            client.Dispose ();
        }
    }

    public class Cat : ModuleBase<SocketCommandContext> {
        [Command ("cat")]
        [Alias ("rcat", "randcat")]
        public async Task RandomCat () {
            Console.WriteLine ($"{DateTime.Now}: {Context.User.Username + "#" + Context.User.Discriminator} in {Context.Guild.Name} did z!cat");

            WebClient client = new WebClient ();
            string info = client.DownloadString ("http://theoldreader.com/kittens/600/400/js");

            await ReplyAsync (info.Split ("=") [1].Replace ("\" target", "").Replace ("\"", ""));
            client.Dispose ();
        }
    }
}