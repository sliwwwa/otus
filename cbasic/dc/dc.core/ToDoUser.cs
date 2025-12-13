using System;
using Otus.ToDoList.ConsoleBot;

namespace dc.core
{
    public class ToDoUser
    {
        public Guid UserId { get; private set; }
        public string TelegramUserName { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public long TelegramUserId { get; private set; }

        public ToDoUser(string telegramUserName, long telegramUserId)
        {
//            if (string.IsNullOrWhiteSpace(telegramUserName))
//                throw new ArgumentException("Ваш username Телеграма не дложен быть пустым", nameof(telegramUserName));
            UserId = Guid.NewGuid();
            TelegramUserName = telegramUserName;
            RegisteredAt = DateTime.UtcNow;
            TelegramUserId = telegramUserId;
        }
    }
}
