using PieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PieShop.Controllers.Api
{
    [Route("api/[controller]")] //api/search
    [ApiController]
    //ControllerBase doesn't include support for views
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var allPies = _pieRepository.AllPies;
            return Ok(allPies);
        }

        //We have to specify the parameter otherwise we cannot distinguish from the other methods. A Get without an Id
        //will execute GetAll
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!_pieRepository.AllPies.Any(p => p.PieId == id))
                return NotFound();
            //return new JsonResult(_pieRepository.AllPies.Where(p =>p.PieId == id);
            return Ok(_pieRepository.AllPies.Where(p => p.PieId == id));
        }

        [HttpPost]
        //fromBody indicates that the content to search is found in the body of the query
        public IActionResult SearchPies([FromBody] string searchQuery)
        {
            IEnumerable<Pie> pies = new List<Pie>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                pies = _pieRepository.SearchPies(searchQuery);
            }
            return new JsonResult(pies);
        }


    }
}
