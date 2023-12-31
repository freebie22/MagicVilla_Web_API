﻿using System.Net;

namespace Magic_Villa_VillaApi.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            Errors = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public List<string> Errors { get; set; }
    }
}
