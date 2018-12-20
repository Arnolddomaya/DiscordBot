
using Features.lib.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Features.lib.Games
{


    public abstract class Game
    {

        public Game(List<ulong> users)
        {
            Init(users);
            Reward = new double[1] { 50 };
        }

        public Game(List<ulong> users, double[] reward)
        {
            Init(users);
            Reward = reward;
        }

        private void Init(List<ulong> users)
        {
            Players = users;
            CurrentPlayerIndex = 0;
            NbPlayers = Players.Count;
            ToDisplay = "";
            nbTurn = 0;
            IsFinished = RunningStatus.Running;
            FinalRank = new List<ulong>();
        }

        // Attributs
        public List<ulong> Players { get; private set; }
        public List<ulong> FinalRank { get; protected set; }

        protected int CurrentPlayerIndex;
        public ulong CurrentPlayer
        {
            get { return Players[CurrentPlayerIndex]; }
            private set { }
        }
        public int NbPlayers { get; private set; }
        public string ToDisplay { get; private set; }
        public double[] Reward { get; private set; }
        public int nbTurn { get; protected set; }
        public RunningStatus IsFinished { get; protected set; }


        //Methodes 
        public void MoveToNextPlayer()
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % NbPlayers;
            nbTurn++;
        }

        public string PrintGame()
        {
            return ToDisplay;
        }
    }
}
