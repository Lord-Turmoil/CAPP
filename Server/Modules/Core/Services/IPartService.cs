using Server.Modules.Core.Models;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services;

public interface IPartService
{
    Task<ApiResponse<Part>> GetPartAsync(int id);
    Task<ApiResponse<List<Part>>> GetPartsAsync();
    Task<ApiResponse<Part>> CreatePartAsync(string name, string opitz);
    Task<ApiResponse> DeletePartAsync(int id);
}