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
        private readonly string[] _allowedContentTypes;

        public ValidImageAttribute(string[] allowedContentTypes)
        {
            this._allowedContentTypes = allowedContentTypes;
            ErrorMessage = "Invalid Image Format Used";
        }

        public override bool IsValid(object value)
        {
            if (!(value is IFormFile))
                return false;
            IFormFile formFile = (IFormFile) value;
            Console.WriteLine(formFile.ContentType);
            bool typeCorrect = false;
            foreach (var type in _allowedContentTypes)
            {
                typeCorrect = formFile.ContentType.Equals("image/" + type);
                if (typeCorrect)
                    break;
            }
            
            // bool typeCorrect = formFile.ContentType.Equals("image/jpg") ||
                               // formFile.ContentType.Equals("image/png") ||
                               // formFile.ContentType.Equals("image/jpeg") ||
                               // formFile.ContentType.Equals("image/pjpeg") ||
                               // formFile.ContentType.Equals("image/x-png");
            return typeCorrect;
        }
    }
}
