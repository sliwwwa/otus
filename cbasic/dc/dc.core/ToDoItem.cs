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
        public ToDoItemState State { get; set; }
        public DateTime? StateChangedAt { get; set; }

        public ToDoItem(ToDoUser user, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Введено невалидное имя, либо оно не введено.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            CreatedAt = DateTime.UtcNow;
            State = ToDoItemState.Active;
        }
    }
}