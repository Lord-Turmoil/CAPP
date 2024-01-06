// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Shared.Dtos;

namespace Server.Modules.Core.Dtos;

public class ProcedureDto : TimestampDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Order { get; set; } = null!;
}