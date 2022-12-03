using DronesAPI.Commons.Enums;
using DronesAPI.Data;
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

        public DronesController(DronesContext context)
        {
            _context = context;
        }

        /// <summary>
        /// checking available drones for loading
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drone>>> GetDrones()
        {
            return await _context.Drones.Where(x => x.State.Equals(StateEnum.IDLE.ToString())).ToListAsync();
        }
    }
}
