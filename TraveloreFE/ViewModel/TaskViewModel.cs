using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using TraveloreFE.Model;
using Windows.Web.Http;
using HttpClient = Windows.Web.Http.HttpClient;

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

        public async void AddTask(string description, DateTime dateTime)
        {
            try
            {
                string url = "http://localhost:5001/api/Task";
                Task t = new Task(description, false, dateTime);
                var json = JsonConvert.SerializeObject(t);
                var stringContent = new HttpStringContent(json);
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.PostAsync(new Uri(url), stringContent);
                response.EnsureSuccessStatusCode();
                var httpResponseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }
    }
}
