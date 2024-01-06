// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using RestSharp;

namespace Client.Extensions.Request;

class BaseRequest
{
    public BaseRequest(string route, Method method)
    {
        Route = route;
        Method = method;
    }

    public Method Method { get; set; }
    public string Route { get; set; } = "";
    public string ContentType { get; set; } = "application/json";

    public object? Parameter { get; set; }
}