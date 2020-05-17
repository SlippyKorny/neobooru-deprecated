using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.Utilities.Attributes
{
    public class ValidImageAttribute : ValidationAttribute
    {
        private readonly string[] allowedExtensions;

        public ValidImageAttribute(string[] allowedExtensions)
        {
            this.allowedExtensions = allowedExtensions;
            ErrorMessage = "Invalid Image Format Used";
        }

        public override bool IsValid(object value)
        {
            if (!(value is IFormFile))
                return false;
            IFormFile formFile = (IFormFile) value;
            Console.WriteLine(formFile.ContentType);
            return formFile.ContentType.Equals("image/jpg");
        }
    }
}
