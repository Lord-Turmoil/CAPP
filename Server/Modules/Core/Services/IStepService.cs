using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services;

public interface IStepService
{
    Task<ApiResponse<StepDto>> CreateStepAsync(int procedureId, string description, int order);
}