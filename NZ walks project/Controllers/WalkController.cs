using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Contracts;
using NZWalks.API.Entity_Models.Domain;

namespace NZ_walks_project.Controllers
{
    [Route("api/walk")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;

        public WalkController(IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;
        }

        [HttpPost] 
        public async Task<IActionResult> CreateWalk([FromBody] Walk walk)
        {
            try
            {
                var newWalk = new Walk
                {
                    Name = walk.Name,
                    Description = walk.Description,
                    LengthInKm = walk.LengthInKm,
                    WalkImageUrl = walk.WalkImageUrl,
                    DifficultyId = walk.DifficultyId,
                    RegionId = walk.RegionId,
                };

                await _walkRepository.CreateAsync(newWalk);
                await _walkRepository.SaveChangesAsync();
                return Ok(newWalk);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]

        public async Task<IActionResult> GetAllWalks()
        {
            try
            {
                var walkEntityModel = await _walkRepository.GetAllAsync();

                if(walkEntityModel == null)
                {
                    return null;
                }

                return Ok(walkEntityModel);
            }
            catch(Exception ex) { 
            
                return BadRequest(ex.Message);
            
            }
            
        }

       [HttpGet]
       [Route("walkById/{id}")]

        public async Task<IActionResult> GetWalkById(int id)
        {
            try
            {
                var selectedWalk  =  await _walkRepository.GetByIdAsync(id);
                if(selectedWalk == null)
                {
                    return NotFound();
                }
                return Ok(selectedWalk);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateWalk/{id}")]

        public async Task<IActionResult> UpdateWalk([FromRoute] int id, [FromBody]Walk walk)
        {
            try
            {
                var existingWalk = await _walkRepository.UpdateWalkAsync(id, walk);
                if(existingWalk == null)
                {
                    return null;
                }

                return Ok(existingWalk);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteWalk/{id}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] int id)
        {
            try{
                var deletedwalk = await _walkRepository.DeleteWalkAsync(id);
                if (deletedwalk == null)
                {
                    return NotFound();
                }
                return Ok(deletedwalk);
            }catch(Exception ex) { 
                return BadRequest(ex.Message);
            }
        }


    }
}
