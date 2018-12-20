using Features.lib.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Features.lib.Games
{
    
    public class GoodNumber : Game
    {
   
        private int max;
        
        public int target { get; private set; }
        public bool won { get; private set; }
        private string baseMsg = "Le nombre cherché est ";
        private string[] msg = {"Trouvé, Bravo!", "Beacoup plus petit! ", "Plus petit!", "Plus grand!", "Beaucoup plus grand!" };

        // On choisit d'utiliser ulong dans les param de GoodNumber au lieu de User
        // pour ne pas avoir à créer une instance de User à chaque fois dans les Tasks 
        // Bon choix??
        public GoodNumber( ulong discordId,  int pmax = 100) 
            : base(new List<ulong>{ discordId }, new double[1] {70})
        {
            max = pmax;
            target = new Random().Next(1, max + 1);
            won = false; 
        }
        
        public string Check(int nb)
        {
            nbTurn++;
            int dValeur = 40;
            if (nb == target)
            {
                won = true;
                return msg[0];
            }
            int delta =  target - nb;
            if (delta < -dValeur) return baseMsg + msg[1];
            else if (delta >= -dValeur && delta < 0 ) return baseMsg + msg[2];
            else if (delta > 0 && delta <= dValeur) return baseMsg + msg[3];
            else   return baseMsg + msg[4];
        }

    }
}
