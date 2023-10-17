using PersonalSiteApi.EntityFramework.Classes;
using System.ComponentModel.DataAnnotations;

namespace PersonalSiteApi.Models
{
    public class ProjectContent
    {
        public Guid? Id { get; set; }
        public Guid DetailId { get; set; }
        public ProjectContentType Type { get; set; }
        public string? Content { get; set; }
        public int? Order { get; set; }
    }
}
