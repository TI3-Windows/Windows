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
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetTasks()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Task task)
        {
            throw new NotImplementedException();
        }
    }
}
