﻿using Client.Extensions.Request;
using RestSharp;
using Server.Modules.Core.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.Services;

class ProcedureService : IProcedureService
{
    private const string Route = "api/Procedure/";
    private readonly IHttpRestClient _client;

    public ProcedureService(IHttpRestClient client)
    {
        _client = client;
    }

    public Task<ApiResponse<ProcedureDto>> CreateProcedureAsync(int groupId, string description, int order)
    {
        var request =
            new BaseRequest($"{Route}CreateProcedure?groupId={groupId}&description={description}&order={order}",
                Method.Post);
        return _client.ExecuteAsync<ProcedureDto>(request);
    }

    public Task<ApiResponse> DeleteProcedureAsync(int id)
    {
        var request = new BaseRequest($"{Route}DeleteProcedure?id={id}", Method.Delete);
        return _client.ExecuteAsync(request);
    }
}