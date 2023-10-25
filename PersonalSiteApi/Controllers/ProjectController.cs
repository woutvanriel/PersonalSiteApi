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
    public class ProjectController : BaseController
    {
        public ProjectController(IConfiguration config, PersonalSiteContext context, IWebHostEnvironment webHostEnvironment) : base(config, context, webHostEnvironment) { }

        [HttpGet]
        [Authorize]
        [Route("{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectDB>))]
        public IActionResult GetProjects(int page)
        {
            return Ok(_context.Projects.OrderBy(x => x.Order).Skip(page * 18).Take(18).ToList());
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectDB>))]
        public IActionResult GetAllProjects()
        {
            return Ok(_context.Projects.OrderBy(x => x.Order).ToList());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProject(Guid id)
        {
            var project = GetProjectDB(id);
            project!.Images = project.Images!.ToList();
            if (project == null) return NotFound("No project found.");
            return Ok(project);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImages()
        {
            if (Request.ContentType == null || Request.ContentType.Split(";")[0] != "multipart/form-data") return StatusCode((int)HttpStatusCode.UnsupportedMediaType);
            if (Request.Form.Files.Count < 1) return BadRequest("No files found.");
            if (Request.Form["id"].Count == 0) return BadRequest("No id given.");

            var id = new Guid(Request.Form["id"].ToString());
            var project = _context.Projects.Include(x => x.Images).FirstOrDefault(x => x.Id == id);
            if (project == null) return NotFound("No Project found");

            if (project.Images == null) project.Images = new HashSet<ImageDB>();

            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                project.Images.Add(new ImageDB {
                    Location = await Utility.UploadFile(file, "project", _environment),
                });
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{projectid}/{imageid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteImage(Guid projectid, Guid imageid)
        {
            var project = _context.Projects.Include(x => x.Images).FirstOrDefault(x => x.Id == projectid);
            if (project == null) return NotFound("No Project found");
            if (project.Images == null) return Conflict("No images connected to Project.");
            var imageToRemove = project.Images.FirstOrDefault(x => x.Id == imageid);
            if (imageToRemove == null) return NotFound("Image not found.");
            project.Images.Remove(imageToRemove);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddProject(Project project)
        {
            var db = new ProjectDB
            {
                Slug = project.Slug,
                Title = project.Title
            };
            _context.Projects.Add(db);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return Conflict("Slug is already in use.");
            }
            return Ok(db.Id);
        }

        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EditProject(Project project)
        {
            if (project.Id == null || !project.Id.HasValue) return BadRequest("Id is required.");

            var projectDb = _context.Projects.FirstOrDefault(x => x.Id == project.Id);
            if (projectDb == null) return NotFound("Project not found.");

            if (project.Slug != null && projectDb.Slug != project.Slug) projectDb.Slug = project.Slug;
            if (project.Title != null && projectDb.Title != project.Title) projectDb.Title = project.Title;
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return Conflict("Slug is already in use.");
            }
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProject(Guid id)
        {
            var project = GetProjectDB(id);
            if (project == null) return NotFound("No project found.");
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult SaveOrder(Guid[] ids)
        {
            if (_context.Projects.Count() < ids.Length) return BadRequest("Not all projects given.");
            for (int i = 0; i < ids.Length; i++)
            {
                var project = _context.Projects.FirstOrDefault(x => x.Id == ids[i]);
                if (project == null) continue;
                project.Order = i;
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageDB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProjectBySlug(string slug)
        {
            return Ok(
                _context.Projects
                    .Include(x => x.Images)
                    .Include(x => x.Details!.Where(x => x.Language!.Name == _language))
                    .ThenInclude(x => x.Content!.OrderBy(x => x.Order))
                    .FirstOrDefault(x => x.Slug == slug)
            );
        }

        [HttpGet]
        [Route("{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PageDB>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProjectsDetails(int page)
        {
            return Ok(
                _context.Projects
                    .Include(x => x.Images)
                    .Include(x => x.Details!.Where(x => x.Language!.Name == _language))
                    .Skip(page * 18)
                    .Take(18)
                    .ToList()
            );
        }

        private ProjectDB? GetProjectDB(Guid id)
        {
            return _context.Projects.Include(x => x.Images).FirstOrDefault(x => x.Id == id);
        }
    }
}
