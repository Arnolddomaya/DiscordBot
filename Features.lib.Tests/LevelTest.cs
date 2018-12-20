using Features.lib.Levels;
using System;
using Xunit;

namespace Features.lib.Tests
{
    public class LevelTest
    {
        [Theory]
        [InlineData(40, 1, 0)]
        [InlineData(50, 1, 10)]
        [InlineData(40 + 160, 2, 0)]
        [InlineData(40 + 160  + 639, 2, 639)]
        [InlineData(40 + 160 + 640, 3, 0)]
        public void GetXpTest(int xp, int expecLevel, int expecXp)
        {
            Level l = new Level(xp);
            Assert.Equal(expecLevel, l.CurrentLevel);
            Assert.Equal(expecXp, l.CurrentXp);
        }

        [Theory]
        [InlineData(10, 20, 0)]
        [InlineData(30, 0, 1)]
        [InlineData(41, 11, 1)]
        public void GetAddToCurrentXp(double newXp, double expecXp, double expecLevel)
        {
            Level l = new Level(10);
            l.GetXp(newXp);
            Assert.Equal(expecLevel, l.CurrentLevel);
            Assert.Equal(expecXp, l.CurrentXp);
        }

        [Theory]
        [InlineData(10, 10, 0)]
        [InlineData(30, 30, 0)]
        [InlineData(41, 01, 1)]
        public void EmptyConstructor(double newXp, double expecXp, double expecLevel)
        {
            Level l = new Level();
            l.GetXp(newXp);
            Assert.Equal(expecLevel, l.CurrentLevel);
            Assert.Equal(expecXp, l.CurrentXp);
        }
    }
}
