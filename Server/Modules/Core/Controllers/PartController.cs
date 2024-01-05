using Microsoft.AspNetCore.Mvc;
using Server.Modules.Core.Dtos;
using Server.Modules.Core.Services;
using Shared.Dtos;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PartController : BaseController<PartController>
{
    private readonly IPartService _service;
    public PartController(ILogger<PartController> logger, IPartService service) : base(logger)
    {
        _service = service;
    }

    [HttpPost]
    public Task<ApiResponse<PartDto>> CreatePartAsync([FromBody] CreatePartDto dto)
    {
        return _service.CreatePartAsync(dto.Name, dto.Opitz);
    }

    [HttpGet]
    public Task<ApiResponse<PartDto>> GetPartAsync(int id)
    {
        return _service.GetPartAsync(id);
    }

    [HttpGet]
    public Task<ApiResponse<List<PartDto>>> GetPartsAsync()
    {
        return _service.GetPartsAsync();
    }

    [HttpDelete]
    public Task<ApiResponse> DeletePartAsync(int id)
    {
        return _service.DeletePartAsync(id);
    }
}