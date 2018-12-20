using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using Features.lib.Games;
using Discord.WebSocket;
using System.Linq;
using Features.lib.Users;
using DiscordBotVS.DbConnections;
using System.Collections;
using Features.lib.Games.Ttt;

namespace DiscordBotVS.Helpers
{
    public class DisplayList
    {
        public static Embed GameList(List<GoodNumber> games)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();
            

            Console.WriteLine($"affichage de GameList {games}");
            int len = games.Count;
            string players = "";
            for (int i = 0; i < len; i++)
            {
                // Get the command Summary attribute information

                string embedFieldText = " nb turn  : " + games[i].nbTurn.ToString();

                int j = 1;
                foreach (ulong id in games[i].Players)
                {
                    User u = DbConnection.FindUserWithDiscordIs(id);
                    players += $"player {j} : {u.Username}  ";
                    j++;
                       
                }
                embedBuilder.AddField($"Game n° {i + 1}  --   " + players, embedFieldText);
            }
           return  embedBuilder.Build();
        }

        public static Embed GameList(List<TicTacToe> games)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();


            Console.WriteLine($"affichage de GameList {games}");
            int len = games.Count;
            string players = "";
            for (int i = 0; i < len; i++)
            {
                // Get the command Summary attribute information

                string embedFieldText = " nb turn  : " + games[i].nbTurn.ToString();

                int j = 1;
                foreach (ulong id in games[i].Players)
                {
                    User u = DbConnection.FindUserWithDiscordIs(id);
                    players += $"player {j} : {u.Username}  ";
                    j++;

                }
                embedBuilder.AddField($"Game n° {i + 1}  --   " + players, embedFieldText);
            }
            return embedBuilder.Build();
        }

     
    }
}
