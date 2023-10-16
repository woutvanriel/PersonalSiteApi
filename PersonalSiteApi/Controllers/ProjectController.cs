using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalSiteApi.EntityFramework;
using PersonalSiteApi.EntityFramework.Classes;
using PersonalSiteApi.Models;

namespace PersonalSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        public ProjectController(IConfiguration config, PersonalSiteContext context, IWebHostEnvironment webHostEnvironment) : base(config, context, webHostEnvironment) { }

        [HttpGet]
        [Route("{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectDB>))]
        public IActionResult GetProjects(int page)
        {
            return Ok(_context.Projects.Skip(page * 18).Take(18).ToList());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProject(Guid id)
        {
            var project = GetProjectDB(id);
            if (project == null) return NotFound("No project found.");
            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddProject(Project project)
        {
            _context.Projects.Add(new ProjectDB
            {
                Slug = project.Slug,
            });
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EditProject(Project project)
        {
            if (project.Id == null || !project.Id.HasValue) return BadRequest("Id is required.");

            var projectDb = _context.Projects.FirstOrDefault(x => x.Id == project.Id);
            if (projectDb == null) return NotFound("Project not found.");

            if (project.Slug != null && projectDb.Slug != project.Slug) projectDb.Slug = project.Slug;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
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

        private ProjectDB? GetProjectDB(Guid id)
        {
            return _context.Projects.FirstOrDefault(x => x.Id == id);
        }
    }
}
