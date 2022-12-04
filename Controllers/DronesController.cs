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
        /// check drone battery level for a given drone
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [HttpGet("{DroneDto}")]
        public async Task<ActionResult<string>> GetBatteryLevel(DroneDto drone)
        {
            try
            {
                var result = await _context.Drones.FirstOrDefaultAsync(x => x.SerialNumber == drone.SerialNumber);
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
    }
}
