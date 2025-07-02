using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPdf_BE.DTOs
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static ServiceResult<T> SuccessResult(T data, string message = "")
            => new() { Success = true, Data = data, Message = message };

        public static ServiceResult<T> Failure(List<string>? errors = null)
            => new() { Success = false, Message = "Lỗi hệ thống", Errors = errors };
    }

}