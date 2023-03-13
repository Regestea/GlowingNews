using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AWS.Application.Common.Filters
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FileAllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public FileAllowedExtensionsAttribute(params string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile formFile)
            {
                var extension = Path.GetExtension(formFile.FileName)?.ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !_extensions.Contains(extension))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            else if (value is IFormFileCollection formFileCollection)
            {
                foreach (var file in formFileCollection)
                {
                    var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
                    if (string.IsNullOrEmpty(extension) || !_extensions.Contains(extension))
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "ThisExtensionIsNotAllowed";
        }
    }
}
