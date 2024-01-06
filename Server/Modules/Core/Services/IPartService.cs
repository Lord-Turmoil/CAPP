// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services;

public interface IPartService
{
    Task<ApiResponse<PartDto>> GetPartAsync(int id);
    Task<ApiResponse<List<PartDto>>> GetPartsAsync();
    Task<ApiResponse<PartDto>> CreatePartAsync(string name, string opitz);
    Task<ApiResponse<PartDto>> UpdatePartAsync(int id, string name, string opitz);
    Task<ApiResponse> DeletePartAsync(int id);
}