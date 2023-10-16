using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonalSiteApi.EntityFramework.Classes
{
    public class ProjectDetailsDB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public ProjectDB? Project { get; set; }
        public LanguageDB? Language { get; set; }
        public ICollection<PageContentDB>? Content { get; set; }
        public string? Title { get; set; }
    }
}
