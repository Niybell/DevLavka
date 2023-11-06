using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.Frontend.ResponseModels
{
    public class TextServerResponse
    {
        public TextServerResponse(string description, int statusCode)
        {
            Description = description;
            StatusCode = statusCode;
        }

        public string Description { get; set; }
        public int StatusCode { get; set; }
    }
}