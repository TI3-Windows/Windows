using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travelore.Model;

namespace Travelore.Data.Repositories
{
    public class TravelListRepository : ITravelListRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TravelList> _travelLists;

        public TravelListRepository(ApplicationDbContext context)
        {
            _context = context;
            _travelLists = context.TravelLists;
        }

        public void Add(TravelList travelList)
        {
            _travelLists.Add(travelList);
        }

        public void Delete(TravelList travelList)
        {
            _travelLists.Remove(travelList);
        }

        public TravelList GetTravelListId(int id)
        {
            return _travelLists
                .Include(tl => tl.Categories).ThenInclude(c => c.Items)
                .Include(tl => tl.Tasks)
                .SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<TravelList> GetTravelLists()
        {
            return _travelLists
                .Include(tl => tl.Categories).ThenInclude(c => c.Items)
                .Include(tl => tl.Tasks)
                .ToList();        
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TravelList travelList)
        {
            _travelLists.Update(travelList);
        }
    }
}
