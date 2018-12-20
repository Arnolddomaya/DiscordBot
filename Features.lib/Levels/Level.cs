using Features.lib.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Features.lib.Levels
{
    public class Level
    {

        public Level()
        {
            init();
        }

        public Level(int xp)
        {
            init();
            GetXp(xp);           
        }

        private void init()
        {
            CurrentLevel = 0;
            CurrentXp = 0;
            // distribution des niveaux de level en fonction des puissance de powerOf 
            powerOf = 4;
            MaxLevel = 4;
            XpMax = SumOfPowers(MaxLevel);
        }

        // Attributs
        [Key]
        public int Id { get; set; }
        public double CurrentXp { get; private set; }
        public double CurrentLevel { get; private set; }
        private double powerOf; // distribution de la courbe des levels en fonction des puissance de powerOf
        public double XpMax { get; private set; }
        public double MaxLevel { get; private set; }

        // Attributs Relationnels
        public int UserId { get; set; }
        public User User { get; set; }


        //methods
        public void GetXp(double xp)
        {
            xp = CurrentXp + xp;
            if (xp >= XpMax)
            {
                CurrentLevel = MaxLevel;
                CurrentXp = 0;
                return;
            }
            while (xp >= Math.Pow(powerOf, (CurrentLevel + 1)) * 10)
            {
                //Console.WriteLine($"entrée boucle xp {xp}");
                xp -=  Math.Pow(powerOf, (CurrentLevel + 1)  ) * 10;
                //Console.WriteLine($"sortie boucle xp {xp}");
                CurrentLevel++;
                //.WriteLine($"current level {CurrentLevel}");
            }
            CurrentXp = xp;
        }

        public double SumOfPowers(double nb)
        {
            double res = 0;
            for (double i = 1; i <= nb; i++)
            {
                res += Math.Pow(powerOf, i) * 10;
            }

            return res;
        }
    }
}
