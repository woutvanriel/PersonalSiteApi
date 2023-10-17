using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PersonalSiteApi.EntityFramework.Classes
{
    [Index(nameof(Slug), IsUnique = true)]
    public class ProjectDB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public ICollection<ProjectContentDB>? Content { get; set; }
        public ICollection<ProjectDetailsDB>? Details { get; set; }
        public ICollection<ImageDB>? Images { get; set; }
        public string? Slug { get; set; }
        public int? Order { get; set; }
    }
}
