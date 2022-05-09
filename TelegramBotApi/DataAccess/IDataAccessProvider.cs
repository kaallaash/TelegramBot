using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBotApi.Models;

namespace TelegramBotApi.DataAccess
{
    public interface IDataAccessProvider
    {
        Task AddChat(Chat chat);
        Task UpdateChat(Chat chat);
        Task<Chat> GetChat(long chatId);
        Task<List<Chat>> GetChats();
    }
}
