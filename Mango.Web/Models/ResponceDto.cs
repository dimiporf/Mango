﻿namespace Mango.Web.Models
{
    public class ResponseDto
    {
        // Gets or sets the result object of the response
        public object? Result { get; set; }

        // Gets or sets a boolean indicating if the operation was successful
        public bool IsSuccess { get; set; } = true;

        // Gets or sets a message describing the result or any error
        public string Message { get; set; } = "";
    }
}

