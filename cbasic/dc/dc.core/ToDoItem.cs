using System;

namespace dc.core
{
    public class ToDoItem
    {
        public enum ToDoItemState { Active, Completed }
        public Guid Id { get; private set; }
        public ToDoUser User { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ToDoItemState State { get; private set; }
        public DateTime? StateChangedAt { get; private set; }

        public ToDoItem(ToDoUser user, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("не дложен быть пустым", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            CreatedAt = DateTime.UtcNow;
            State = ToDoItemState.Active;
        }

        public void SetCompleted()
        {
            State = ToDoItemState.Completed;
        }
    }
}