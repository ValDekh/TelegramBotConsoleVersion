using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace TelegramBotConsoleVersion.Services
{
    public class MyCustomTelegramBot : IMyCustomTelegramBot
    {
        private static IConfiguration? AppConfig { get; set; }
        private static ICosmosDBSetter? CosmosDBSetter { get; set; }


        public MyCustomTelegramBot(IConfiguration configuration, ICosmosDBSetter dbSetter)
        {
            AppConfig = configuration;
            CosmosDBSetter = dbSetter;
        }

        public async Task Run()
        {

            await CosmosDBSetter?.Creator();
            TelegramBotClient telegramClient = GetTelegramBotClient();
            telegramClient.StartReceiving(Update, Error);


        }

        private async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update?.Message?.Text != null)
            {
                await CosmosDBSetter.AddItemsToContainerAsync(update);
                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat,
                    text: $"Answer : {update.Message.Text}");
            }
        }

        private static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public TelegramBotClient GetTelegramBotClient()
        {
            var token = "6058383219:AAH8O4pcNxHzQ6jG9HntuYJ_U3kU58WE5IE";
            // var token = AppConfig["AppConfig:Token"];
            if (token is null)
            {
                throw new ArgumentException("Can't get a token");
            }
            var telegramClient = new TelegramBotClient(token);
            return telegramClient;
        }
    }


}

