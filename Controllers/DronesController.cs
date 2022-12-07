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
        public async Task<ActionResult> GetDrones()
        {
            var drones = await _context.Drones.Where(x => x.State.Equals(StateEnum.IDLE.ToString())).ToListAsync();
            if (!drones.Any())
            {
                return NoContent();
            }
            var droneDtos = drones.Select(x => _mapper.Map<DroneDto>(x)).ToList();
            return Ok(droneDtos);
        }

        /// <summary>
        /// registering a drone
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateDrone([FromBody] DroneForCreationDto drone)
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
        /// <param name="droneId"></param>
        /// <returns></returns>
        [HttpGet("BatteryLevel/{droneId}", Name = "BatteryLevel")]
        public async Task<ActionResult> GetBatteryLevel(Guid droneId)
        {
            var droneEntity = await _context.Drones.FirstOrDefaultAsync(x => x.Id.Equals(droneId));
            if (droneEntity == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            return Ok($"The battery level for given drone is {droneEntity.BatteryCapacity}%");
        }

        /// <summary>
        /// loading a drone with medication items
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="medicationDto"></param>
        /// <returns></returns>
        [HttpPut("{droneId}")]
        public async Task<ActionResult> LoadingDroneItems(Guid droneId, [FromBody] IEnumerable<MedicationDto> medicationDto)
        {
            var currentDate = DateTime.Now;
            try
            {
                var droneEntity = await _context.Drones.FirstOrDefaultAsync(x => x.Id.Equals(droneId));
                if (droneEntity == null)
                {
                    return NotFound();
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
                    var medications = new List<Medication>();

                    foreach (var item in _mapper.Map<IEnumerable<Medication>>(medicationDto))
                    {
                        var suma = (medications.Sum(x => x.Weight) + item.Weight);
                        if (suma > droneEntity.WeightLimit)
                        {
                            break;
                        }
                        medications.Add(item);
                    }
                    droneEntity.Items = new List<ItemBase>(medications);
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
        /// <param name="droneId"></param>
        /// <returns></returns>
        [HttpGet("LoadedMedicationsDrone/{droneId}", Name = "LoadedMedicationsDrone")]
        public async Task<ActionResult> GetLoadedMedicationsDrone(Guid droneId)
        {
            var droneEntity = await _context.Drones.FirstOrDefaultAsync(x => x.Id.Equals(droneId) 
                && x.State.Equals(StateEnum.LOADED.ToString()));
            if (droneEntity == null)
            {
                return NotFound();
            }
            var medications = droneEntity.Items?.OfType<Medication>().Select(x => _mapper.Map<MedicationDto>(x));
            if (medications == null)
            {
                return NoContent();
            }
            return Ok(medications.ToList());
        }
    }
}
