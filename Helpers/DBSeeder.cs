using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FleetPulse_BackEndDevelopment.Helpers;
public class DBSeeder
{
    public static void Seed(FleetPulseDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

       dbContext.Database.EnsureCreated();

        var executionStrategy = dbContext.Database.CreateExecutionStrategy();

        executionStrategy.Execute(
            () => {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (!dbContext.Users.Any())
                        {
                            var usersData = File.ReadAllText("./Resources/users.json");
                            var parsedUsers = JsonConvert.DeserializeObject<User[]>(usersData);

                            foreach (var user in parsedUsers)
                            {
                                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);
                            }

                            dbContext.Users.AddRange(parsedUsers);
                            dbContext.SaveChanges();
                        }
                          
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            });
    }
}