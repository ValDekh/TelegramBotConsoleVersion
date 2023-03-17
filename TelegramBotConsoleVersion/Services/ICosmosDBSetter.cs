using Microsoft.VisualBasic;
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
        Task Creator();
        Task AddItemsToContainerAsync(Update update);
    }
}
