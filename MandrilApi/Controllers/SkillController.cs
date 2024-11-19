using Microsoft.AspNetCore.Mvc;
using MandrilApi.Models;
using MandrilApi.Services;

namespace MandrilApi.Controllers;

[ApiController]
[Route("mandril/{mandrilId}/[controller]")]
public class SkillController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Skill>> GetSkills([FromRoute] int mandrilId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        return Ok(mandril.Skills);
    }

    [HttpGet("{skillId}")]
    public ActionResult<Skill> GetSkill([FromRoute] int mandrilId, [FromRoute] int skillId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
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
    public ActionResult<Skill> PostSkill([FromRoute] int mandrilId, [FromBody] SkillInsert skillInsert)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        var currentSkill = mandril.Skills?.FirstOrDefault(h => h.Name == skillInsert.Name);

        if (currentSkill != null)
        {
            return BadRequest("There is already a skill with the same name");
        }

        #pragma warning disable CS8604 // Possible null reference argument.
        var maxSkillId = mandril.Skills.Any() ? mandril.Skills.Max(h => h.Id) : 0;
        #pragma warning restore CS8604 // Possible null reference argument.

        var newSkill = new Skill() 
        {
            Id = maxSkillId + 1,
            Name = skillInsert.Name,
            Power = skillInsert.Power
        };

        mandril.Skills?.Add(newSkill);

        return CreatedAtAction(nameof(GetSkill),
            new {mandrilId = mandrilId, skillId = newSkill.Id},
            newSkill
        );
    }

    [HttpPut("{skillId}")]
    public ActionResult<Skill> PutSkill([FromRoute] int mandrilId, [FromRoute] int skillId, [FromBody] SkillInsert skillInsert)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        var skill = mandril.Skills?.FirstOrDefault(h => h.Id == skillId);
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

        return Ok($"Skill with Id '{skillId}' has been edited on mandril with Id '{mandrilId}'");
    }

    [HttpDelete("{skillId}")]
    public ActionResult<Skill> DeleteSkill([FromRoute] int mandrilId, [FromRoute] int skillId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        var skill = mandril.Skills?.FirstOrDefault(h => h.Id == skillId);
        if (skill == null)
        {
            return NotFound($"Skill with Id '{skillId}' does not exist on mandril with Id '{mandrilId}'");
        }

        mandril.Skills?.Remove(skill);

        return Ok($"Skill with id '{skillId}' has been deleted on mandril with Id '{mandrilId}'"); 
    }

    [HttpDelete("all")]
    public ActionResult<IEnumerable<Skill>> DeleteSkill([FromRoute] int mandrilId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
        if (mandril == null)
        {
            return NotFound($"Mandril with Id '{mandrilId}' do not exist");
        }

        mandril.Skills?.Clear();

        return Ok($"All skills have been deleted on mandril with Id {mandrilId}");
    }
}