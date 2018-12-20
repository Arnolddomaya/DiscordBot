using Features.lib.Games;
using Features.lib.Games.Ttt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Features.lib.SimpleTests
{
    public class TicTacToeSimpleTests
    {
        public static void Test()
        {
            Test1();
        }
        public static void Test2()
        {
            Table t = new Table();
            t.Insert(0, "O");
            t.Insert(1, "O");
            t.Insert(4, "O");
            t.Insert(3 , "X");
            Console.WriteLine(t);


            if (t.GoodAlign("O"))
                Console.WriteLine("Jeu gagné!");
            else
                Console.WriteLine("Pas encore" +
                    " gagné!");


        }

        private Table Table()
        {
            throw new NotImplementedException();
        }

        public static void Test1()
        {
            TicTacToe g = new TicTacToe(new List<ulong> { (ulong)(1), (ulong)(2) });
            Console.WriteLine(g);
            Console.WriteLine($"Current player n° {g.CurrentPlayer}, Pawn {g.CurrentPlayerPawn}");
            //Console.WriteLine($"Possible plays = {g.DisplayArray(g.RemainingPlays)}");
            g.Turn(g.CurrentPlayer, "B3");
            g.Turn(g.CurrentPlayer, "A2");
            g.Turn(g.CurrentPlayer, "C2");
            Console.WriteLine(g);
            //Console.WriteLine($"Possible plays = {g.DisplayArray(g.RemainingPlays)}");
            g.Turn(g.CurrentPlayer, "C3");
            Console.WriteLine(g);
            //g.Turn(g.CurrentPlayer, "C2");
            //Console.WriteLine(g);
            //g.Turn(g.CurrentPlayer, "cdkjfpa&rà");
            //Console.WriteLine(g);
            //Console.WriteLine($"nb tours {g.nbTurn}");
        }
    }
}
