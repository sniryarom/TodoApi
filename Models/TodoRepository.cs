using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;

            if( _context.TodoItems.Count() == 0)
            {
                for (int i = 0; i <= 5; i++) {
                    Add(new TodoItem
                    {
                        Text = "Item "+ (i+1).ToString(),
                        User = "Snir",
                        CreateDate = DateTime.Now.ToShortDateString(),
                        IsPrivate = false,
                        IsComplete = (i % 2 == 0) ? true : false
                    });    
                }
            }      
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        public void Add(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public TodoItem Find(long key)
        {
            return _context.TodoItems.FirstOrDefault(t => t.Key == key);
        }

        public void Remove(long key)
        {
            var entity = _context.TodoItems.First(t => t.Key == key);
            _context.TodoItems.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(TodoItem item)
        {
            _context.TodoItems.Update(item);
            _context.SaveChanges();
        }
    }
}