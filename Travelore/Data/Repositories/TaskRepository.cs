using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travelore.Model;
using Task = Travelore.Model.Task;

namespace Travelore.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Task> _tasks;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
            _tasks = context.Tasks;
        }

        public void Add(Task task)
        {
            _tasks.Add(task);
        }

        public void Delete(Task task)
        {
            _tasks.Remove(task);
        }

        public Task GetByTaskId(int id)
        {
            return _tasks.SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Task> GetTasks()
        {
            return _tasks;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Task task)
        {
            _tasks.Update(task);
        }
    }
}
