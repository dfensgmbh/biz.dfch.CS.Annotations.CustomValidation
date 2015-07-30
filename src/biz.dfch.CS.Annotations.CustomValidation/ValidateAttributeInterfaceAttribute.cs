using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.dfch.CS.Annotations.CustomValidation
{

    public class ValidateAttributeInterfaceAttribute : ValidationAttribute
    {
        private readonly ICustomValidatable _validationObject;

        public ValidateAttributeInterfaceAttribute(ICustomValidatable validationObject)
        {
            _validationObject = validationObject;
        }

        public override bool IsValid(Object value)
        {
            return _validationObject.IsValid(value);
        }
    }
}
