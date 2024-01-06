// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services;

public interface IGroupService
{
    Task<ApiResponse<GroupDto>> CreateGroupAsync(string description, string matrix);
    Task<ApiResponse<GroupDto>> UpdateGroupAsync(int id, string description, string matrix);

Task<ApiResponse<GroupDto>> GetGroupAsync(int id);
    Task<ApiResponse<List<GroupDto>>> GetGroupsAsync();
    Task<ApiResponse> DeleteGroupAsync(int id);

    Task<ApiResponse<List<GroupDto>>> SearchGroups(string opitz);
}