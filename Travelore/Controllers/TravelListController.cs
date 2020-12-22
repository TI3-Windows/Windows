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
        public readonly ICustomerRepository _customerRepo;


        public TravelListController(
            ITravelListRepository travelListRepository,
            ICustomerRepository customerRepository
            )
        {
            _travelRepo = travelListRepository;
            _customerRepo = customerRepository;
        }

        [HttpGet]
        public IEnumerable<TravelList> GetTravelLists()
        {
            return _travelRepo.GetTravelLists();
        }

        [HttpGet("{id}")]
        public ActionResult<TravelList> GetTravelList(int id)
        {
            return _travelRepo.GetTravelListId(id);
        }

        [HttpGet("{tlId}/{id}")]
        public ActionResult<Destination> GetDestination(int tlId, int id)
        {
            return _travelRepo.GetDestinationtId(tlId, id);
        }

        [HttpGet("User")]
        public IEnumerable<TravelList> GetTravelListsByUser()
        {
            Customer c = _customerRepo.GetBy(User.Identity.Name);
            return c.TravelLists;
        }

        [HttpPost]
        public ActionResult NewTravellist(TravelList t)
        {
            TravelList travellist = new TravelList()
            {
                Name = t.Name,
                Country = t.Country,
                Street = t.Street,
                HouseNr = t.HouseNr,
                DateLeave = t.DateLeave,
                DateBack = t.DateBack
            };
            _travelRepo.Add(travellist);
            _travelRepo.SaveChanges();
            return CreatedAtAction(nameof(GetTravelList), new { id = travellist.Id }, travellist);
        }

        [HttpPost("Destination/{id}")]
        public ActionResult NewDestination(int id, Destination destination)
        {
            var travellist = _travelRepo.GetTravelListId(id);
            if (travellist == null)
            {
                return NotFound();
            }
            travellist.AddDestination(destination);
            _travelRepo.SaveChanges();
            return CreatedAtAction(nameof(GetDestination), new { tlId = travellist.Id, id = destination.Id }, destination);
        }
    }
}