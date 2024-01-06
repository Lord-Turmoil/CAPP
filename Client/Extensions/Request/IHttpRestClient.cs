using Tonisoft.AspExtensions.Response;

namespace Client.Extensions.Request;

interface IHttpRestClient
{
    Task<ApiResponse> ExecuteAsync(BaseRequest request);
    Task<ApiResponse<TResult>> ExecuteAsync<TResult>(BaseRequest request) where TResult : class;
}