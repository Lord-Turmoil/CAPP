// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Microsoft.AspNetCore.Mvc;
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
    public Task<ApiResponse<GroupDto>> CreateGroupAsync(string description, string matrix)
    {
        return _service.CreateGroupAsync(description, matrix);
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

    [HttpGet]
    public Task<ApiResponse<List<GroupDto>>> SearchGroups(string opitz)
    {
        return _service.SearchGroups(opitz);
    }
}