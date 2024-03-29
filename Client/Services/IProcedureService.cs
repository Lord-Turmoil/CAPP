﻿// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Server.Modules.Core.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.Services;

interface IProcedureService
{
    Task<ApiResponse<ProcedureDto>> CreateProcedureAsync(int groupId, string description, int order);
    Task<ApiResponse<ProcedureDto>> UpdateProcedureAsync(int id, string description);
    Task<ApiResponse> DeleteProcedureAsync(int id);
}