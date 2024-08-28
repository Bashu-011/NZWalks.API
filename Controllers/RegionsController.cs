using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //for routing
    // it points to https//localhost:1234/api/regions
    //since it points to the regions model

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        //this should be added
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from the db

            var regionsModel = await regionRepository.GetAllAsync();

            

            //map domain to dtos
            //var regionsDto = new List<RegionDto>();

            //foreach (var region in regionsModel)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}

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
            
            //map the models to dtos using the mapper

            var regionsDto = mapper.Map<List<RegionDto>>(regionsModel);

            return Ok(regionsDto);

        }

        //Get region by Id endpoint
        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var regionModel = await regionRepository.GetByIdAsync(id);


            if (regionModel == null)
            {
                return NotFound();
            }
            else
            {
                //mapping using automapper
               var regionDto = mapper.Map<RegionDto>(regionModel);
               
                
                return Ok(regionDto);
            }
            //convert the model to a dto for the end client
        }

        //post to create new region
        [HttpPost]

        public async Task<IActionResult> CreateRegion([FromBody] PostNewRegion postNewRegion)
        {
            if (ModelState.IsValid)
            {

                //Convert the Dto to a domain model
                var regionDomainModel = mapper.Map<Region>(postNewRegion);


                //use domain model to create a region

                //await dbContext.Regions.AddAsync(regionDomainModel);
                //await dbContext.SaveChangesAsync();

                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //update region
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> PutById([FromRoute]Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            if (ModelState.IsValid)
            {
                //map dto to domain model
                var region = mapper.Map<Region>(updateRegionDto);

                //using the repository pattern
                region = await regionRepository.UpdateAsync(id, region);

                if (region == null)
                {
                    return NotFound();
                }
                else
                {
                    //map region dto to domain model, basically to the db
                    //region.Code = updateRegionDto.Code;
                    //region.Name = updateRegionDto.Name;
                    //region.RegionImageUrl = updateRegionDto.RegionImageUrl;

                    //await dbContext.SaveChangesAsync();

                    //convert domain model to dto
                    var regionDto = mapper.Map<RegionDto>(region);

                    return Ok(regionDto);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Delete region endpoint
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            //var region = await dbContext.Regions.FindAsync(id);
            var region = await regionRepository.DeleteAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            else
            {
                //Delete if there
                
                return Ok(mapper.Map<RegionDto>(region));

            }
        }
    }
    }

