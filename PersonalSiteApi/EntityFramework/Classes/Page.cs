﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSiteApi.EntityFramework.Classes
{
    [Index(nameof(Slug), IsUnique = true)]
    public class PageDB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public ICollection<PageDetailsDB>? Details { get; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public int? Order { get; set; }
    }
}
