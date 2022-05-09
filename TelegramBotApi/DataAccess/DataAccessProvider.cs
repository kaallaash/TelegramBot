using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotApi.Models;

namespace TelegramBotApi.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly AppDbContext context;

        public DataAccessProvider(DbContext context)
        {
            this.context = (AppDbContext)context;
        }

        public async Task AddChat(Chat chat)
        {
            if (!await context.Chats.AnyAsync(c => c.Id == chat.Id))
            {
                await context.Chats.AddAsync(chat);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateChat(Chat chat)
        {
            var chatById = await context.Chats.FirstOrDefaultAsync(c => c.Id == chat.Id);

            if (chatById is not null)
            {
                chatById.RegistrationDate = chat.RegistrationDate;
                await context.SaveChangesAsync();
            }
        }

        public async Task<Chat> GetChat(long chatId)
        {
            return await context.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task<List<Chat>> GetChats()
        {
            return await context.Chats.ToListAsync();
        }
    }
}
