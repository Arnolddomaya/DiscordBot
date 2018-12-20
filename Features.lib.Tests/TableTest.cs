using Features.lib.Games.Ttt;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.lib.Tests
{

    public class TableTest
    {

        [Theory]
        [InlineData(new int[] { }, "X", false)]
        [InlineData(new int[] { 1, 2 }, "X", false)]
        [InlineData(new int[] { 0, 1, 2 }, "X", true)]
        [InlineData(new int[] { 3, 4, 5 }, "X", true)]
        [InlineData(new int[] { 6, 7, 8 }, "X", true)]
        [InlineData(new int[] { 0, 3, 6 }, "X", true)]
        [InlineData(new int[] { 1, 4, 7 }, "X", true)]
        [InlineData(new int[] { 2, 5, 8 }, "X", true)]
        [InlineData(new int[] { 0, 4, 8 }, "X", true)]
        [InlineData(new int[] { 2, 4, 6 }, "X", true)]
               
        public void WinOTest(int[] pos, string pawn, bool expectWin) 
        {
            Table t = new Table();
            foreach (int i in pos)
                t.Insert(i, pawn);
            Assert.Equal(t.GoodAlign(pawn), expectWin);
        }
    }

}
