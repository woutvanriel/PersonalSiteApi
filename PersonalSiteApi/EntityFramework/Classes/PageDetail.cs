using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonalSiteApi.EntityFramework.Classes
{
    public class PageDetailsDB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public PageDB? Page { get; set; }
        public LanguageDB? Language { get; set; }
        public ICollection<PageContentDB>? Content { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
