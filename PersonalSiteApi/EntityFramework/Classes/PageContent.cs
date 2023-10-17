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
        public PageContentType Type { get; set; }
        [StringLength(int.MaxValue)]
        public string? Content { get; set; }
        public int? Order { get; set; }
    }

    public enum PageContentType
    {
        Text = 0,
        Image = 1,
        Html = 2,
        ProjectContainer = 3
    }
}
