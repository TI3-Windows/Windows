using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TraveloreFE.Model;
using TraveloreFE.ViewModel.Commands;
using Windows.Web.Http;
using HttpClient = Windows.Web.Http.HttpClient;
using Task = TraveloreFE.Model.Task;

namespace TraveloreFE.ViewModel
{
    public class TaskViewModel
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public Travellist Travellist { get; set; }

        //public ICommand AddTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }

        public TaskViewModel(Travellist tl)
        {
            Travellist = tl;
            Tasks = new ObservableCollection<Task>();
            loadTasks();
            //AddTaskCommand = new AddTaskCommand(this);
            DeleteTaskCommand = new DeleteTaskCommand(this);
        }

        private void loadTasks()
        {
            var taskList = Travellist.Tasks;
            foreach(var t in taskList)
            {
                Tasks.Add(t);
            }
        }

        // Add A Task WITH Parameters
        public async System.Threading.Tasks.Task AddNewTask(string description, DateTime? dateTime)
        {
            var task = new Task() { Description = description , DoneTask = false, EndDate = dateTime };
            var taskJson = JsonConvert.SerializeObject(task);

            HttpClient httpClient = new HttpClient();
            var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Task"),
                new HttpStringContent(taskJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                Tasks.Add(JsonConvert.DeserializeObject<Task>(res.Content.ToString()));
            }
        }

        // Delete A Task
        public async System.Threading.Tasks.Task DeleteSelectedTask(int id)
        {
            var taskIdJson = JsonConvert.SerializeObject(id);
            HttpClient httpClient = new HttpClient();
            var url = $"http://localhost:5001/api/Task/{id}";
            var res = await httpClient.DeleteAsync(new Uri(url));
            if(res.IsSuccessStatusCode)
            {
                var deletedTask = Tasks.SingleOrDefault((t) => t.Id == id);
                if(deletedTask != null)
                {
                    Tasks.Remove(deletedTask);
                }
            }
        }

        public async System.Threading.Tasks.Task UpdateSelectedTask(int id)
        {
            var taskIdJson = JsonConvert.SerializeObject(id);
            HttpClient httpClient = new HttpClient();
            var url = $"http://localhost:5001/api/Task/{id}";
            var res = await httpClient.PutAsync(new Uri(url),new HttpStringContent(taskIdJson, Windows.Storage.Streams.UnicodeEncoding.Utf8,"application/json"));
            if(res.IsSuccessStatusCode)
            {
                Task task = Tasks.FirstOrDefault(t => t.Id == id);
                task.DoneTask = !task.DoneTask;
            }
        }
    }
}
