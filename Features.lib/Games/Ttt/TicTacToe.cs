using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Features.lib.Games.Ttt
{
    public class TicTacToe : Game
    {

        private Dictionary<string, string> messages =
            new Dictionary<string, string>{
                { "start", "Starting the Game!"},
                { "badInput", "Impossible to play this position!"},
                { "badPlayer", "Not this player Turn!"},
                { "possiblePlays", "A1    A2    A3\nB1    B2    B3\nC1    C2    C3" }
                                            };

        //Contructeurs
        public TicTacToe(List<ulong> players, double[] rewards)
            : base(players, rewards)
        {
            Init();
        }
        public TicTacToe(List<ulong> players)
            : base(players, new double[] { 70, 35 })
        {
            Init();
        }

        private void Init()
        {
            Pawns = new string[2] { "X", "O" };
            //GameTable = new string[9] ;
            GameTable = new Table();
            PossiblePlays = new List<string> { "A1", "A2", "A3",
                                               "B1", "B2", "B3",
                                               "C1", "C2" ,"C3" };
            RemainingPlays = new List<string> { "A1", "A2", "A3",
                                               "B1", "B2", "B3",
                                               "C1", "C2" ,"C3" };
            GameStatus = messages["start"];
        }

        //Attributs 
        public string[] Pawns { get; private set; }
        public Table GameTable { get; private set; }
        public List<string> PossiblePlays { get; private set; }
        public List<string> RemainingPlays { get; private set; }

        public string GameStatus { get; private set; }

        //Methodes
        public string CurrentPlayerPawn
        {
            get { return Pawns[CurrentPlayerIndex]; }
            private set { }
        }


        public void Turn(ulong playerDiscordId, string played)
        {
            if (IsFinished != RunningStatus.Running)
            {
                GameStatus = "Game is finish, Please start a new Game!";
                return;
            }
            if (playerDiscordId != CurrentPlayer)
            {
                GameStatus = messages["badPlayer"] + $", Player n° {CurrentPlayerIndex} must play!";
                return;
            }
            if (!RemainingPlays.Contains(played))
            {
                GameStatus = messages["badInput"] + $" {played} " +
                    $"\nPossibles plays : {DisplayArray(RemainingPlays)}";
                return;
            }
            GameStatus = $"Player {CurrentPlayerIndex + 1}, plays {played}";
            int index = PossiblePlays.FindIndex(str => str == played);
            RemainingPlays.Remove(played);

            GameTable.Insert(index, CurrentPlayerPawn);
            if (GameTable.GoodAlign(CurrentPlayerPawn))
            {
                IsFinished = RunningStatus.Finished;
                // Add the winner then the second in the FinalRank list
                FinalRank.Add(CurrentPlayer);

                int secondIndex = (CurrentPlayerIndex + 1) % 2;
                FinalRank.Add(Players[secondIndex]);
                Console.WriteLine($"\nFinal Ranck {FinalRank[0]}, {FinalRank[1]}");   
            }
            if (IsFinished == RunningStatus.Running)
                MoveToNextPlayer();
            //Cas Match null
            if (IsFinished == RunningStatus.Running && nbTurn == 9)
                IsFinished = RunningStatus.FinishNull;

        }

        public string TableToStr()
        {
            string str, str1, str2;
            str2 = "{0} | {1} | {2} | {3} |\n";
            str1 = "  +---+---+---+\n";
            str =  "    1   2  3\n";
            int i = 0;
            char j = 'A';
            while (i < 9)
            {
                str += str1;
                str += String.Format(str2, j, GameTable.Cases[i], GameTable.Cases[i + 1], GameTable.Cases[i + 2]);
                i += 3;
                j++;
            }
            str += str1;
            return str;
        }

        public string TableToStrDiscord()
        {
            string res = "";
            int i = 0, j = 0;
            char c = 'A';
            
            res += "        1      2      3\n";
            foreach (string str in GameTable.Cases)
            {
                 
                if (i == 0)
                {
                    // permet de repositionner la 2eme ligne pourqu'elle soit bien allignée avec les autres
                    if (j == 3)
                        res += c + "    ";
                    else
                        res += c + "   ";
                    c++;
                }
                if (str == "X")
                    res += ":red_circle:  ";
                else if (str == "O")
                    res += ":white_circle:  ";
                else
                    res += ":black_circle:  ";
                i++;
                if (i == 3)
                {
                    res += "\n";
                    i = 0;
                }
                j++;
            }
            return res; 
        }

        public string DisplayArray(List<string> ar)
        {
            return String.Join(" ", ar);
        }
        public override string ToString()
        {
            return GameStatus + "\n" + TableToStr();
        }

        public string ToStringDiscord()
        {
            return GameStatus + "\n\n"  + TableToStrDiscord();
        }
    
        public string DisplayArray(string[] gameTable)
        {
            return String.Join(" | ", gameTable);
        }

    }
}
