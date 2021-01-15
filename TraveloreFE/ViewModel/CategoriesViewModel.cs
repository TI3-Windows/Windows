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
            if (categoryList != null)
            {
                foreach (var c in categoryList)
                {
                    List<Item> orderdItems = c.Items.OrderBy(i => i.Name).ToList();
                    c.Items = orderdItems;
                    Categories.Add(c);
                    CategoryNames.Add(c.Name);
                }
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

        public async System.Threading.Tasks.Task UpdateItem(string itemName)
        {
            var itemId = 0;
            Item item = null;
            foreach (Category cat in Categories)
            {
                if (cat.Items.FirstOrDefault(i => i.Name.Equals(itemName)) != null)
                {
                    item = cat.Items.FirstOrDefault(i => i.Name.Equals(itemName));
                    itemId = item.Id;
                }

            }
            var itemIdJson = JsonConvert.SerializeObject(itemId);
            HttpClient httpClient = new HttpClient();
            var url = $"http://localhost:5001/api/Category/UpdateItem/{itemId}";
            var res = await httpClient.PutAsync(new Uri(url), new HttpStringContent(itemIdJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                item.DoneItem = !item.DoneItem;
            }
        }

        // Add A Item WITH Parameters
        public async System.Threading.Tasks.Task AddNewItem(string name, int amount, string catName)
        {
            var item = new Item() { Name = name, Amount = amount, DoneItem = false };
            var categoryId = Categories.Where(c => c.Name.Equals(catName)).FirstOrDefault().Id;
            var itemJson = JsonConvert.SerializeObject(item);

            HttpClient httpClient = new HttpClient();
            var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Category/" + categoryId + "/NewItem"),
                new HttpStringContent(itemJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                Category cat = Categories.Where(c => c.Id == categoryId).FirstOrDefault();
                cat.Items.Add(JsonConvert.DeserializeObject<Item>(res.Content.ToString()));
                List<Item> orderdItems = cat.Items.OrderBy(i => i.Name).ToList();
                cat.Items = orderdItems;
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

        public async System.Threading.Tasks.Task RemoveCat(Category cat)
        {
            var catIdJson = JsonConvert.SerializeObject(cat.Id);
            HttpClient httpClient = new HttpClient();
            var url = $"http://localhost:5001/api/Category/{cat.Id}";
            var res = await httpClient.DeleteAsync(new Uri(url));
            if (res.IsSuccessStatusCode)
            {
                var deletedCat = Categories.SingleOrDefault((t) => t.Id == cat.Id);
                if (deletedCat != null)
                {
                    Categories.Remove(deletedCat);
                    Travellist.Categories.Remove(deletedCat);
                }
            }
        }

        public async System.Threading.Tasks.Task RemoveItem(Item item)
        {
            var itemIdJson = JsonConvert.SerializeObject(item.Id);
            HttpClient httpClient = new HttpClient();
            var url = $"http://localhost:5001/api/Category/DeleteItem/{item.Id}";
            var res = await httpClient.DeleteAsync(new Uri(url));
            if (res.IsSuccessStatusCode)
            {
                Category cat = null;
                foreach (Category c in Categories)
                {
                    if (c.Items.Contains(item))
                    {
                        cat = c;
                    }
                }
                if (cat != null)
                {
                    cat.Items.Remove(item);
                    Category tCat = Travellist.Categories.SingleOrDefault(c => c.Id == cat.Id);
                    tCat.Items.Remove(item);
                }
            }
        }
    }
}
