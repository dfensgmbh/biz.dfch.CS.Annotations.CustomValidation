using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.dfch.CS.Annotations.CustomValidation
{

    public class CustomAnnotationAttribute : ValidationAttribute
    {
        private readonly object _validationObject;
        public CustomAnnotationAttribute(object validationObject)
        {
            _validationObject = validationObject;
        }

        public override bool IsValid(Object value)
        {
            return false;
        }
        public override string FormatErrorMessage(string name)
        {
            return String.Format("ErrorMessage: '{0}'", name);
        }
    }
}
