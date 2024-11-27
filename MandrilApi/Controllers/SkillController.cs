using Microsoft.AspNetCore.Mvc;
using MandrilApi.Models;
using MandrilApi.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        return Ok(mandril.Skills);
    }

    [HttpGet("{skillId}")]
    public async Task<ActionResult<Skill>> GetSkill([FromRoute] int mandrilId, [FromRoute] int skillId)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        var skill = mandril.Skills?.FirstOrDefault(h => h.Id == skillId);
        if (skill == null)
        {
            return NotFound($"Skill with Id '{skillId}' does not exist on mandril with Id '{mandrilId}'");
        }

        return Ok(skill);
    }

    [HttpPost]
    public async Task<ActionResult<Skill>> PostSkill([FromRoute] int mandrilId, [FromBody] SkillInsert skillInsert)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        var currentSkill = mandril.Skills?.FirstOrDefault(h => h.Name == skillInsert.Name);

        if (currentSkill != null)
        {
            return BadRequest("There is already a skill with the same name");
        }

        var newSkill = new Skill() 
        {
            Name = skillInsert.Name,
            Power = skillInsert.Power,
            MandrilId = mandrilId
        };

        mandril.Skills?.Add(newSkill);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSkill),
            new {mandrilId = mandrilId, skillId = newSkill.Id},
            newSkill
        );
    }

    [HttpPut("{skillId}")]
    public async Task<ActionResult<Skill>> PutSkill([FromRoute] int mandrilId, [FromRoute] int skillId, [FromBody] SkillInsert skillInsert)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        var skill = mandril.Skills?.FirstOrDefault(s => s.Id == skillId);
        if (skill == null)
        {
            return NotFound($"Skill with Id '{skillId}' does not exist on mandril with Id '{mandrilId}'");
        }

        var sameSkill = mandril.Skills?.FirstOrDefault(h => h.Id != skillId && h.Name == skillInsert.Name);
        if (sameSkill != null)
        {
            return BadRequest("There is already a skill with the same name");
        }

        skill.Name = skillInsert.Name;
        skill.Power = skillInsert.Power;

        await _context.SaveChangesAsync();

        return Ok($"Skill with Id '{skillId}' has been edited on mandril with Id '{mandrilId}'");
    }

    [HttpDelete("{skillId}")]
    public async Task<ActionResult<Skill>> DeleteSkill([FromRoute] int mandrilId, [FromRoute] int skillId)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        var skill = mandril.Skills?.FirstOrDefault(h => h.Id == skillId);
        if (skill == null)
        {
            return NotFound($"Skill with Id '{skillId}' does not exist on mandril with Id '{mandrilId}'");
        }

        mandril?.Skills?.Remove(skill);
        await _context.SaveChangesAsync();

        return Ok($"Skill with id '{skillId}' has been deleted on mandril with Id '{mandrilId}'"); 
    }

    [HttpDelete("all")]
    public async Task<ActionResult<IEnumerable<Skill>>> DeleteSkill([FromRoute] int mandrilId)
    {
        var mandril = await _context.Mandriles.Include(m => m.Skills).FirstOrDefaultAsync(m => m.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        mandril.Skills.Clear();
        await _context.SaveChangesAsync();

        return Ok($"All skills have been deleted on mandril with Id {mandrilId}");
    }
}