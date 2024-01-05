using Microsoft.AspNetCore.Mvc;
using Server.Modules.Core.Dtos;
using Server.Modules.Core.Services;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProcedureController : BaseController<ProcedureController>
{
    private readonly IProcedureService _service;

    public ProcedureController(ILogger<ProcedureController> logger, IProcedureService service) : base(logger)
    {
        _service = service;
    }

    [HttpPost]
    public Task<ApiResponse<ProcedureDto>> CreateProcedureAsync([FromBody] CreateProcedureDto dto)
    {
        return _service.CreateProcedureAsync(dto.PartId, dto.Description, dto.Order);
    }

    [HttpDelete]
    public Task<ApiResponse> DeleteProcedureAsync(int id)
    {
        return _service.DeleteProcedureAsync(id);
    }
}