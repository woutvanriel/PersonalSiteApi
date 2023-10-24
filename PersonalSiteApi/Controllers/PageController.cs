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
    public class PageController : BaseController
    {
        public PageController(IConfiguration config, PersonalSiteContext context, IWebHostEnvironment webHostEnvironment) : base(config, context, webHostEnvironment) { }

        [HttpGet]
        [Route("{page}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PageDB>))]
        public IActionResult GetPages(int page)
        {
            return Ok(_context.Pages.OrderBy(x => x.Order).Skip(page * 18).Take(18).ToList());
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PageDB>))]
        public IActionResult GetAllPages()
        {
            return Ok(_context.Pages.OrderBy(x => x.Order).ToList());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageDB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPage(Guid id)
        {
            var Page = GetPageDB(id);
            if (Page == null) return NotFound("No Page found.");
            return Ok(Page);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddPage(Page Page)
        {
            var db = new PageDB
            {
                Slug = Page.Slug,
                Title = Page.Title,
            };
            _context.Pages.Add(db);
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
        public IActionResult EditPage(Page Page)
        {
            if (Page.Id == null || !Page.Id.HasValue) return BadRequest("Id is required.");

            var PageDb = _context.Pages.FirstOrDefault(x => x.Id == Page.Id);
            if (PageDb == null) return NotFound("Page not found.");

            if (Page.Slug != null && PageDb.Slug != Page.Slug) PageDb.Slug = Page.Slug;
            if (Page.Title != null && PageDb.Title != Page.Title) PageDb.Title = Page.Title;
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
        public IActionResult DeletePage(Guid id)
        {
            var Page = GetPageDB(id);
            if (Page == null) return NotFound("No Page found.");
            _context.Pages.Remove(Page);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SaveOrder(Guid[] ids)
        {
            if (_context.Pages.Count() < ids.Length) return BadRequest("Not all Pages given.");
            for (int i = 0; i < ids.Length; i++)
            {
                var Page = _context.Pages.FirstOrDefault(x => x.Id == ids[i]);
                if (Page == null) continue;
                Page.Order = i;
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PageDB>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPagesHeader()
        {
            return Ok(_context.Pages.Include(x => x.Details!.Where(x => x.Language!.Name == _language)).OrderBy(x => x.Order).ToList());
        }

        [HttpGet]
        [Route("{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageDB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPageBySlug(string slug)
        {
            return Ok(
                _context.Pages
                    .Include(x => x.Details!.Where(x => x.Language!.Name == _language))
                    .ThenInclude(x => x.Content!.OrderBy(x => x.Order))
                    .FirstOrDefault(x => x.Slug == slug)
            );
        }

        private PageDB? GetPageDB(Guid id)
        {
            return _context.Pages.FirstOrDefault(x => x.Id == id);
        }
    }
}
