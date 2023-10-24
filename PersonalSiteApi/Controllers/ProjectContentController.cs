using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalSiteApi.EntityFramework;
using PersonalSiteApi.EntityFramework.Classes;
using PersonalSiteApi.Models;
using System.Net;

namespace PersonalSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectContentController : BaseController
    {
        public ProjectContentController(IConfiguration config, PersonalSiteContext context, IWebHostEnvironment webHostEnvironment) : base(config, context, webHostEnvironment) { }

        [HttpGet]
        [Route("{detailid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectContentDB>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllContent(Guid detailid)
        {
            var detail = _context.ProjectDetails.Include(x => x.Content).FirstOrDefault(x => x.Id == detailid);
            if (detail == null) return NotFound("No Detail found.");
            return Ok(detail.Content!.OrderBy(x => x.Order));
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectContentDB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetContent(Guid id)
        {
            var content = _context.ProjectContent.FirstOrDefault(x => x.Id == id);
            if (content == null) return NotFound("No Content found.");
            return Ok(content);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddContent(ProjectContent content)
        {
            var detail = _context.ProjectDetails.Include(x => x.Content).FirstOrDefault(x => x.Id == content.DetailId);
            if (detail == null) return NotFound("No Detail found.");
            var db = new ProjectContentDB()
            {
                Details = detail,
                Type = content.Type,
                Content = content.Content,
                Order = detail!.Content!.Count(),
                Alt = content.Alt,
            };
            detail.Content!.Add(db);
            _context.SaveChanges();
            return Ok(db.Id);
        }

        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EditContent(ProjectContent content)
        {
            var db = _context.ProjectContent.FirstOrDefault(x => x.Id == content.Id);
            if (db == null) return NotFound("No Content found.");
            if (db.Type != content.Type) db.Type = content.Type;
            if (db.Content != content.Content) db.Content = content.Content;
            if (db.Alt !=  content.Alt) db.Alt = content.Alt;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult SaveOrder(Guid[] ids)
        {
            if (_context.ProjectContent.Count() < ids.Length) return BadRequest("Not all projects given.");
            for (int i = 0; i < ids.Length; i++)
            {
                var project = _context.ProjectContent.FirstOrDefault(x => x.Id == ids[i]);
                if (project == null) continue;
                project.Order = i;
            }
            _context.SaveChanges();
            return Ok();
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UploadImage()
        {
            if (Request.ContentType == null || Request.ContentType.Split(";")[0] != "multipart/form-data") return StatusCode((int)HttpStatusCode.UnsupportedMediaType);
            if (Request.Form.Files.Count < 1) return BadRequest("No files found.");
            if (Request.Form["id"].Count == 0) return BadRequest("No id given.");

            Guid id = new Guid(Request.Form["id"]!);
            var content = _context.ProjectContent.FirstOrDefault(x => x.Id == id);
            if (content == null) return NotFound("Content not found.");

            var file = Request.Form.Files.First();
            if (file == null) return NotFound("No files found.");

            content.Content = await Utility.UploadFile(file, "content", _environment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteContent(Guid id)
        {
            var content = _context.ProjectContent.FirstOrDefault(x => x.Id == id);
            if (content == null) return NotFound("Content not found.");
            _context.ProjectContent.Remove(content);
            _context.SaveChanges();
            return Ok();
        }
    }
}
