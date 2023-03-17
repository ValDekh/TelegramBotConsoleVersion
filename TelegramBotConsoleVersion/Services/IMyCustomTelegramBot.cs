using Telegram.Bot;

namespace TelegramBotConsoleVersion.Services
{
    public interface IMyCustomTelegramBot
    {
        Task Run();
        TelegramBotClient GetTelegramBotClient();

    }
}