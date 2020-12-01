using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TrueLayer.Entities
{
    public class PokemonDBContext : DbContext
    {
        private static bool _created = false;
        public PokemonDBContext()
        {
            if (!_created)
            {
                _created = true;
                if (Database != null)
                {
                    Database.EnsureDeleted();
                    Database.EnsureCreated();
                }
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=d:\Pokemon.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PokemonEntity>().HasKey(c => c.name);
        }
        public DbSet<PokemonEntity> Pokemons { get; set; }
    }
}
