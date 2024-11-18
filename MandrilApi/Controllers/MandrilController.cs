using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MandrilApi.Models;
using MandrilApi.Services;

namespace MandrilApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MandrilController : ControllerBase
{
    [HttpGet("all")]
    public ActionResult<IEnumerable<Mandril>> GetMandriles()
    {
        var allMandriles = MandrilDataStore.Current.Mandriles;

        if (allMandriles.Count < 1)
        {
            return NotFound("There are no mandriles to show");
        }

        return Ok(allMandriles);
    }

    [HttpGet("{mandrilId}")]
    public ActionResult<Mandril> GetMandril(int mandrilId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }
        
        return Ok(mandril);
    }

    [HttpPost]
    public ActionResult<Mandril> PostMandril(MandrilInsert mandrilInsert)
    {
        var maxMandrilId = MandrilDataStore.Current.Mandriles.DefaultIfEmpty(new Mandril { Id = 0 }).Max(x => x.Id);
        var newMandril = new Mandril()
        {
            Id = maxMandrilId + 1,
            FirstName = mandrilInsert.FirstName,
            LastName = mandrilInsert.LastName
        };

        MandrilDataStore.Current.Mandriles.Add(newMandril);

        return CreatedAtAction(nameof(GetMandril),
            new {mandrilId = newMandril.Id},
            newMandril
        );
    }

    [HttpPut("{mandrilId}")]
    public ActionResult<Mandril> PutMandril([FromRoute] int mandrilId, [FromBody] MandrilInsert mandrilInsert)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        mandril.FirstName = mandrilInsert.FirstName;
        mandril.LastName = mandrilInsert.LastName;
        
        return Ok($"Mandril with id '{mandrilId}' has been edited");
    }

    [HttpDelete("{mandrilId}")]
    public ActionResult<Mandril> DeleteMandril([FromRoute] int mandrilId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        MandrilDataStore.Current.Mandriles.Remove(mandril);
        
        return Ok($"Mandril with id '{mandrilId}' has been deleted");
        
    }

    [HttpDelete("all")]
    public ActionResult<IEnumerable<Mandril>> DeleteMandriles()
    {
        var allMandriles = MandrilDataStore.Current.Mandriles;

        if (allMandriles.Count < 1)
        {
            return NotFound("There are no mandriles to delete");
        }

        MandrilDataStore.Current.Mandriles.Clear();

        return Ok($"All mandriles have been deleted");
    }
}