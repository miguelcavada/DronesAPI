using AutoMapper;
using DronesAPI.Commons.Enums;
using DronesAPI.Data;
using DronesAPI.Dtos;
using DronesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DronesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DronesController : ControllerBase
    {
        private readonly DronesContext _context;
        private readonly IMapper _mapper;

        public DronesController(DronesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// checking available drones for loading
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drone>>> GetDrones()
        {
            try
            {
                var result = await _context.Drones.Where(x => x.State.Equals(StateEnum.IDLE.ToString())).ToListAsync();
                var dronesDto = _mapper.Map<IEnumerable<Drone>>(result);
                return Ok(dronesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// registering a drone
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [HttpPost("{DroneForCreationDto}")]
        public async Task<ActionResult<DroneDto>> CreateDrone([FromBody] DroneForCreationDto drone)
        {
            try
            {
                if (drone == null)
                {
                    return BadRequest("DroneForCreationDto object is null");
                }
                var droneEntity = _mapper.Map<Drone>(drone);

                await _context.Drones.AddAsync(droneEntity);
                await _context.SaveChangesAsync();

                var droneDto = _mapper.Map<DroneDto>(droneEntity);
                return Ok(droneDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// check drone battery level for a given drone
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [HttpGet("{DroneDto}")]
        public async Task<ActionResult<string>> GetBatteryLevel([FromBody] DroneDto drone)
        {
            try
            {
                var result = await _context.Drones.FirstOrDefaultAsync(x => x.Id.Equals(drone.Id));
                if (result == null)
                {
                    return NotFound();
                }
                var droneDto = _mapper.Map<Drone>(result);
                return Ok(new { batteryLevel = $"The battery level for given drone is {droneDto.BatteryCapacity}%" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// loading a drone with medication items
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [HttpPost("{DroneDto}")]
        public async Task<ActionResult<Drone>> LoadingDroneItems([FromBody] DroneDto drone)
        {
            var currentDate = DateTime.Now;
            try
            {
                var droneEntity = await _context.Drones.FirstOrDefaultAsync(x => x.Id.Equals(drone.Id));

                if (droneEntity != null)
                {
                    if (droneEntity.BatteryCapacity < 25)
                    {
                        return BadRequest("The given drone has its battery level less than 25%");
                    }
                    else
                    {
                        foreach (var item in drone.Medications)
                        {
                            var suma = (droneEntity.DroneItems?.Sum(x => x.WeightMedication) + item.Weight);
                            if (suma > droneEntity.WeightLimit)
                            {
                                break;
                            }
                            await _context.DroneItems.AddAsync(new DroneItem
                            {
                                DroneId = droneEntity.Id,
                                ItemBaseId = item.Id,
                                WeightMedication = item.Weight,
                                CreationDate = currentDate
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
