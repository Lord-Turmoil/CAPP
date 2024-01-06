﻿using Microsoft.AspNetCore.Mvc;
using Server.Modules.Core.Dtos;
using Server.Modules.Core.Services;
using Shared.Dtos;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class GroupController : BaseController<GroupController>
{
    private readonly IGroupService _service;
    public GroupController(ILogger<GroupController> logger, IGroupService service) : base(logger)
    {
        _service = service;
    }


    [HttpPost]
    public Task<ApiResponse<GroupDto>> CreateStepAsync([FromBody] CreateGroupDto dto)
    {
        return _service.CreateGroupAsync(dto.Description, dto.Matrix);
    }

    [HttpGet]
    public Task<ApiResponse<GroupDto>> GetGroupAsync(int id)
    {
        return _service.GetGroupAsync(id);
    }

    [HttpGet]
    public Task<ApiResponse<List<GroupDto>>> GetGroupsAsync()
    {
        return _service.GetGroupsAsync();
    }

    [HttpDelete]
    public Task<ApiResponse> DeleteGroupAsync(int id)
    {
        return _service.DeleteGroupAsync(id);
    }
}