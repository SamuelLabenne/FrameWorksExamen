/*using Microsoft.AspNetCore.Http;
using FrameWorksExamen.Data;
using FrameWorksExamen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FrameWorksExamen.Services
{
    public class SessionUser
    {
        class UserStats
        {
            public DateTime LastEntered { get; set; }
            public int Count { get; set; }
            public User User { get; set; }
        }


        readonly RequestDelegate _next;
        static Dictionary<string, UserStats> UserDictionary = new Dictionary<string, UserStats>();

        public SessionUser(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ApplicationDbContext dbContext)
        {
           
            

            string name = httpContext.User.Identity.Name == null ? "-" : httpContext.User.Identity.Name;
            try
            {
                UserStats us = UserDictionary[name];
                us.Count++;
                us.LastEntered = DateTime.Now;
            }
            catch
            {
                UserDictionary[name] = new UserStats
                {
                    User = dbContext.Users.FirstOrDefault(u => u.UserName == name),
                    Count = 1,
                    LastEntered = DateTime.Now
                };
            }

            await _next(httpContext);
        }

        public static User GetUser(HttpContext httpContext, ApplicationDbContext dbContext)
        {
            var userName = httpContext.User.Identity.Name;

            if (UserDictionary.ContainsKey(userName))
            {
                var userStats = UserDictionary[userName];
                if (userStats.User != null)
                {
                    return userStats.User;
                }
            }

            // If user not found in UserDictionary or User object is null,
            // retrieve the user from dbContext.Users
            return dbContext.Users.FirstOrDefault(u => u.UserName == userName);
        }
    }
}*/
