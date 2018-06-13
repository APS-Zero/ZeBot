using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
namespace ZeBot {
    public class ZeBot {
        static void Main () => new ZeBot ().StartAsync ().GetAwaiter ().GetResult ();

        private DiscordSocketClient _client;

        private CommandHandler _handler;
        public async Task StartAsync () {

            _client = new DiscordSocketClient ();

            await _client.LoginAsync (TokenType.Bot, "");

            await _client.StartAsync ();
            _client.Ready += async () => {
                Console.Title = "ZeBot (BETA)";

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write ("Logged in as ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine ("ZeBot.");

            };

            _handler = new CommandHandler (_client);

            await Task.Delay (-1);

        }

    }
}
