using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MandrilApi.Models;
using MandrilApi.Services;
using Microsoft.EntityFrameworkCore;
using MandrilApi.Helpers;

namespace MandrilApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MandrilController : ControllerBase
{

    private readonly AppDbContext _context;
    public MandrilController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetMandriles()
    {
        try
        {
            var mandriles = await _context.Mandriles.Include(m => m.Skills).ToListAsync();
            return Ok(mandriles);
        }
        catch (Exception ex)
        {
            return Problem(Messages.Database.ConnectionFailed, ex.Message);
        }
    }

    [HttpGet("{mandrilId}")]
    public async Task<ActionResult<Mandril>> GetMandril(int mandrilId)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(x => x.Id == mandrilId);

        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        return Ok(mandril);
    }

    [HttpPost]
    public async Task<ActionResult<Mandril>> PostMandril(MandrilInsert mandrilInsert)
    {
        var newMandril = new Mandril
        {
            FirstName = mandrilInsert.FirstName,
            LastName = mandrilInsert.LastName
        };

        _context.Mandriles.Add(newMandril);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMandril),
             new { mandrilId = newMandril.Id },
             newMandril);
    }

    [HttpPut("{mandrilId}")]
    public async Task<ActionResult<Mandril>> PutMandril([FromRoute] int mandrilId, [FromBody] MandrilInsert mandrilInsert)
    {
        var mandril = await _context.Mandriles.FirstOrDefaultAsync(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' does not exist");
        }

        mandril.FirstName = mandrilInsert.FirstName;
        mandril.LastName = mandrilInsert.LastName;

        await _context.SaveChangesAsync();

        return Ok($"Mandril with id '{mandrilId}' has been edited");
    }

    [HttpDelete("{mandrilId}")]
    public async Task<ActionResult<Mandril>> DeleteMandril([FromRoute] int mandrilId)
    {
        var mandril = await _context.Mandriles.FirstOrDefaultAsync(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        _context.Mandriles.Remove(mandril);
        await _context.SaveChangesAsync();

        return Ok($"Mandril with id '{mandrilId}' has been deleted");
    }

    [HttpDelete("all")]
    public async Task<ActionResult<IEnumerable<Mandril>>> DeleteMandriles()
    {
        var allMandriles = await _context.Mandriles.ToListAsync();

        if (allMandriles.Count < 1)
        {
            return NotFound("There are no mandriles to delete");
        }

        _context.Mandriles.RemoveRange(allMandriles);
        await _context.SaveChangesAsync();

        return Ok($"All mandriles have been deleted");
    }
}