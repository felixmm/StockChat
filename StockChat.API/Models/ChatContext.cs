﻿using Microsoft.EntityFrameworkCore;
namespace StockChat.API.Models
{
    public class ChatContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=stockchat.db");
    }
}
