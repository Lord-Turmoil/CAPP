using Server.Modules.Core.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services;

public interface IProcedureService
{
    Task<ApiResponse<ProcedureDto>> CreateProcedureAsync(int groupId, string description, int order);
    Task<ApiResponse> DeleteProcedureAsync(int id);
}