using System;

namespace dc.core
{
    public class ToDoUser
    {
        public Guid UserId { get; private set; }
        public string TelegramUserName { get; private set; }
        public DateTime RegisteredAt { get; private set; }

        public ToDoUser(string telegramUserName)
        {
            if (string.IsNullOrWhiteSpace(telegramUserName))
                throw new ArgumentException("Ваш username Телеграма не дложен быть пустым", nameof(telegramUserName));

            UserId = Guid.NewGuid();
            TelegramUserName = telegramUserName;
            RegisteredAt = DateTime.UtcNow;
        }
    }
}