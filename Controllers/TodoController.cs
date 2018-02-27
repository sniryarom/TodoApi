using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using Microsoft.AspNetCore.Cors;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET api/todo
        [HttpGet]
        public IEnumerable<TodoItem> GetAll([FromQuery]int delay)
        {
            handleDelay(delay);

            return _todoRepository.GetAll();
        }

        // GET api/todo/5
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id, [FromQuery]int delay)
        {
            handleDelay(delay);

            var item = _todoRepository.Find(id);
            if (item == null)
            {       
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/todo
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item, [FromQuery]int delay)
        {
            handleDelay(delay);

            if (item == null)
            {
                return BadRequest();
            }

            _todoRepository.Add(item);

            return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
        }

        // PUT api/todo/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item, [FromQuery]int delay)
        {
            handleDelay(delay);

            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = _todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Text = item.Text;
            todo.User = item.User;
            todo.IsPrivate = item.IsPrivate;

            _todoRepository.Update(todo);
            return new NoContentResult();
        }

        // DELETE api/todo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id, [FromQuery]int delay)
        {
            handleDelay(delay);
            var todo = _todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _todoRepository.Remove(id);
            return new NoContentResult();
        }

        private void handleDelay(int delay)
        {
            if (delay < 0)
                delay = 0;
            else if (delay > 5000)
                delay = 5000;

            System.Threading.Thread.Sleep(delay);
        }
    }
}