using Microsoft.AspNetCore.Mvc;
using Server.Modules.Core.Dtos;
using Server.Modules.Core.Services;
using Shared.Dtos;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StepController : BaseController<StepController>
{
    private readonly IStepService _service;
    public StepController(ILogger<StepController> logger, IStepService service) : base(logger)
    {
        _service = service;
    }


    [HttpPost]
    public Task<ApiResponse<StepDto>> CreateStepAsync([FromBody] CreateStepDto dto)
    {
        return _service.CreateStepAsync(dto.ProcedureId, dto.Description, dto.Order);
    }
}