// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.Core.Models;

[Owned]
public class Procedure : TimestampModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(1023)]
    public string Description { get; set; } = null!;

    [Required]
    public int Order { get; set; }

    [Required]
    public int GroupId { get; set; }

    [ForeignKey("GroupId")]
    public Group Group { get; set; } = null!;
}