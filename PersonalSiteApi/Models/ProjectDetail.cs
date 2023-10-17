using PersonalSiteApi.EntityFramework.Classes;

namespace PersonalSiteApi.Models
{
    public class ProjectDetail
    {
        public Guid? Id { get; set; }
        public Guid LanguageId { get; set; }
        public Guid ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
