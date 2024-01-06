using Server.Modules.Core.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.Services;

interface IProcedureService
{
    Task<ApiResponse<ProcedureDto>> CreateProcedureAsync(int groupId, string description, int order);
    Task<ApiResponse> DeleteProcedureAsync(int id);
}