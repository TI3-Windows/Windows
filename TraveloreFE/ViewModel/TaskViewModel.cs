using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using TraveloreFE.Model;

namespace TraveloreFE.ViewModel
{
    public class TaskViewModel
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public TaskViewModel()
        {
            Tasks = new ObservableCollection<Task>();
            loadTasks();
        }

        private async void loadTasks()
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(new Uri("http://localhost:5001/api/Task"));
            var taskList = JsonConvert.DeserializeObject<IList<Task>>(json);
            foreach(var t in taskList)
            {
                Tasks.Add(t);
            }
        }
    }
}
