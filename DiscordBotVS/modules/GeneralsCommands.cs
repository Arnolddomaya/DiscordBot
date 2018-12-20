using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotVS.modules
{
    public class GeneralsCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Ping"), Summary("Basical test, Display Pong!")]
        public async Task Two()
        {
            //Console.WriteLine("in Ping");
            await Context.Channel.SendMessageAsync("Pong!");
        }


        [Command("UserId"), Summary("Id of the msg author!")]
        public async Task Users()
        {

            await Context.Channel.SendMessageAsync("Username : "+ Context.Message.Author.Username +" , id : " + Context.Message.Author.Id.ToString());
           
            //await Context.Channel.SendMessageAsync(Context.Client.GetGuild(122).Users.ToString());

        }
        
    }
}
