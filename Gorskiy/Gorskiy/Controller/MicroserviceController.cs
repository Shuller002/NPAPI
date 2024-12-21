using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Gorskiy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MicroserviceController : ControllerBase
    {
        // Временное хранилище задач
        private static readonly List<TodoItem> TodoItems = new();

        // Получить все задачи
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return Ok(TodoItems);
        }

        // Получить задачу по ID
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var item = TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // Создать новую задачу
        [HttpPost]
        public ActionResult Create([FromBody] TodoItem newItem)
        {
            newItem.Id = TodoItems.Count > 0 ? TodoItems.Max(t => t.Id) + 1 : 1;
            TodoItems.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        // Обновить задачу
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] TodoItem updatedItem)
        {
            var existingItem = TodoItems.FirstOrDefault(t => t.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updatedItem.Name;
            existingItem.Description = updatedItem.Description;
            return NoContent();
        }

        // Удалить задачу
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            TodoItems.Remove(item);
            return NoContent();
        }
    }

    // Модель задачи
    public class TodoItem
    {
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
