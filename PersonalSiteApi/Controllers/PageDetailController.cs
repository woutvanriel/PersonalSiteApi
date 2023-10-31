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
    public class PageDetailController : BaseController
    {
        public PageDetailController(IConfiguration config, PersonalSiteContext context, IWebHostEnvironment webHostEnvironment) : base(config, context, webHostEnvironment) { }

        [HttpGet]
        [Authorize]
        [Route("{Pageid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PageDetailsDB>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPageDetails(Guid Pageid)
        {
            var Pages = _context.PageDetails.Include(x => x.Page).Include(x => x.Language).Where(x => x.Page != null && x.Page.Id == Pageid).ToList();
            return Ok(Pages);
        }

        [HttpGet]
        [Authorize]
        [Route("{detailid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageDetailsDB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPageDetail(Guid detailid)
        {
            var detail = _context.PageDetails.Include(x => x.Page).Include(x => x.Language).FirstOrDefault(x => x.Id == detailid);
            if (detail == null) NotFound("No Page detail found.");
            return Ok(detail);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddPageDetail(PageDetail PageDetail)
        {
            var language = _context.Languages.FirstOrDefault(x => x.Id == PageDetail.LanguageId);
            if (language == null) return NotFound("Language not found.");
            var Page = _context.Pages.FirstOrDefault(x => x.Id == PageDetail.PageId);
            if (Page == null) return NotFound("Page not found.");
            var db = new PageDetailsDB
            {
                Language = language,
                Page = Page,
                Title = PageDetail.Title,
                Description = PageDetail.Description,
            };
            _context.PageDetails.Add(db);
            _context.SaveChanges();
            return Ok(db.Id);
        }

        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EditPageDetail(PageDetail PageDetail)
        {
            var db = _context.PageDetails.Include(x => x.Language).Include(x => x.Page).FirstOrDefault(x => x.Id == PageDetail.Id);
            if (db == null) return NotFound("Page detail not found.");

            if (db.Language != null && db.Language.Id != PageDetail.Id)
            {
                var language = _context.Languages.FirstOrDefault(x => x.Id == PageDetail.LanguageId);
                if (language == null) return NotFound("Language not found.");
                db.Language = language;
            }

            if (db.Title != PageDetail.Title) db.Title = PageDetail.Title;
            if (db.Description != PageDetail.Description) db.Description = PageDetail.Description;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{detailid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePageDetail(Guid detailid)
        {
            var details = _context.PageDetails.Include(x => x.Content).FirstOrDefault(x => x.Id == detailid);
            if (details == null) return NotFound("Page detail not found.");
            foreach (var content in details.Content!)
            {
                _context.PageContent.Remove(content);
            }
            _context.PageDetails.Remove(details);
            _context.SaveChanges();
            return Ok();
        }

        private PageDetailsDB? GetPageDetails(Guid Pageid, Guid languageid)
        {
            return _context.PageDetails.Include(x => x.Language).Include(x => x.Page).FirstOrDefault(x => x.Page != null && x.Page.Id == Pageid && x.Language != null && x.Language.Id == languageid);
        }
    }
}
