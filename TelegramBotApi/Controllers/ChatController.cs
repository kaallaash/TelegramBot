using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotApi.DataAccess;
using TelegramBotApi.Models;
using TelegramBotHelpers;

namespace TelegramBotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IDataAccessProvider dataAccessProvider;

        public ChatController(IDataAccessProvider dataAccessProvider)
        {
            this.dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        [Route("GetRegistrationDate")]
        public async Task<DateTime> GetRegistrationDate(long chatId)
        {
            var chat = await dataAccessProvider.GetChat(chatId);

            if (chat is not null)
            {
                return chat.RegistrationDate;
            }

            return DateTime.Now;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(long id)
        {
            var chat = new Chat { Id = id, RegistrationDate = DateTime.Now };
            await dataAccessProvider.AddChat(chat);
            return Ok();
        }

        [HttpPost]
        [Route("SendMessageToAll")]
        public async Task<IActionResult> SendMessageToAll(string token, string message)
        {
            var client = new TelegramBotClient(token);
            var chats = await dataAccessProvider.GetChats();

            for (int i = 0; i < chats.Count; i++)
            {
                if (i % 30 == 0)
                {
                    Thread.Sleep(1000);
                }

                try
                {
                    await client.SendTextMessageAsync(chats[i].Id, message,
                        replyMarkup: Button.GetButtons());
                }
                catch { }
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Chat chat)
        {
            await dataAccessProvider.UpdateChat(chat);
            return Ok();
        }
    }
}
