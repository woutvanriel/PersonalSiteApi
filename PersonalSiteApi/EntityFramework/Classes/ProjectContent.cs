﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PersonalSiteApi.EntityFramework.Classes
{
    public class ProjectContentDB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public ProjectDetailsDB? Details { get; set; }
        public ProjectContentType Type { get; set; }
        [StringLength(int.MaxValue)]
        public string? Content { get; set; }
        public int? Order { get; set; }
    }

    public enum ProjectContentType
    {
        Text = 0,
        Image = 1,
        Html = 2,
    }
}
