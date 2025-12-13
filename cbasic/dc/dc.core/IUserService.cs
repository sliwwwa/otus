using System;
using Otus.ToDoList.ConsoleBot;

namespace dc.core
{
    public interface IUserService
    {
        ToDoUser RegisterUser(long telegramUserId, string telegramUserName);
        ToDoUser? GetUser(long telegramUserId);
    }
}