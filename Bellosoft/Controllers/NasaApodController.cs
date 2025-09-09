using Bellosoft.Interfaces;
using Bellosoft.Models;
using Bellosoft.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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
    }
}

