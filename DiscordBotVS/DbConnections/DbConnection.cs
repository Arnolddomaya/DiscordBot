using DiscordBotVS.Data;
using Features.lib.Levels;
using Features.lib.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBotVS.DbConnections
{
    public class DbConnection
    {
        public static bool CheckUserExist(ulong id )
        {
            Console.WriteLine();
            Console.WriteLine("in DBconnection.UserExist() ");

            User u = FindUserWithDiscordIs(id);
            if (u == null)
            {
                Console.WriteLine($"Db connection, user {id} doesnt exist in db");
                return false;
            }
            return true;           
        }

        public static User FindUserWithDiscordIs(ulong id)
        {
            User u;
            Console.WriteLine("In findUser ");
            using (var context = new ApplicationDbContext())
            {
                u = context.Users
                    .Include(e => e.UserLevel)
                    .FirstOrDefault(e => e.DiscordId == id);
            }
            if (u != null){
                Console.WriteLine($"1--Username {u.Username}, " +
                                  $"DiscordId {u.DiscordId}, " +
                                  $"level {u.UserLevel.CurrentLevel}!");
            }

            Console.WriteLine("Exit FindUser! ");
            return u;
        }

        public static void SetUserInDB(ulong userId, string name)
        {

            Console.WriteLine();
            Console.WriteLine("in SetUserInDb()");
            Console.WriteLine($"Try to insert user {userId} in DBB ");
            using (var context = new ApplicationDbContext())
            {
                var u = new User(userId, name);
                context.Users.Add(u);
                context.SaveChanges();
            }
            Console.WriteLine($"Inserting in DBB Success");

        }

        public static void SetRewardToUser(List<ulong> userIds, double[] rewards)
        {
            for(int i = 0; i < userIds.Count; i++)
            {
                Console.WriteLine($"Set Reward {i}");
                SetRewardToUser(userIds[i], rewards[i]);
            }
        }

        public static void SetRewardToUser(ulong userId, double reward)
        {
            Console.WriteLine();
            Console.WriteLine("in set Reward!");
            User u;
            using (var context = new ApplicationDbContext())
            {
                u = context.Users.FirstOrDefault(s => s.DiscordId == userId);
                Level l = context.Levels.FirstOrDefault(s => s.UserId == u.Id);
                Console.WriteLine($"1-- id {u.DiscordId}, level {l.CurrentLevel}!");
                l.GetXp(reward);

                //context.Update(u);
                context.SaveChanges();
                Console.WriteLine($"1-- id {u.DiscordId}, level {l.CurrentLevel}!");
            }
        }

        public static List<User> GetAllUsers()
        {
            List<User> l;
            using (var context = new ApplicationDbContext())
            {
                l = context.Users
                    .Include( u => u.UserLevel)
                    .ToList();
            }
            return l;
        }
    }



}
