using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Data { get; set; }

        public Response()
        {
        }

        public Response(bool succeeded, string? message = null, IEnumerable<string>? errors = null, T? data = default)
        {
            Succeeded = succeeded;
            Message = message ?? string.Empty;
            Errors = errors ?? Enumerable.Empty<string>(); 
            Data = data ?? default!;
        }
        public Response(T data, string? message = null)
        {
            Succeeded = true;
            Message = message ?? string.Empty;
            Data = data ?? default!;
        }

        //public Response Success(string? message = null)
        //{
        //    return new Response(true, message);
        //}

    }
}
