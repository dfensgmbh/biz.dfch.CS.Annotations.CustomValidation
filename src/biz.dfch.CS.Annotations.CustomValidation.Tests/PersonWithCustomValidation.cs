/**
 *
 *
 * Copyright 2015 Ronald Rink, d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using biz.dfch.CS.Annotations.CustomValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace biz.dfch.CS.Annotations.CustomValidation.Tests
{
    public class PersonWithCustomValidation
    {
        [Required]
        [MaxLength(8)]
        public String Name { get; set; }

        [Required]
        public String Description { get; set; }

        [Range(18, 67)]
        public UInt16 Age { get; set; }

        [CustomValidation(typeof(PersonWithCustomValidation), "CustomValidationPropertyFailValidator")]
        public String CustomValidationPropertyFail { get; set; }

        public static ValidationResult CustomValidationPropertyFailValidator(String name, ValidationContext validationContext)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult(
                    String.Format("CustomValidationPropertyFailValidator-ErrorMessage: '{0}'", name)
                    ,
                    new List<String>() { validationContext.MemberName }
                    );
            }
            return ValidationResult.Success;
        }

        [CustomValidation(typeof(PersonWithCustomValidation), "CustomValidationPropertyPassValidator")]
        public String CustomValidationPropertyPass { get; set; }

        public static ValidationResult CustomValidationPropertyPassValidator(String name, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }
}
