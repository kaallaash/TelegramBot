using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotHelpers
{
    public class Button
    {
        public static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "/my-id"},
                        new KeyboardButton { Text = "/reg-date" } },
                    new List<KeyboardButton>{ new KeyboardButton{ Text = "/date"},
                        new KeyboardButton { Text = "/time" },
                        new KeyboardButton { Text = "/day" } }
                }
            };
        }
    }
}
