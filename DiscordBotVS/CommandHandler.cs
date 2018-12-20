using Discord.Commands;
using Discord.WebSocket;
using DiscordBotVS.DbConnections;
using Features.lib.Levels;
using Features.lib.Users;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotVS
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
       

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;

            _commands.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;
            _client.Ready += Client_Ready;
            _client.UserJoined += _client_UserJoined;
            //_client.UserLeft += _client_UserLeft;
           
        }

        private Task _client_UserLeft(SocketGuildUser arg)
        {
            throw new NotImplementedException();
        }

        private Task _client_UserJoined(SocketGuildUser arg)
        {
            if (!DbConnection.CheckUserExist(arg.Id))
            {
                DbConnection.SetUserInDB(arg.Id, arg.Username);
            }

            return Task.CompletedTask;
        }

        private Task Client_Ready()
        {

            
            foreach (var guild in _client.Guilds)
            {
                foreach(var user in guild.Users)
                {

                    
                    if (!DbConnection.CheckUserExist(user.Id))
                    {                      
                       DbConnection.SetUserInDB(user.Id, user.Username);
                    }
                                       
                }
            }                  
            return Task.CompletedTask; 
        }

        /*private async Task ClientOnChannelCreated()
        {

        }*/

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;

            if (msg.HasCharPrefix('!', ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
