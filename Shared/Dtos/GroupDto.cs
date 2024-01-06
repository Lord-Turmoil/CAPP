// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Server.Modules.Core.Dtos;

namespace Shared.Dtos;

public class GroupDto : TimestampDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Matrix { get; set; } = null!;

    public IEnumerable<ProcedureDto> Procedures { get; set; } = null!;

    public IEnumerable<PartDto> Parts { get; set; } = null!;
}