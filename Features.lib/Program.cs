﻿using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Commands;
using Features.lib.SimpleTests;

namespace DiscordBotVS
{
    class Program
    {
        static void Main(string[] args)
        {

            new Program().StartAsync().GetAwaiter().GetResult();
            //TicTacToeSimpleTests.Test();
        }

        private DiscordSocketClient client;
        private CommandHandler handler;
        private CommandService service;

        public async Task StartAsync()
        {
            client = new DiscordSocketClient();
            service = new CommandService();

            await client.LoginAsync(TokenType.Bot, "NTE3NzE0NjUzMjY3MjMwNzIy.DvJ5JQ.UYR_vunC8HTAiEq4ndmwboFR-6A");
            await client.StartAsync();

            handler = new CommandHandler(client, service );

            await Task.Delay(-1);
        }
    }
}
