﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBotConsoleVersion.Services
{
    public interface ICosmosDBSetter
    {
        async Task Creator() { }
        async Task AddItemsToContainerAsync(Update update) { }
        private static async Task CreateDbAsync() { }
        private static async Task CreateContainerAsync() { }
    }
}
