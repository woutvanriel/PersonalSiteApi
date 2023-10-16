using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalSiteApi.EntityFramework.Classes;
using PersonalSiteApi.EntityFramework;
using PersonalSiteApi.Models;

namespace PersonalSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LanguageController : BaseController
    {
        public LanguageController(IConfiguration config, PersonalSiteContext context) : base(config, context) { } 

        [HttpGet]
        [Route("{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LanguageDB>))]
        public IActionResult GetLanguages(int page)
        {
            return Ok(_context.Languages.Skip(page * 20).Take(20).ToList());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LanguageDB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLanguage(Guid id)
        {
            var Language = GetLanguageDB(id);
            if (Language == null) return NotFound("No Language found.");
            return Ok(Language);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddLanguage(Language Language)
        {
            _context.Languages.Add(new LanguageDB
            {
                Name = Language.Name
            });
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EditLanguage(Language Language)
        {
            var LanguageDb = _context.Languages.FirstOrDefault(x => x.Id == Language.Id);
            if (LanguageDb == null) return NotFound("Language not found.");

            if (Language.Name != null && LanguageDb.Name != Language.Name) LanguageDb.Name = Language.Name;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteLanguage(Guid id)
        {
            var Language = GetLanguageDB(id);
            if (Language == null) return NotFound("No Language found.");
            _context.Languages.Remove(Language);
            _context.SaveChanges();
            return Ok();
        }

        private LanguageDB? GetLanguageDB(Guid id)
        {
            return _context.Languages.FirstOrDefault(x => x.Id == id);
        }
    }
}
