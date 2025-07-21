using Microsoft.EntityFrameworkCore;
using MythsApi.Core.Entities;
using MythsApi.Infrastructure.Data;

namespace MythsApi.Api.Migrate
{
    public static class DatabaseInitializer
    {
        public static void InitializeAndSeedDBIfNotFound(this  IServiceProvider serviceProvider,bool useSqlDB)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MythsDbContext>();
            if (useSqlDB)
            {

                // Apply any pending migrations
                context.Database.Migrate();
            }
            else
            {
                // For InMemory, no migration is needed — just ensure database is created
                context.Database.EnsureCreated();
            }

            // Seed data if not already present
            if (context.Pantheons.Any()) return;


            var greek = new Pantheon { Name = "Greek" };
            var hindu = new Pantheon { Name = "Hindu" };
            var norse = new Pantheon { Name = "Norse" };

            context.Pantheons.AddRange(greek, hindu, norse);
            context.SaveChanges();

            var deities = new List<Deity>
                {
                new() { Name = "Zeus", Gender = "Male", Description = "Sky god", PantheonId = greek.Id },
                new() { Name = "Athena", Gender = "Female", Description = "Wisdom and war", PantheonId = greek.Id },
                new() { Name = "Apollo", Gender = "Male", Description = "God of sun and music", PantheonId = greek.Id },
                new() { Name = "Artemis", Gender = "Female", Description = "Goddess of the hunt", PantheonId = greek.Id },

                new() { Name = "Shiva", Gender = "Male", Description = "Destroyer", PantheonId = hindu.Id },
                new() { Name = "Vishnu", Gender = "Male", Description = "Preserver", PantheonId = hindu.Id },
                new() { Name = "Kali", Gender = "Female", Description = "Goddess of destruction", PantheonId = hindu.Id },

                new() { Name = "Odin", Gender = "Male", Description = "Allfather", PantheonId = norse.Id },
                new() { Name = "Thor", Gender = "Male", Description = "Thunder god", PantheonId = norse.Id },
                new() { Name = "Loki", Gender = "Male", Description = "Trickster god", PantheonId = norse.Id }

                };

            context.Deities.AddRange(deities);
            context.SaveChanges();
            var inMemoryIndicator = useSqlDB ? "" : "-in memory db";
            context.Myths.AddRange(
            // Greek
            new Myth { Title = $"Birth of Athena {inMemoryIndicator}", Story = "Sprang from Zeus’s head.", Region = "Greek", OriginPeriod = "Ancient Greece", DeityId = deities[1].Id },
            new Myth { Title = "Apollo and the Python", Story = "Apollo slays the python to claim Delphi.", Region = "Greek", OriginPeriod = "Ancient Greece", DeityId = deities[2].Id },
            new Myth { Title = "Artemis and Actaeon", Story = "Actaeon sees Artemis bathing and is turned into a stag.", Region = "Greek", OriginPeriod = "Ancient Greece", DeityId = deities[3].Id },

            // Hindu
            new Myth { Title = "Tandava", Story = "Shiva performs the dance of destruction.", Region = "India", OriginPeriod = "Ancient India", DeityId = deities[4].Id },
            new Myth { Title = "Vishnu’s Avatar - Narasimha", Story = "Vishnu takes lion-man form to slay demon Hiranyakashipu.", Region = "India", OriginPeriod = "Ancient India", DeityId = deities[5].Id },
            new Myth { Title = "Kali vs Raktabija", Story = "Kali destroys the demon whose blood spawns clones.", Region = "India", OriginPeriod = "Ancient India", DeityId = deities[6].Id },

            // Norse
            new Myth { Title = "Ragnarok", Story = "End of Norse gods.", Region = "Norse", OriginPeriod = "Viking Age", DeityId = deities[7].Id },
            new Myth { Title = "Thor and the Midgard Serpent", Story = "Thor battles Jörmungandr during Ragnarok.", Region = "Norse", OriginPeriod = "Viking Age", DeityId = deities[8].Id },
            new Myth { Title = "Loki’s Punishment", Story = "Loki is chained under a serpent for his betrayal.", Region = "Norse", OriginPeriod = "Viking Age", DeityId = deities[9].Id },
            new Myth { Title = "Zeus and Prometheus", Story = "Zeus punishes Prometheus for giving fire to man.", Region = "Greek", OriginPeriod = "Ancient Greece", DeityId = deities[0].Id }

            );
            // persisit data
            context.SaveChanges();
        }
    }
}
