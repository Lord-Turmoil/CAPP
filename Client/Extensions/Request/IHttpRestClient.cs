// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Tonisoft.AspExtensions.Response;

namespace Client.Extensions.Request;

interface IHttpRestClient
{
    Task<ApiResponse> ExecuteAsync(BaseRequest request);
    Task<ApiResponse<TResult>> ExecuteAsync<TResult>(BaseRequest request) where TResult : class;
}