using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalSiteApi.EntityFramework;
using PersonalSiteApi.EntityFramework.Classes;
using PersonalSiteApi.Models;

namespace PersonalSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectDetailController : BaseController
    {
        public ProjectDetailController(IConfiguration config, PersonalSiteContext context, IWebHostEnvironment webHostEnvironment) : base(config, context, webHostEnvironment) { }

        [HttpGet]
        [Authorize]
        [Route("{projectid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectDetailsDB>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProjectDetails(Guid projectid)
        {
            var projects = _context.ProjectDetails.Include(x => x.Project).Include(x => x.Language).Where(x => x.Project != null && x.Project.Id == projectid).ToList();
            return Ok(projects);
        }

        [HttpGet]
        [Authorize]
        [Route("{detailid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDetailsDB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProjectDetail(Guid detailid)
        {
            var detail = _context.ProjectDetails.Include(x => x.Project).Include(x => x.Language).FirstOrDefault(x => x.Id == detailid);
            if (detail == null) NotFound("No project detail found.");
            return Ok(detail);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddProjectDetail(ProjectDetail projectDetail)
        {
            var language = _context.Languages.FirstOrDefault(x => x.Id == projectDetail.LanguageId);
            if (language == null) return NotFound("Language not found.");
            var project = _context.Projects.FirstOrDefault(x => x.Id == projectDetail.ProjectId);
            if (project == null) return NotFound("Project not found.");
            var db = new ProjectDetailsDB
            {
                Language = language,
                Project = project,
                Title = projectDetail.Title,
                Description = projectDetail.Description,
            };
            _context.ProjectDetails.Add(db);
            _context.SaveChanges();
            return Ok(db.Id);
        }

        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EditProjectDetail(ProjectDetail projectDetail)
        {
            var db = _context.ProjectDetails.Include(x => x.Language).Include(x => x.Project).FirstOrDefault(x => x.Id == projectDetail.Id);
            if (db == null) return NotFound("Project detail not found.");

            if (db.Language != null && db.Language.Id != projectDetail.Id)
            {
                var language = _context.Languages.FirstOrDefault(x => x.Id == projectDetail.LanguageId);
                if (language == null) return NotFound("Language not found.");
                db.Language = language;
            }

            if (db.Title != projectDetail.Title) db.Title = projectDetail.Title;
            if (db.Description != projectDetail.Description) db.Description = projectDetail.Description;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{detailid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProjectDetail(Guid detailid)
        {
            var db = _context.ProjectDetails.FirstOrDefault(x => x.Id == detailid);
            if (db == null) return NotFound("Project detail not found.");
            _context.ProjectDetails.Remove(db);
            _context.SaveChanges();
            return Ok();
        }

        private ProjectDetailsDB? GetProjectDetails(Guid projectid, Guid languageid)
        {
            return _context.ProjectDetails.Include(x => x.Language).Include(x => x.Project).FirstOrDefault(x => x.Project != null && x.Project.Id == projectid && x.Language != null && x.Language.Id == languageid);
        }
    }
}
