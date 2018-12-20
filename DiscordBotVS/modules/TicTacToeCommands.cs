using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotVS.DbConnections;
using DiscordBotVS.Helpers;
using Features.lib.Games;
using Features.lib.Games.Ttt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotVS.modules
{
    [Name("TicTacToe")]
    [Group("ttt")]
    [Summary("Commands of TicTacToe game ")]
    public class TicTacToeCommands : ModuleBase<SocketCommandContext>
    {
        private static List<TicTacToe> TicTacToeList = new List<TicTacToe>();

        [Command("List"), Summary("Display all running games with their player.")]
        public async Task ListAll()
        {
            await ReplyAsync("List of running TicTacToe games : ", false,
                            DisplayList.GameList(TicTacToeList));
        }

        [Command("mine"), Summary("Get all TicTacToe games of message author!")]
        public async Task mine(string opponent)
        {

        }

        [Command("start"), Summary("Param string (username of the oponent) ,  Start the game!")]
        public async Task start(string opponent)
        { 
            //Get the second player by his Username
            var player2 = Context.Guild.Users.First(x => x.Username == opponent) as IUser;
            Console.WriteLine($"Opponent {opponent}, Player 2  {player2}");
            if (player2 != null)
                Console.WriteLine($"Opponent {opponent}, Player 2  {player2}, id {player2.Id}");


            if (player2 == null)
            {
                await Context.Channel.SendMessageAsync($"No User with Username {opponent}");
                return;
            }
            //Check if there is a game, in the list, with those 2 players
            List<ulong> lst = new List<ulong> { Context.Message.Author.Id, player2.Id };
            int index = TicTacToeList.FindIndex(a => Enumerable.SequenceEqual(lst, a.Players));

            //If there is already a game, erase it for starting a new game
            if (index != -1)
            {
                TicTacToeList.RemoveAt(index);
                Console.WriteLine("Ecrasement de l' ancienne partie!");
                await Context.Channel.SendMessageAsync($"Nouvelle Partie!");
            }

            //add a new game in game List
            TicTacToe game = new TicTacToe(lst);
            TicTacToeList.Add(game);
            Console.WriteLine(game);
            await Context.Channel.SendMessageAsync(game.ToStringDiscord());
        }

        [Command("play"), Summary("Play a pawn , take game number(in gam list) and position as parameters")]
        public async Task play(int nbGame, string position)
        {
            int index = nbGame - 1;
            //if the input is a bad index
            if (TicTacToeList.Count <= index)
            {
                await Context.Channel.SendMessageAsync($"No game with index {nbGame}, Start a new Game or play with right index!");
                return;
            }

            //if the message author is not player of this index game
            if (!TicTacToeList[index].Players.Contains(Context.Message.Author.Id))
            {
                await Context.Channel.SendMessageAsync($"Can't play in this game, check game list to fine right game's number!");
                return;
            }

            //if it's not message author turn's to play
            if (Context.Message.Author.Id != TicTacToeList[index].CurrentPlayer)
            {
                await Context.Channel.SendMessageAsync($"Not your turn to play!");
                return;
            }
            TicTacToeList[index].Turn(Context.Message.Author.Id, position);
            Console.WriteLine(TicTacToeList[index]);
            Console.WriteLine(TicTacToeList[index].DisplayArray(TicTacToeList[index].GameTable.Cases));
            await Context.Channel.SendMessageAsync(TicTacToeList[index].ToStringDiscord());

            if (TicTacToeList[index].IsFinished == RunningStatus.Finished)
            {
                await Context.Channel.SendMessageAsync($"Jeu finit " +
                    $"{DbConnection.FindUserWithDiscordIs(TicTacToeList[index].CurrentPlayer).Username} a gagné!");

                DbConnection.SetRewardToUser(TicTacToeList[index].FinalRank, TicTacToeList[index].Reward);
                TicTacToeList.RemoveAt(index);
            }
            else if (TicTacToeList[index].IsFinished == RunningStatus.FinishNull)
            {
                await Context.Channel.SendMessageAsync($"Jeu finit " +
                    $"Match Null!");

                //double egalityReward = (TicTacToeList[index].Reward[0] + TicTacToeList[index].Reward[0]) / 2
                DbConnection.SetRewardToUser(TicTacToeList[index].Players, new double[] { 50, 50});
                TicTacToeList.RemoveAt(index);
            }


        }
    }
}
