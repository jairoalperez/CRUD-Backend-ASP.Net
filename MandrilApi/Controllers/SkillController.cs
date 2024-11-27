using Microsoft.AspNetCore.Mvc;
using MandrilApi.Models;
using Microsoft.EntityFrameworkCore;
using MandrilApi.Helpers;

namespace MandrilApi.Controllers;

[ApiController]
[Route("mandril/{mandrilId}/[controller]")]
public class SkillController : ControllerBase
{
    private readonly AppDbContext _context;

    public SkillController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Skill>>> GetSkills([FromRoute] int mandrilId)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound(Messages.Mandril.NotFound);
        }

        if (mandril?.Skills?.Count < 1)
        {
            return NotFound(Messages.Skill.NoSkills);
        }

        return Ok(mandril?.Skills);
    }

    [HttpGet("{skillId}")]
    public async Task<ActionResult<Skill>> GetSkill([FromRoute] int mandrilId, [FromRoute] int skillId)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound(Messages.Mandril.NotFound);
        }

        var skill = mandril.Skills?.FirstOrDefault(h => h.Id == skillId);
        if (skill == null)
        {
            return NotFound(Messages.Skill.NotFound);
        }

        return Ok(skill);
    }

    [HttpPost]
    public async Task<ActionResult<Skill>> PostSkill([FromRoute] int mandrilId, [FromBody] SkillInsert skillInsert)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound(Messages.Mandril.NotFound);
        }

        var currentSkill = mandril.Skills?.FirstOrDefault(h => h.Name == skillInsert.Name);

        if (currentSkill != null)
        {
            return BadRequest(Messages.Skill.Repeated);
        }

        var newSkill = new Skill() 
        {
            Name = skillInsert.Name,
            Power = skillInsert.Power,
            MandrilId = mandrilId
        };

        mandril.Skills?.Add(newSkill);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetSkill),
            new {mandrilId = mandrilId, skillId = newSkill.Id},
            new {
                Message = Messages.Skill.Created,
                Skill = newSkill
                }
        );
    }

    [HttpPut("{skillId}")]
    public async Task<ActionResult<Skill>> PutSkill([FromRoute] int mandrilId, [FromRoute] int skillId, [FromBody] SkillInsert skillInsert)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound(Messages.Mandril.NotFound);
        }

        var skill = mandril.Skills?.FirstOrDefault(s => s.Id == skillId);
        if (skill == null)
        {
            return NotFound(Messages.Skill.NotFound);
        }

        var sameSkill = mandril.Skills?.FirstOrDefault(h => h.Id != skillId && h.Name == skillInsert.Name);
        if (sameSkill != null)
        {
            return BadRequest(Messages.Skill.Repeated);
        }

        skill.Name = skillInsert.Name;
        skill.Power = skillInsert.Power;

        await _context.SaveChangesAsync();

        return Ok(Messages.Skill.Edited);
    }

    [HttpDelete("{skillId}")]
    public async Task<ActionResult<Skill>> DeleteSkill([FromRoute] int mandrilId, [FromRoute] int skillId)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound(Messages.Mandril.NotFound);
        }

        var skill = mandril.Skills?.FirstOrDefault(h => h.Id == skillId);
        if (skill == null)
        {
            return NotFound(Messages.Skill.NotFound);
        }

        mandril?.Skills?.Remove(skill);
        await _context.SaveChangesAsync();

        return Ok(Messages.Skill.Deleted); 
    }

    [HttpDelete("all")]
    public async Task<ActionResult<IEnumerable<Skill>>> DeleteSkill([FromRoute] int mandrilId)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound(Messages.Mandril.NotFound);
        }

        mandril.Skills.Clear();
        await _context.SaveChangesAsync();

        return Ok(Messages.Skill.AllDeleted);
    }
}