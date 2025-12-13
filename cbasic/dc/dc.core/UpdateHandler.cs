using System;
using Otus.ToDoList.ConsoleBot;
using Otus.ToDoList.ConsoleBot.Types;
using dc.core;
using System.ComponentModel;
using System.Data;

namespace dc.app
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly IUserService _userService;
        private readonly IToDoService _toDoService;

        public UpdateHandler(IUserService userService, IToDoService toDoService)
        {
            _userService = userService;
            _toDoService = toDoService;
        }

        public void HandleUpdateAsync(ITelegramBotClient botClient, Update update)
        {
            string command = update?.Message?.Text;
            int argument = 0;

            if (command == null)
                return;
            if (command.Contains(" "))
            {
                string[] strings = command.Split(" ");
                if (!string.IsNullOrWhiteSpace(strings[1]))
                {
                    if (Int32.TryParse(strings[1], out argument))
                    {
                        command = strings[0];
                    }
                }
            }

            //            string incomingMessage = update.Message.Text;
            var from = update.Message.From;
            long userId = from.Id;

            // Попытаемся найти пользователя через сервис
            var user = _userService.GetUser(userId);

            // Если пользователя нет в системе, зарегистрируем автоматически
            /*            if (user == null)
                        {
                            string telegramUserName = from.Username ?? "Unknown";
                            user = new ToDoUser(telegramUserName, userId);
                            _userService.RegisterUser(user);
                        }*/

            switch (command)
            {
                case "/start":
                    if (user == null)
                    {
                        //регистрируем без запроса имени
                        string telegramUserName = from?.Username ?? "Unknown";
                        user = _userService.RegisterUser(userId, telegramUserName);
                    }

                    Globals.UserName = user.TelegramUserName;
                    MsgUtils.MenuForRegisteredUser();
                    MsgUtils.ShowMenu(Globals.Menu, Globals.UserName);
                    botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                    break;

                case "/help":
                    if (user == null)
                    {
                        MsgUtils.ShowHelp();
                        botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                        break;
                    }
                    MsgUtils.ShowHelpRegistered();
                    botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                    break;
                case "/info":
                        MsgUtils.ShowInfo();
                        botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                        break;
                case "/addtask":
                    if (user == null)
                    {
                        MsgUtils.ErrorCase(Globals.UserName);
                        botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                        break;
                    }
                    try
                    {
                        Console.Write("Введи имя задачи: ");
                        string itemName = Console.ReadLine();
                        _toDoService.Add(user, itemName);
                        var allItems = _toDoService.GetAllByUserId(user.UserId);
                        for (int i = 0; i < allItems.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: {allItems[i].Name} - {allItems[i].State}");
                        }
                        botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                    }
                    
                    break;
                case "/completetask":
                    if (user == null)
                    {
                        MsgUtils.ErrorCase(Globals.UserName);
                        botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                        break;
                    }

                    var activeList = _toDoService.GetAllByUserId(user.UserId);
                    try
                    {
                        _toDoService.MarkCompleted(activeList[argument - 1].Id);
                        Console.WriteLine($"Задача \"{activeList[argument - 1].Name}\" выполнена");
                        for (int i = 0; i < activeList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: {activeList[i].Name} - {activeList[i].State}");
                        }
                        botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw new Exception("Задача не найдена");
                    }
                    break;
                case "/removetask":
                    if (user == null)
                    {
                        MsgUtils.ErrorCase(Globals.UserName);
                        botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                        break;
                    }
                    var newList = _toDoService.GetAllByUserId(user.UserId);
                    var deletedTask = newList[argument - 1].Name;
                    try
                    {
                        _toDoService.Delete(newList[argument - 1].Id);
                        Console.WriteLine($"Задача \"{deletedTask}\" удалена");
                        for (int i = 0; i < newList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: {newList[i].Name} - {newList[i].State}");
                        }
                        botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        throw new Exception("Задача не найдена");
                    }
                    break;
                default:
                    MsgUtils.ErrorCase(Globals.UserName);
                    botClient.SendMessage(update.Message.Chat, MsgUtils.EnterCommand());
                    break;
            }
        }
    }
}