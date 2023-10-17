using PersonalSiteApi.EntityFramework.Classes;

namespace PersonalSiteApi.Models
{
    public class PageDetail
    {
        public Guid? Id { get; set; }
        public Guid LanguageId { get; set; }
        public Guid PageId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
