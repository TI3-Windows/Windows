using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public interface ITaskRepository
    {
        IEnumerable<Task> GetTasks();
        Task GetByTaskId(int id);
        void Add(Task task);
        void Update(Task task);
        void Delete(Task task);
        void SaveChanges();
    }
}
