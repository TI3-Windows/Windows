using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TraveloreFE.Model;

namespace TraveloreFE.ViewModel
{
    public class CategoriesViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public CategoriesViewModel()
        {
            Categories = new ObservableCollection<Category>();
            loadCategories();
        }

        private async void loadCategories()
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(new Uri("http://localhost:5001/api/Category"));
            var categoryList = JsonConvert.DeserializeObject<IList<Category>>(json);
            foreach (var c in categoryList)
            {
                Categories.Add(c);
            }
        }
    }
}
