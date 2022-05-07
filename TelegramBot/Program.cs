using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    internal class Program
    {
        private static string token = "5349315331:AAHZXZOrTGBD6UiB9TX8CybX0VXMCUw1YMw";
        private static TelegramBotClient client;
        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            //await client.SendChatActionAsync(replyMarkup: GetButtons());
            if (message.Text != null)
            {
                Console.WriteLine($" Пришло сообщение: {message.Text}");

                switch (message.Text)
                {
                    case "/start":
                        await client.SendTextMessageAsync(message.Chat.Id, "Hello", replyMarkup: GetButtons());
                        break;

                    case "/my-id":
                        await client.SendTextMessageAsync(message.Chat.Id, message.Chat.Id.ToString(), replyMarkup: GetButtons());
                        break;

                    case "/reg-date":
                        await client.SendTextMessageAsync(message.Chat.Id, message.Chat.Id.ToString(), replyMarkup: GetButtons());
                        break;

                    case "/date":
                        await client.SendTextMessageAsync(message.Chat.Id, DateTime.Now.Date.ToString(), replyMarkup: GetButtons());
                        break;

                    case "/time":
                        await client.SendTextMessageAsync(message.Chat.Id, DateTime.Now.TimeOfDay.ToString(), replyMarkup: GetButtons());
                        break;

                    case "/day":
                        await client.SendTextMessageAsync(message.Chat.Id, DateTime.Now.DayOfWeek.ToString(), replyMarkup: GetButtons());
                        break;
                }

            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "/my-id"}, new KeyboardButton { Text = "/reg-date" } },
                    new List<KeyboardButton>{ new KeyboardButton{ Text = "/date"}, new KeyboardButton { Text = "/time" },
                        new KeyboardButton { Text = "/day" } }
                }
            };
        }
    }
}
