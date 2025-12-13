using System;
using Otus.ToDoList.ConsoleBot;
using static dc.core.ToDoItem;

namespace dc.core
{
    public class ToDoService : IToDoService
    {
        private readonly Dictionary<Guid, List<ToDoItem>> _items = new();

        public IReadOnlyList<ToDoItem> GetAllByUserId(Guid userId)
        {
            var userItems = _items.FirstOrDefault(x => x.Key == userId);
            if (userItems.Value == null)
            {
                throw new Exception($"Для пользователя с userId: {userId} задачи не найдены");
            }
            return userItems.Value;
        }

        public IReadOnlyList<ToDoItem> GetActiveByUserId(Guid userId)
        {
            var userItems = _items.FirstOrDefault(x => x.Key == userId);
            var activeItems = userItems.Value?.Where(x => x.State == ToDoItemState.Active);

            if (activeItems == null && activeItems?.Count() == 0)
            {
                throw new Exception($"Для пользователя с userId: {userId} активные задачи не найдены");
            }
            return (IReadOnlyList<ToDoItem>)activeItems;
        }

        public ToDoItem Add(ToDoUser user, string name)
        {
            if (name == null)
            {
                throw new Exception("Имя задачи не введено");
            }
            if (_items.Where(x => x.Key == user.UserId).Count() >= Globals.MaxTaskNum)
                throw new Exception("Привышен лимит количества задач");

            var item = new ToDoItem(user, name);
            var userItems = _items.FirstOrDefault(x => x.Key == user.UserId);
            if (userItems.Value != null)
            {
                userItems.Value.Add(item);
                return item;
            }
            var newUserList = new List<ToDoItem>();
            newUserList.Add(item);
            _items.Add(user.UserId, newUserList);

            return item;
        }

        public void MarkCompleted(Guid id)
        {
            var userItems = _items.FirstOrDefault(x => x.Value.FirstOrDefault(y => id == y.Id) != null);
            userItems.Value.FirstOrDefault(x => x.Id == id)?.SetCompleted(); //то же самое (на всякий)
        }

        public void Delete(Guid id)
        {
            var userItems = _items.FirstOrDefault(x => x.Value.FirstOrDefault(y => id == y.Id) != null);
            userItems.Value.Remove(userItems.Value.FirstOrDefault(x => x.Id == id)); //проверить userItems и userItems.Value.FirstOrDefault(x => x.Id == id на null
        }
    }
}