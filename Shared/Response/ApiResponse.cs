// Copyright (C) 2018 - 2023 Tony's Studio. All rights reserved.

namespace Tonisoft.AspExtensions.Response;

public class ApiResponse
{
    public bool Status { get; set; }
    public string? Message { get; set; }
    public object? Result { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(string message, bool status = false)
    {
        Status = status;
        Message = message;
        Result = null;
    }

    public ApiResponse(object result, bool status = true)
    {
        Status = status;
        Message = null;
        Result = result;
    }
}

public class ApiResponse<TResult> where TResult : class
{
    public bool Status { get; set; }
    public string? Message { get; set; }
    public TResult? Result { get; set; }

    public ApiResponse() { }

    public ApiResponse(string message, bool status = false)
    {
        Status = status;
        Message = message;
        Result = null;
    }

    public ApiResponse(TResult result, bool status = true)
    {
        Status = status;
        Message = null;
        Result = result;
    }
}