using Server.Modules.Core.Dtos;
using Server.Modules.Core.Models;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services;

public interface IPartService
{
    Task<ApiResponse<PartDto>> GetPartAsync(int id);
    Task<ApiResponse<List<PartDto>>> GetPartsAsync();
    Task<ApiResponse<PartDto>> CreatePartAsync(string name, string opitz);
    Task<ApiResponse> DeletePartAsync(int id);
}