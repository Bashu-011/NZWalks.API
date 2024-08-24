using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    //for routing
    // it points to https//localhost:1234/api/regions
    //since it points to the regions model

    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //this should be added
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Nairobi",
                    Code = "NBO",
                    RegionImageUrl = "https://images.pexels.com/photos/27469316/pexels-photo-27469316/free-photo-of-a-group-of-people-hiking-up-a-mountain.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Kisumu",
                    Code = "KSM",
                    RegionImageUrl = "https://images.pexels.com/photos/18734944/pexels-photo-18734944/free-photo-of-lone-walk.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Mombasa",
                    Code = "MSA",
                    RegionImageUrl = "https://images.pexels.com/photos/5550026/pexels-photo-5550026.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }

               

            };
            return Ok(regions);
        }
    }
}
