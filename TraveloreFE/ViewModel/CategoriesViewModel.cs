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
        public ObservableCollection<String> CategoryNames { get; set; }
        public Travellist Travellist { get; set; }
        public CategoriesViewModel(Travellist tl)
        {
            Travellist = tl;
            Categories = new ObservableCollection<Category>();
            CategoryNames = new ObservableCollection<String>();
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
                CategoryNames.Add(c.Name);
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

        // Add A Item WITH Parameters
        public async System.Threading.Tasks.Task AddNewItem(string name, int amount, string catName)
        {
            var item = new Item() { Name = name, Amount = amount, DoneItem = false };
            var categoryId = Categories.Where(c => c.Name.Equals(catName)).FirstOrDefault().Id;
            var itemJson = JsonConvert.SerializeObject(item);

            HttpClient httpClient = new HttpClient();
            var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Category/"+categoryId+"/NewItem"),
                new HttpStringContent(itemJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                Category cat = Categories.Where(c => c.Id == categoryId).FirstOrDefault();
                cat.Items.Add(JsonConvert.DeserializeObject<Item>(res.Content.ToString()));
            }
        }

        public async System.Threading.Tasks.Task AddNewCat(string name)
        {
            var cat = new Category() { Name = name, DoneCat = false, Items = new List<Item>() };
            var catJson = JsonConvert.SerializeObject(cat);
            HttpClient httpClient = new HttpClient();
            var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Category/"),
                new HttpStringContent(catJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                Category newCat = JsonConvert.DeserializeObject<Category>(res.Content.ToString());
                Categories.Add(newCat);
                CategoryNames.Add(newCat.Name);
            }
        }
    }
}
