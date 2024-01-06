// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Newtonsoft.Json;
using RestSharp;
using Tonisoft.AspExtensions.Response;

namespace Client.Extensions.Request;

class HttpRestClient : IHttpRestClient
{
    private readonly string _apiUrl;
    private readonly RestClient _client;

    public HttpRestClient(string apiUrl)
    {
        _apiUrl = apiUrl;
        _client = new RestClient();
    }

    public async Task<ApiResponse> ExecuteAsync(BaseRequest request)
    {
        var restRequest = new RestRequest(_apiUrl + request.Route, request.Method);
        restRequest.AddHeader("ContentType", request.ContentType);

        if (request.Parameter != null)
        {
            restRequest.AddParameter("param", JsonConvert.SerializeObject(request.Parameter));
        }

        RestResponse response = await _client.ExecuteAsync(restRequest);
        if (response.Content == null)
        {
            return new ApiResponse("Nothing", true);
        }

        return JsonConvert.DeserializeObject<ApiResponse>(response.Content)!;
    }

    public async Task<ApiResponse<TResult>> ExecuteAsync<TResult>(BaseRequest request) where TResult : class
    {
        var restRequest = new RestRequest(_apiUrl + request.Route, request.Method);
        restRequest.AddHeader("ContentType", request.ContentType);

        if (request.Parameter != null)
        {
            restRequest.AddParameter("param", JsonConvert.SerializeObject(request.Parameter));
        }

        RestResponse response = await _client.ExecuteAsync<TResult>(restRequest);
        if (response.Content == null)
        {
            return new ApiResponse<TResult>("Nothing", true);
        }

        return JsonConvert.DeserializeObject<ApiResponse<TResult>>(response.Content)!;
    }
}