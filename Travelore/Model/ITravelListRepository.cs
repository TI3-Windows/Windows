using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public interface ITravelListRepository
    {
        IEnumerable<TravelList> GetTravelLists();
        TravelList GetTravelListId(int id);
        void Add(TravelList travelList);
        void Update(TravelList travelList);
        void Delete(TravelList travelList);
        void SaveChanges();
    }
}
