using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mangas.Domain.Entities; // Asegúrate de que la clase Manga esté en el namespace correcto
using mangas.Services.Features.Mangas; // Si tu MangaService está en este namespace
namespace mangas.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class MangaController : ControllerBase
{
    private readonly MangaService _mangaService;

    public MangaController(MangaService mangaService)
    {
        _mangaService = mangaService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_mangaService.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var manga = _mangaService.GetById(id);
        if (manga == null)
            return NotFound();
        return Ok(manga);
    }

    [HttpPost]
    public IActionResult Add([FromBody] Manga manga)
    {
        _mangaService.Add(manga);
        return CreatedAtAction(nameof(GetById), new { id = manga.Id }, manga);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Manga manga)
    {
        if (id != manga.Id)
            return BadRequest();
        
        var existingManga = _mangaService.GetById(id);
        if (existingManga == null)
            return NotFound();

        _mangaService.Update(manga);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var existingManga = _mangaService.GetById(id);
        if (existingManga == null)
            return NotFound();

        _mangaService.Delete(id);
        return NoContent();
    }
}
