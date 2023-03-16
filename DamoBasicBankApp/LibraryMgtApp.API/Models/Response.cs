using LibraryManagementApp.Interfaces;

namespace LibraryManagementApp.Models
{
    public class Response<T>
    {

        public Response()
        {

        }

        public Response(bool success)
        {
            Success = success;
        }

        public Response(bool success, string message) : this(success)
        {
            Message = message;
        }

        public Response(bool success, T content) : this(success)
        {
            Content = content;
        }

        public Response(bool success, string message, T content) : this(success, message)
        {
            Content = content;
        }
        public int? Status { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public string Code { get; set; }
        public T Content { get; set; }

    }

    public class Response : Response<object>
    {
        public Response() : base()
        {
        }

        public Response(bool success) : base(success)
        {
        }

        public Response(bool success, string message) : base(success, message)
        {
        }

        public Response(bool success, object content) : base(success, string.Empty, content)
        {
        }

        public Response(bool success, string message, object content) : base(success, message, content)
        {
        }
    }

    public class SuccessResponse : Response
    {
        public SuccessResponse() : base(true)
        {
            Status = 200;
        }

        public SuccessResponse(object content) : base(true, string.Empty, content)
        {

        }

        public SuccessResponse(string message) : base(true, message, null)
        {

        }

        public SuccessResponse(string message, object content) : base(true, message, content)
        {

        }

    }

    public class SuccessResponse<T> : Response<T>
    {
        public SuccessResponse() : base(true)
        {
            Status = 200;
        }

        public SuccessResponse(T content) : base(true, string.Empty, content)
        {

        }

        public SuccessResponse(string message, T content) : base(true, message, content)
        {

        }

    }

    public class ErrorResponse : Response
    {
        public ErrorResponse() : base(false)
        {
            Status = 500;
        }

        public ErrorResponse(string message) : base(false, message)
        {

        }

        public ErrorResponse(string message, string code) : base(false, message)
        {
            Code = code;
        }

    }

    public class ErrorResponse<T> : Response<T>
    {
        public ErrorResponse() : base(false)
        {
            Status = 500;
        }

        public ErrorResponse(string message) : base(false, message)
        {

        }

        public ErrorResponse(string message, string code) : base(false, message)
        {
            Code = code;
        }
    }
}