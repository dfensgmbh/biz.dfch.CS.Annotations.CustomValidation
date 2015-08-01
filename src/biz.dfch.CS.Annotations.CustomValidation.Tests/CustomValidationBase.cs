using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace biz.dfch.CS.Annotations.CustomValidation.Tests
{
    public class CustomValidationBase
    {
        // This code is never invoked by System.ComponentModel.DataAnnotation 
        // as it does not find the Validator method in this base class.
        public static ValidationResult AnnotationWithValidationInBaseClassValidator(String name, ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
