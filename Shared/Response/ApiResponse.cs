// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

namespace Tonisoft.AspExtensions.Response;

public class ApiResponse
{
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

    public bool Status { get; set; }
    public string? Message { get; set; }
    public object? Result { get; set; }
}

public class ApiResponse<TResult> where TResult : class
{
    public ApiResponse()
    {
    }

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

    public bool Status { get; set; }
    public string? Message { get; set; }
    public TResult? Result { get; set; }
}