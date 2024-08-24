using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    //for routing
    // it points to https//localhost:1234/api/regions
    //since it points to the regions model

    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //this should be added
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get data from the db

            var regionsModel = dbContext.Regions.ToList();

            //map domain to dtos
            var regionsDto = new List<RegionDto>();

            foreach (var region in regionsModel)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Nairobi",
            //        Code = "NBO",
            //        RegionImageUrl = "https://images.pexels.com/photos/27469316/pexels-photo-27469316/free-photo-of-a-group-of-people-hiking-up-a-mountain.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            //    }
            //    }

            //return dtos
            return Ok(regionsDto);

        }

        //Get region by Id endpoint
        [HttpGet]
        [Route("{id:guid}")]

        public IActionResult GetById(Guid id)
        {
            var regionModel = dbContext.Regions.Find(id);

            if (regionModel == null)
            {
                return NotFound();
            }
            else
            {
                var regionDto = new RegionDto()
                {
                    Id = regionModel.Id,
                    Name = regionModel.Name,
                    Code = regionModel.Code,
                    RegionImageUrl = regionModel.RegionImageUrl,
                };
                //here regionModel still gives the client data
                return Ok(regionDto );
            }
            //convert the model to a dto for the end client
        }

        //post to create new region
        [HttpPost]

        public IActionResult CreateRegion([FromBody] PostNewRegion postNewRegion)
        {
            //Convert the Dto to a domain model
            var regionDomainModel = new Region
            {
                Name = postNewRegion.Name,
                Code = postNewRegion.Code,
                RegionImageUrl = postNewRegion.RegionImageUrl
            };

            //use domain model to create a region

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id =  regionDomainModel.Id}, regionDomainModel);

        }


    }
    }

