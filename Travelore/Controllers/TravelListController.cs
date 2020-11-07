using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travelore.Model;

namespace Travelore.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TravelListController : ControllerBase
    {
        private readonly ITravelListRepository _travelRepo;
        

        public TravelListController(ITravelListRepository travelListRepository)
        {
            _travelRepo = travelListRepository;
        }

        [HttpGet]
        public IEnumerable<TravelList> GetTravelLists()
        {
            return _travelRepo.GetTravelLists();
        }
    }
}