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
        public async Task<ActionResult<IEnumerable<DroneDto>>> GetDrones()
        {
            var drones = await _context.Drones.Where(x => x.State.Equals(StateEnum.IDLE.ToString())).ToListAsync();
            if (!drones.Any())
            {
                return NoContent();
            }
            var droneDtos = drones.Select(x => _mapper.Map<DroneDto>(x)).ToList();
            return droneDtos;
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
            var droneEntity = await _context.Drones.FirstOrDefaultAsync(x => x.Id.Equals(drone.Id));
            if (droneEntity == null)
            {
                return NotFound();
            }
            return $"The battery level for given drone is {droneEntity.BatteryCapacity}%";
        }

        /// <summary>
        /// loading a drone with medication items
        /// </summary>
        /// <param name="droneDto"></param>
        /// <returns></returns>
        [HttpPut("{droneDto}")]
        public async Task<ActionResult> LoadingDroneItems([FromBody] DroneDto droneDto)
        {
            var currentDate = DateTime.Now;
            try
            {
                var droneEntity = await _context.Drones.FirstOrDefaultAsync(x => x.Id.Equals(droneDto.Id));
                if (droneEntity == null)
                {
                    return NotFound(droneDto);
                }
                droneEntity.State = StateEnum.LOADING.ToString();
                _context.Entry(droneEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                if (droneEntity.BatteryCapacity < 25)
                {
                    return BadRequest("The given drone has its battery level less than 25%");
                }
                else
                {
                    var medicationDtos = new List<MedicationDto>();

                    foreach (var item in droneDto.Medications)
                    {
                        var suma = (medicationDtos.Sum(x => x.Weight) + item.Weight);
                        if (suma > droneEntity.WeightLimit)
                        {
                            break;
                        }
                        medicationDtos.Add(item);
                    }
                    droneEntity.Medications?.AddRange(_mapper.Map<IEnumerable<Medication>>(medicationDtos));
                    droneEntity.State = StateEnum.LOADED.ToString();
                    _context.Entry(droneEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// checking loaded medication items for a given drone
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [HttpGet("{drone}")]
        public async Task<ActionResult<IEnumerable<MedicationDto>>> CheckingLoadedMedications([FromBody] Drone drone)
        {
            var droneEntity = await _context.Drones.FirstOrDefaultAsync(x => x.Id.Equals(drone.Id)
                && x.State.Equals(StateEnum.LOADED.ToString()) || x.State.Equals(StateEnum.DELIVERING.ToString()));
            if (droneEntity == null)
            {
                return NotFound(drone);
            }
            var medications = droneEntity.Medications?.Select(x => _mapper.Map<MedicationDto>(x));
            if (medications == null)
            {
                return NoContent();
            }
            return medications.ToList();
        }
    }
}
