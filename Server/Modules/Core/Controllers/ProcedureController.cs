// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

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
    public Task<ApiResponse<ProcedureDto>> CreateProcedureAsync(int groupId, string description, int order)
    {
        return _service.CreateProcedureAsync(groupId, description, order);
    }

    [HttpPut]
    public Task<ApiResponse<ProcedureDto>> UpdateProcedureAsync(int id, string description)
    {
        return _service.UpdateProcedureAsync(id, description);
    }

    [HttpDelete]
    public Task<ApiResponse> DeleteProcedureAsync(int id)
    {
        return _service.DeleteProcedureAsync(id);
    }
}