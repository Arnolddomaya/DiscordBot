using Discord;
using Discord.Commands;
using DiscordBotVS.Data;
using DiscordBotVS.DbConnections;
using Features.lib.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotVS.modules
{
    [Name("Level")]
    [Group("Level")]
    [Summary("Get info about users Levels ")]
    public class LevelCommands : ModuleBase<SocketCommandContext> 
    {
        [Command("i")]
        [Summary("Get the message author level")]
        public async Task Say()
        {         
            User u;
            u = DbConnection.FindUserWithDiscordIs(Context.Message.Author.Id);

            var builder = new EmbedBuilder()
            {
                Color = new Discord.Color(114, 137, 218),
                Description = $"Message Author Level"
            };

            builder.AddField($"{Context.Message.Author.Username}    Level : {u.UserLevel.CurrentLevel}   Xp : {u.UserLevel.CurrentXp}", "---");
            await ReplyAsync("", false, builder.Build());
        }

        [Command("all")]
        [Summary("Get all Users levels")]
        public async Task Say1()
        {
            var builder = new EmbedBuilder()
            {
                Color = new Discord.Color(114, 137, 218),
                Description = $"all users levels"
            };
            foreach ( User u in DbConnection.GetAllUsers())
            {
                var iU = await Context.Channel.GetUserAsync(u.DiscordId) as IUser;
                
                if (iU != null)
                    builder.AddField(x =>
                    {
                        x.Name = $"{iU.Username}";
                        x.Value = $"Level : {u.UserLevel.CurrentLevel}\n" +
                                  $"Xp    : {u.UserLevel.CurrentXp}";
                        x.IsInline = false;
                    });
            }

            await ReplyAsync("", false, builder.Build());
        } 
    }
}
