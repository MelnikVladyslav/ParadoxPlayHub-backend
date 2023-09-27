using BussnesLogic.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class MockData
    {
        public static void SeedGanres(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ganr>().HasData(new Ganr[]
            {
                new Ganr() { Id = (int)TypeG.Action, Name = "Екшн"},
                new Ganr() { Id = (int)TypeG.RPG, Name = "RPG"},
                new Ganr() { Id = (int)TypeG.Strategy, Name = "Стратегії"},
                new Ganr() { Id = (int)TypeG.Adventure, Name = "Пригодницькі"},
                new Ganr() { Id = (int)TypeG.Racing, Name = "Гонки"},
                new Ganr() { Id = (int)TypeG.Sports, Name = "Спортивні"},
                new Ganr() { Id = (int)TypeG.Horror, Name = "Хоррор"},
                new Ganr() { Id = (int)TypeG.Simulation, Name = "Симуляції"},
                new Ganr() { Id = (int)TypeG.Indie, Name = "Інді"},
                new Ganr() { Id = (int)TypeG.Multiplayer, Name = "Мультиплеєрні"},
                new Ganr() { Id = (int)TypeG.Survival, Name = "Виживання"},
                new Ganr() { Id = (int)TypeG.Arcade, Name = "Аркади"},
                new Ganr() { Id = (int)TypeG.VR, Name = "VR"},
                new Ganr() { Id = (int)TypeG.InteractiveFiction, Name = "Інтерактивні історії"},
            });
        }
    }
}
