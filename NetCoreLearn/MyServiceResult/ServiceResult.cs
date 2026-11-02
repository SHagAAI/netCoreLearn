using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreLearn.MyServiceResult
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }


        // Helper method for Success
        public static ServiceResult<T> Ok(T data, string? message = null)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Data = data,
                StatusCode = 200,
                Message = message
            };
        }

        // Helper method for Not Found (404)
        public static ServiceResult<T> NotFound(string message = "Resource not found.")
        {
            return new ServiceResult<T>
            {
                Success = false,
                StatusCode = 404,
                Message = message
            };
        }

        // Helper method for Bad Request (400)
        public static ServiceResult<T> BadRequest(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                StatusCode = 400,
                Message = message
            };
        }

        // Helper method for custom failure (e.g., 409 Conflict, 401 Unauthorized)
        public static ServiceResult<T> Failure(string message, int statusCode = 400)
        {
            return new ServiceResult<T>
            {
                Success = false,
                StatusCode = statusCode,
                Message = message
            };
        }

        // Helper method for custom success (e.g., 201 Created, 200 Ok)
        public static ServiceResult<T> Okay(T data, string? message = null, int statusCode = 200)
        {
            return new ServiceResult<T>
            {
                Success = true,
                StatusCode = statusCode,
                Data = data,
                Message = message
            };
        }
    }


    // 2. Non-generic version for operations that return NO data (like DELETE)
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public static ServiceResult Ok(string? message = null) =>
            new() { Success = true, StatusCode = 200, Message = message };
        public static ServiceResult NotFound(string message = "Resource not found.") =>
            new() { Success = false, StatusCode = 404, Message = message };
        public static ServiceResult BadRequest(string message) =>
            new() { Success = false, StatusCode = 400, Message = message };
    }
}







