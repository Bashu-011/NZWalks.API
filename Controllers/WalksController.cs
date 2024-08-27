using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        //create walk, post method
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                //map dto to domain model
                var walkModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkModel);

                //map model to dto
                return Ok(mapper.Map<WalkDto>(walkModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //This is a method to get walks
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {
            var walksDomain = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true);

            //map model to dto

            return Ok(mapper.Map<List<WalkDto>>(walksDomain));
        }

        //get walk by id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkModel =  await walkRepository.GetWalkByIdAsync(id);

            if (walkModel == null)
            {
                return NotFound();
            }
            //map domain model to dto

            return Ok(mapper.Map<WalkDto>(walkModel));
        }

        //update walks
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkDto updateWalkDto)
        {
            //Map Dto to domain model
            var walkModel = mapper.Map<Walk>(updateWalkDto);

            walkModel = await walkRepository.UpdateAsync(id, walkModel);

            if (walkModel == null)
            {
                return NotFound();
            }
            //map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkModel));
        }


        //Delet endpoint
        [HttpDelete]
        [Route ("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var deletedModel = await walkRepository.DeleteAsync(id);

            if(deletedModel == null)
            {
                return NotFound();
            }
            else
            {
                //map domain model to dto
                return Ok(mapper.Map<WalkDto>(deletedModel));
            }

        }
    }
}
