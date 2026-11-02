using AutoMapper;
using EFCoreMovieCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netCore.Data;
using netCore.DTOs;
using netCore.Models;
using Services;

namespace netCore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DirectorsController : ControllerBase
{
    private readonly IDirectorService _directorService;
    public DirectorsController(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    // GET: api/directors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Director>>> GetAllDirector()
    {
        var result = await _directorService.GetAllDirectorAsync();
        return Ok(result.Data);
    }

    // GET: api/directors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Director>> GetDirectorById(int id)
    {
        var result = await _directorService.GetDirectorByIdAsync(id);

        if (!result.Success)
        {
            return StatusCode(result.StatusCode, result.Message);
        }

        return Ok(result.Data);
    }

    // POST: api/directors
    [HttpPost]
    public async Task<ActionResult<Director>> CreateDirector(CreateDirectorDto newDirectorDto)
    {
        var result = await _directorService.CreateDirectorAsync(newDirectorDto);

        if (!result.Success)
        {
            return StatusCode(result.StatusCode, result.Message);
        }

        // return CreatedAtAction(nameof(GetDirector), new { id = director.Id }, director);
        return CreatedAtAction(nameof(GetDirectorById), new { id = result.Data!.Id }, result.Data);
    }

    // PUT: api/directors/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDirector(int id, UpdateDirectorDto updateDirectorDto)
    {
        var result = await _directorService.UpdateDirectorAsync(id, updateDirectorDto);
        if (!result.Success)
        {
            return StatusCode(result.StatusCode, result.Message);
        }

        return Ok(result.Data);
    }

    // DELETE: api/directors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDirector(int id)
    {
        var result = await _directorService.DeleteDirectorAsync(id);
        if (!result.Success)
        {
            return StatusCode(result.StatusCode, result.Message);
        }

        return Ok(result.Message);
    }

}