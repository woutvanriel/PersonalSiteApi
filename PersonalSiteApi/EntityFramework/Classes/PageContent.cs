using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonalSiteApi.EntityFramework.Classes
{
    public class PageContentDB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public PageDetailsDB? Details { get; set; }
        public ContentType Type { get; set; }
        public string? Content { get; set; }
        public string? Alt { get; set; }
        public int? Order { get; set; }
    }
}
