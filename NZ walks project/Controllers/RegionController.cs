using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Contracts;
using NZWalks.API.Entity_Models.Domain;
using System.Linq;
using System.Threading.Tasks;
using NZ_walks_project.Entity_Models.DTO;
using NZWalks.API.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace NZWalks.API.Controllers
{
    [Route("api/Region")]
    [ApiController]
    [Authorize]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;

        public RegionController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        // Get all regions
        [HttpGet]
        [Route("getRegion")]
        public async Task<IActionResult> GetRegion()
        {
            try
            {   
                //Get region domain model from database
                var regionsDomain = await _regionRepository.GetAllRegionAsync();

                //Map domain models to DTO s
                var regionsDto = new List<RegionDTO>();
                foreach (var regionDomain in regionsDomain)
                {
                    regionsDto.Add(new RegionDTO
                    {
                        Id = regionDomain.Id,
                        Code = regionDomain.Code,
                        Name = regionDomain.Name,
                        RegionImageUrl = regionDomain.RegionImageUrl,
                    });
                }


                if (regionsDto.Any())
                {   
                    //return DTO s
                    return Ok(regionsDto);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return BadRequest();
            }
        }

        // Get region by Id
        [HttpGet]
        [Route("getRegionById/{id}")]
        public async Task<IActionResult> GetRegionById([FromRoute] int id)
        {
            try
            {   
                //Get Data from database - domain model
                var regionDomain = await _regionRepository.GetRegionByIdAsync(id);

                

                if (regionDomain == null)
                {
                    return NotFound();
                }

                //Map domain models to database
                var regionDto = new RegionDTO
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                };

                //return dto back to client
                return Ok(regionDto);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // Add new region
        [HttpPost]
        [Route("AddRegion")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            try
            {   // Map Dto to Domain model
                var regionDomainModel = new Region
                {
                    Code = addRegionRequestDTO.Code,
                    Name = addRegionRequestDTO.Name,
                    RegionImageUrl = addRegionRequestDTO.RegionImageUrl,
                };

                //use domain model to create region
                await _regionRepository.AddRegionAsync(regionDomainModel);
                await _regionRepository.SaveChangesAsync();

                //Map Domain model to Dto
                var regionDto = new RegionDTO
                {  
                    Id = regionDomainModel.Id,  
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl,
                };

                return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // Update existing region
        [HttpPut]
        [Route("UpdateRegion/{id}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] int id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            try
            {
                
                // check if there is an existing region
                var existingRegion = await _regionRepository.UpdateRegionAsync(id);

                if (existingRegion == null)
                {
               
                    return NotFound();
                }

                //map DTO to entity model
                var existinRegionEntityModel = new Region
                {
                    Code = updateRegionRequestDTO.Code,
                    Name = updateRegionRequestDTO.Name,
                    RegionImageUrl = updateRegionRequestDTO.RegionImageUrl,

                };

                //update the region entity model
                existingRegion.Code = existinRegionEntityModel.Code;
                existingRegion.Name = existinRegionEntityModel.Name;
                existingRegion.RegionImageUrl = existinRegionEntityModel.RegionImageUrl;

                _regionRepository.SaveChangesAsync();

                //convert Entity model to DTO
                var regionupdatedDTO = new RegionDTO
                {  
                   Id = existingRegion.Id,
                   Code = existingRegion.Code,
                   Name = existingRegion.Name,
                   RegionImageUrl = existingRegion.RegionImageUrl
                };

                return Ok(regionupdatedDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // Delete a region
        [HttpDelete]
        [Route("DeleteRegion/{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] int id)
        {
            try
            {
                var regionToDelete = await _regionRepository.DeleteRegionAsync(id);
                if (regionToDelete == null)
                {
                    return NotFound();
                }

                //_regionRepository.Remove(regionToDelete);
                await _regionRepository.SaveChangesAsync();

                var regionupdatedDTO = new RegionDTO
                {
                    Id = regionToDelete.Id,
                    Code = regionToDelete.Code,
                    Name = regionToDelete.Name,
                    RegionImageUrl = regionToDelete.RegionImageUrl
                };

             
                return Ok(regionToDelete);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
