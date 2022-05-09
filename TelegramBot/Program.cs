using System;
using System.Net;
using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBotHelpers;

namespace TelegramBot
{
    internal class Program
    {
        private static readonly string token = Config.GetToken("Config.json");
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
            if (message.Text != null)
            {
                Console.WriteLine($" Пришло сообщение: {message.Text}");
                var httpClient = new HttpClient();
                var response = "Not Found";
                long chatId = message.Chat.Id;

                switch (message.Text)
                {
                    case "/start":
                        await client.SendTextMessageAsync(message.Chat.Id, "Hello",
                            replyMarkup: Button.GetButtons());
                        var webRequest = WebRequest.Create($"https://localhost:44320/api/Chat/Add?id={chatId}");
                        webRequest.Method = "POST";
                        WebResponse webResponse = await webRequest.GetResponseAsync();
                        webResponse.Close();

                        break;

                    case "/my-id":
                        await client.SendTextMessageAsync(message.Chat.Id, message.Chat.Id.ToString(),                            
                            replyMarkup: Button.GetButtons());                        
                        break;

                    case "/reg-date":
                        HttpResponseMessage getRegistrationDateResponse = await httpClient.GetAsync
                            ($"https://localhost:44320/api/Chat/GetRegistrationDate?chatId={chatId}");

                        if (getRegistrationDateResponse.IsSuccessStatusCode)
                        {
                            response = await getRegistrationDateResponse.Content.ReadAsStringAsync();
                            var dateTime = Date.ConvertToDateTime(response);
                            await client.SendTextMessageAsync(message.Chat.Id, dateTime.ToLongDateString(),
                                replyMarkup: Button.GetButtons());
                        }
                        else
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, response,
                                replyMarkup: Button.GetButtons());
                        }
                        
                        break;

                    case "/date":
                        await client.SendTextMessageAsync(message.Chat.Id, DateTime.Now.ToLongDateString(),
                            replyMarkup: Button.GetButtons());
                        break;

                    case "/time":
                        await client.SendTextMessageAsync(message.Chat.Id, DateTime.Now.ToShortTimeString(),
                            replyMarkup: Button.GetButtons());
                        break;

                    case "/day":
                        var russianDayOfWeek = Date.GetRussianDay();
                        await client.SendTextMessageAsync(message.Chat.Id, russianDayOfWeek,
                            replyMarkup: Button.GetButtons());
                        break;
                }
            }
        }
    }
}
