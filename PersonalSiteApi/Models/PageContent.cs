using PersonalSiteApi.EntityFramework.Classes;
using System.ComponentModel.DataAnnotations;

namespace PersonalSiteApi.Models
{
    public class PageContent
    {
        public Guid? Id { get; set; }
        public Guid DetailId { get; set; }
        public PageContentType Type { get; set; }
        public string? Content { get; set; }
        public int? Order { get; set; }
    }
}
