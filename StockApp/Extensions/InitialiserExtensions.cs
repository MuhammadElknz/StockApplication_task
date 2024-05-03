﻿using StockApp.Infrastructure.Data;

namespace StockApp.WebApi.Extensions
{
    public static class InitialiserExtensions
    {



        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }
}
