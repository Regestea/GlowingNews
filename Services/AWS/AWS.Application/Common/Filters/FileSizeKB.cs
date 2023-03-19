using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AWS.Application.Common.Filters
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FileSizeMegabyteAttribute : ValidationAttribute
    {
        private readonly double _minFileSize;
        private readonly double _maxFileSize;


        public FileSizeMegabyteAttribute(double minFileSize, double maxFileSize)
        {
            _minFileSize = minFileSize;
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var maxFileSize = _maxFileSize * 1024 * 1024;
            var minFileSize = _minFileSize * 1024 * 1024;

            if (value is IFormFile formFile)
            {
                if (formFile.Length > maxFileSize|| formFile.Length < minFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            else if (value is IFormFileCollection formFileCollection)
            {
                foreach (var file in formFileCollection)
                {
                    if (file.Length > maxFileSize || file.Length < minFileSize)
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }
            else if (value is string base64File)
            {
                if (base64File.Length > maxFileSize || base64File.Length < minFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            else if (value is ICollection<string> collection)
            {
                foreach (var file in collection)
                {
                    if (file.Length > maxFileSize || file.Length < minFileSize)
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return string.Format("Allowed file size is between {0} KB and {1} KB", _minFileSize,
                _maxFileSize);
        }
    }
}
