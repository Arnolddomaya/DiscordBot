using Discord;
using Discord.Commands;
using DiscordBotVS.DbConnections;
using DiscordBotVS.Helpers;
using Features.lib;
using Features.lib.Games;
using Features.lib.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotVS.modules
{
    // Create a module with the 'goodNum' prefix
    [Name("goodNum")]
    [Group("gN")]
    public class GoodNumCommands : ModuleBase<SocketCommandContext>
    {
        

        private static List<GoodNumber> GoodNumberList = new List<GoodNumber>();
        //enlever le jeu de la liste( quand y'a plusieurs jeu en même temps) quand c'est fini


        [Command("List"), Summary("Display all running games with their player.")]
        public async Task ListAll()
        {
            await ReplyAsync("List of running GoodNum games : ", false,
                            DisplayList.GameList(GoodNumberList));
        }

        [Command("start"), Summary("Param int (default 100) ,  Start the game.")]
        public async Task Three(int max = 100)
        {
            int index = GoodNumberList.FindIndex(a => a.CurrentPlayer == Context.Message.Author.Id);
            if (index != -1)
            {
                GoodNumberList.RemoveAt(index);
                Console.WriteLine("Ecrasement de l' ancienne partie!" );
                await Context.Channel.SendMessageAsync($"Nouvelle Partie!");
            }
            // Check if current user exist in db
            User user = DbConnection.FindUserWithDiscordIs(Context.Message.Author.Id);
            if (user == null)
            {
                Console.WriteLine($"User with DiscordId {Context.Message.Author.Id} not found, please check the DB" );
                return;
            }
            GoodNumber game = new GoodNumber(Context.Message.Author.Id, max );
            Console.WriteLine("target : " + game.target);
            GoodNumberList.Add(game);
            Console.WriteLine("Création de nouvelle partie!");
            await Context.Channel.SendMessageAsync($"Trouver le bon nombre entre 1 et {max}!");
        }

        [Command("check"), Summary("Param int , check if input num is the target.")]
        public async Task GoodNumberCheck(int number)
        {
            int index = GoodNumberList.FindIndex(a => a.CurrentPlayer == Context.Message.Author.Id);

            if (index == -1)
            {
                await Context.Channel.SendMessageAsync("Start the game before lauching this command (with '!gn start')!");
                return;
            }

            Console.WriteLine("target (dans check): " + GoodNumberList[index].target);
            await Context.Channel.SendMessageAsync(GoodNumberList[index].Check(number));
            if (GoodNumberList[index].won)
            { 
                DbConnection.SetRewardToUser(GoodNumberList[index].CurrentPlayer, GoodNumberList[index].Reward[0]);

                GoodNumberList.RemoveAt(index);
                Console.WriteLine("Partie retiré de la liste!");
            }
        }
    }
}
