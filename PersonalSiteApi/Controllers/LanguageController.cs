using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalSiteApi.EntityFramework.Classes;
using PersonalSiteApi.EntityFramework;
using PersonalSiteApi.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace PersonalSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LanguageController : BaseController
    {
        public LanguageController(IConfiguration config, PersonalSiteContext context, IWebHostEnvironment webHostEnvironment) : base(config, context, webHostEnvironment) { }

        [HttpGet]
        [Route("{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LanguageDB>))]
        public IActionResult GetLanguages(int page)
        {
            return Ok(_context.Languages.Skip(page * 20).Take(20).ToList());
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public IActionResult CountLanguages()
        {
            return Ok(_context.Languages.Count());
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage()
        {
            if (Request.ContentType == null || Request.ContentType.Split(";")[0] != "multipart/form-data") return StatusCode((int)HttpStatusCode.UnsupportedMediaType);
            if (Request.Form.Files.Count < 1) return BadRequest("No files found.");
            if (Request.Form["id"].Count == 0) return BadRequest("No id given.");

            var id = new Guid(Request.Form["id"].ToString());
            var language = _context.Languages.FirstOrDefault(x => x.Id == id);
            if (language == null) return NotFound("No Language found");

            IFormFile file = Request.Form.Files.First();
            string location = await Utility.UploadFile(file, "flag", _environment);

            language.Flag = location;
            _context.SaveChanges();

            return Ok(location);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteImage(Guid id) {
            var language = _context.Languages.FirstOrDefault(x => x.Id == id);
            if (language == null) return NotFound("No Language found");
            language.Flag = null;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddLanguage(Language Language)
        {
            LanguageDB db = new LanguageDB
            {
                Name = Language.Name
            };
            _context.Languages.Add(db);
            _context.SaveChanges();
            return Ok(db.Id);
        }

        [HttpPatch]
        [Authorize]
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
        [Authorize]
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
