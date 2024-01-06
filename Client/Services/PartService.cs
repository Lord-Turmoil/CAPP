// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Client.Extensions.Request;
using RestSharp;
using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.Services;

class PartService : IPartService
{
    private const string Route = "api/Part/";
    private readonly IHttpRestClient _client;

    public PartService(IHttpRestClient client)
    {
        _client = client;
    }

    public Task<ApiResponse<PartDto>> GetPartAsync(int id)
    {
        var request = new BaseRequest($"{Route}GetPart?id={id}", Method.Get);
        return _client.ExecuteAsync<PartDto>(request);
    }

    public Task<ApiResponse<List<PartDto>>> GetPartsAsync()
    {
        var request = new BaseRequest($"{Route}GetParts", Method.Get);
        return _client.ExecuteAsync<List<PartDto>>(request);
    }

    public Task<ApiResponse<PartDto>> CreatePartAsync(string name, string opitz)
    {
        var request = new BaseRequest($"{Route}CreatePart?name={name}&opitz={opitz}", Method.Post);
        return _client.ExecuteAsync<PartDto>(request);
    }

    public async Task<ApiResponse> DeletePartAsync(int id)
    {
        var request = new BaseRequest($"{Route}DeletePart?id={id}", Method.Delete);
        return await _client.ExecuteAsync(request);
    }
}