namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _MaxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _MaxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
              if(file.Length > _MaxFileSize) {
                    return new ValidationResult($"Maximum allowed size is {_MaxFileSize} Bytes");
                }  
            }

            return ValidationResult.Success;
        }
    }
}
