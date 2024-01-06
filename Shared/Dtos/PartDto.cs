// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

namespace Shared.Dtos;

public class PartDto : TimestampDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Opitz { get; set; } = null!;
}