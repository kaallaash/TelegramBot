using Microsoft.EntityFrameworkCore;
using System;
using TelegramBotApi.Models;

namespace TelegramBotApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    }
}
