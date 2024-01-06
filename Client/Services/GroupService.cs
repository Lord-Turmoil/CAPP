﻿using Client.Extensions.Request;
using RestSharp;
using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.Services;

class GroupService : IGroupService
{
    private const string Route = "api/Group/";
    private readonly HttpRestClient _client;

    public GroupService(HttpRestClient client)
    {
        _client = client;
    }

    public Task<ApiResponse<GroupDto>> CreateGroupAsync(string description, string matrix)
    {
        var request = new BaseRequest($"{Route}CreateGroup?description={description}&matrix={matrix}", Method.Post);
        return _client.ExecuteAsync<GroupDto>(request);
    }

    public Task<ApiResponse<GroupDto>> GetGroupAsync(int id)
    {
        var request = new BaseRequest($"{Route}GetGroup?id={id}", Method.Get);
        return _client.ExecuteAsync<GroupDto>(request);
    }

    public Task<ApiResponse<List<GroupDto>>> GetGroupsAsync()
    {
        var request = new BaseRequest($"{Route}GetGroups", Method.Get);
        return _client.ExecuteAsync<List<GroupDto>>(request);
    }

    public Task<ApiResponse> DeleteGroupAsync(int id)
    {
        var request = new BaseRequest($"{Route}DeleteGroup?id={id}", Method.Delete);
        return _client.ExecuteAsync(request);
    }
}