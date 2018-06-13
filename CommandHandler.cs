using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Reflection;

namespace ZeBot
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommand;
            _client.Connected += Conected;
        }
        private async Task Conected()
        {
           
            int count =_client.Guilds.Count;
            await _client.SetGameAsync($"+help");

        }

 
        public async Task HandleCommand(SocketMessage s)
        {

            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var Context = new SocketCommandContext(_client, msg);

            int argPos = 0;

            if (msg.HasCharPrefix('-', ref argPos))
            {
                var result = await _service.ExecuteAsync(Context, argPos);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.ErrorReason);
                }
            }

        }

    }
}
