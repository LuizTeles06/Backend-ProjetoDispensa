using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dispensa.Data;
using DispensaInteligente;
using Dispensa.models.dto;
namespace Dispensa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly DispensaContext _context;

        public SensorController(DispensaContext context)
        {
            _context = context;
        }

        // GET: api/Sensor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensor()
        {
            return await _context.Sensor.ToListAsync();
        }

        // PUT: api/Sensor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensor(int id, Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return BadRequest();
            }

            _context.Entry(sensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sensor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sensor>> PostSensor(CriarSensorDTO criarSensorDTO)
{
    var sensor = new Sensor
    {
        Produto = criarSensorDTO.Produto,
        Status = criarSensorDTO.Status,
        
    };

    _context.Sensor.Add(sensor);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetSensor", new { id = sensor.Id }, sensor);
}


        // DELETE: api/Sensor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _context.Sensor.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensor.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorExists(int id)
        {
            return _context.Sensor.Any(e => e.Id == id);
        }
    }
}
