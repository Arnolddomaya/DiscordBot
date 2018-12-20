using Features.lib.Levels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Features.lib.Users
{
    public class User
    {
       
        //Constructeurs
        public User() {}

        public User( ulong discordId, string name )
        {
            Init(discordId, name);
            UserLevel = new Level();
        }

        public User(ulong discordId , string name, Level level)
        {
            Init(discordId, name);
            UserLevel = level;
        }

        private void Init(ulong discordId, string name)
        {
            Username = name;
            DiscordId = discordId;
        }

        // Attributs 
        [Key]
        public int Id { get; set; }
        public ulong DiscordId { get; private set; }
        public string Username { get; set; }

        //Attributs relationnel
        public Level UserLevel { get; private set; }

        //Methodes
        public override bool Equals(object obj)
        {
            var item = obj as User;
            if (item == null) return false;

            return this.DiscordId == item.DiscordId;
        }

        public override int GetHashCode()
        {
            return this.DiscordId.GetHashCode();
        }
    }
}
