using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using toolservice.Service.Interface;
namespace toolservice.Model 
{
    public class ToolTypeValidation :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IToolTypeService) validationContext
                         .GetService(typeof(IToolTypeService));

            string property = validationContext.DisplayName as string;
            var model = validationContext.ObjectInstance.GetType().GetProperty(property);
            var curPropertyValue = model.GetValue
                (validationContext.ObjectInstance, null);
 
            if (curPropertyValue == null || (int)curPropertyValue < 0)
                return new ValidationResult("Object null");

            var toolType = service.getToolType((int)curPropertyValue).Result;

            if(toolType == null)
                return new ValidationResult("id tooltype It is not valid"); 
            
            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage; 
 
            return $"{validationContext.DisplayName} ";
        }

    }
}