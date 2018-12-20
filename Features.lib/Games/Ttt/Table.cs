using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Features.lib.Games.Ttt
{
    public class Table
    {
        //Constructeurs 
        public Table()
        {
            Cases = Enumerable.Repeat(" ", 9).ToArray();
        }

        //attributs
        public string[] Cases { get; private set; }

        public void Insert(int index, string pawn)
        {
            Cases[index] = pawn;
        }

        public bool GoodAlign(string pawn)
        {
            int[,] winCombinations = new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            for (int i = 0; i < 8; i++)
            {

                if (pawn.Equals(Cases[winCombinations[i, 0]]) &&
                    pawn.Equals(Cases[winCombinations[i, 1]]) &&

                    pawn.Equals(Cases[winCombinations[i, 2]]))

                    return true;
                Console.WriteLine();
            }
            return false;
        }

        public override string ToString()
        {
            int i = 0;
            string res = "";
            foreach ( string s in Cases)
            {

                res += s + " ";
                i++;
                if (i == 3)
                {
                    res += "\n";
                    i = 0;
                }
                    
            }
            return res;
        }
    }
}
