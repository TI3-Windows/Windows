using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TraveloreFE.Model;
using Windows.Web.Http;
using HttpClient = Windows.Web.Http.HttpClient;

namespace TraveloreFE.ViewModel
{
    public class CategoriesViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public Travellist Travellist { get; set; }
        public CategoriesViewModel(Travellist tl)
        {
            Travellist = tl;
            Categories = new ObservableCollection<Category>();
            loadCategories();
        }

        private void loadCategories()
        {
            //HttpClient httpClient = new HttpClient();
            //var json = await httpClient.GetStringAsync(new Uri("http://localhost:5001/api/Category"));
            //var categoryList = JsonConvert.DeserializeObject<IList<Category>>(json);


            var categoryList = Travellist.Categories;

            foreach (var c in categoryList)
            {
                Categories.Add(c);
            }
        }

        public async System.Threading.Tasks.Task UpdateSelectedCategory(int id)
        {
            var CategoryIdJson = JsonConvert.SerializeObject(id);
            HttpClient httpClient = new HttpClient();
            var url = $"http://localhost:5001/api/Category/{id}";
            var res = await httpClient.PutAsync(new Uri(url), new HttpStringContent(CategoryIdJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                Category category = Categories.FirstOrDefault(t => t.Id == id);
                //task.DoneTask = !task.DoneTask;
            }
        }
    }
}
