using Bellosoft.Interfaces;
using Bellosoft.Models;
using Bellosoft.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace Bellosoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NasaApodController : ControllerBase
    {
        private readonly INasaApod _nasa;
        private readonly Context _db;

        public NasaApodController(INasaApod nasa, Context db)
        {
            _nasa = nasa;
            _db = db;
        }

        [HttpPost("today")]
        public async Task<ActionResult<NasaApod>> CreateToday(CancellationToken ct)
        {
            var today = DateTime.UtcNow.Date;

            var exists = await _db.NasaApods.AsNoTracking().AnyAsync(x => x.Date == today, ct);
            if (exists) return BadRequest(new { error = "APOD de hoje já está salvo." });

            NasaApod? apod;
            try
            {
                apod = await _nasa.GetTodayAsync(ct);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, new { error = "Falha ao chamar a API.", detail = ex.Message });
            }

            if (apod is null) return BadRequest(new { error = "Resposta vazia da NASA." });
           
            var entity = new NasaApod
            {
                Date = apod.Date,
                Title = apod.Title,
                Url = apod.Url
            };

            _db.NasaApods.Add(entity);
            await _db.SaveChangesAsync(ct);

            return Ok(entity);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<NasaApod>>> GetAll(CancellationToken ct)
        {
            var list = await _db.NasaApods
                .AsNoTracking()
                .ToListAsync(ct);

            return Ok(list);
        }

        [HttpGet("date")]
        public async Task<ActionResult<NasaApod>> GetByDate([FromQuery] string date, CancellationToken ct)
        {
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                        DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                                        out var targetDate))
            {
                return BadRequest(new { error = "Formato de data inválido. Use yyyy-MM-dd." });
            }

            var item = await _db.NasaApods
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Date.Date == targetDate.Date, ct);

            if (item is null) return NotFound(new { error = "Nenhum APOD encontrado para a data informada." });

            return Ok(item);
        }
    }
}

