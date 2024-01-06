﻿using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.Services;

interface IPartService
{
    Task<ApiResponse<PartDto>> GetPartAsync(int id);
    Task<ApiResponse<List<PartDto>>> GetPartsAsync();
    Task<ApiResponse<PartDto>> CreatePartAsync(string name, string opitz);
    Task<ApiResponse> DeletePartAsync(int id);
}