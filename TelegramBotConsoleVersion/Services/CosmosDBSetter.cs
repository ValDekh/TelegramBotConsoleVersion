﻿using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Container = Microsoft.Azure.Cosmos.Container;

namespace TelegramBotConsoleVersion.Services
{
    public class CosmosDBSetter : ICosmosDBSetter
    {
        private static CosmosClient _client;
        private static Database _database;
        private static Container _container;
        private static IConfiguration _appConfig;
        public CosmosDBSetter(IConfiguration appConfig)
        {
            _appConfig = appConfig;
        }

        private static void ClientInitializer()
        {
            _client = new CosmosClient($"{_appConfig["AppConfig:COSMOS_ENDPOINT"]}",
                $"{_appConfig["AppConfig:COSMOS_KEY"]}");
        }

        private static async Task CreateDbAsync()
        {
            Database TelegrameMessageDb = await _client.CreateDatabaseIfNotExistsAsync("telegramMessageDb");
            _database = TelegrameMessageDb;

        }

        private static async Task CreateContainerAsync()
        {
            _container = await _database.CreateContainerIfNotExistsAsync("receivedMessages", "/MessageContainer_id");
        }

        public static async Task Creator()
        {
            ClientInitializer();
            await CreateDbAsync();
            await CreateContainerAsync();
        }


        public static async Task AddItemsToContainerAsync(Update update)
        {
            MessageInfo item = new MessageInfo
            {
                Id = Guid.NewGuid().ToString(),
                MessageContainer_id = Guid.NewGuid().ToString(),
                Message_id = update?.Message.MessageId,
                Message_text = update.Message.Text,
                UserId = update.Message.From.Id,
                Username = update.Message.From.Username,
            };

            try
            {
                MessageInfo createdItem = await _container.CreateItemAsync<MessageInfo>(
                                item: item,
                                partitionKey: new PartitionKey($"{item.MessageContainer_id}"));
            }
            catch (Exception)
            {
                Console.WriteLine("Bad connection");
            }


        }
    }
}
