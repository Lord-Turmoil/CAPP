// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.Core.Models;

[Owned]
public class Group : TimestampModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; } = null!;

    [Required]
    [MaxLength(127)]
    public string Matrix { get; set; } = null!;

    public IEnumerable<Procedure> Procedures { get; set; } = null!;
}