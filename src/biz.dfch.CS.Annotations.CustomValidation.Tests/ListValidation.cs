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

namespace biz.dfch.CS.Annotations.CustomValidation.Tests
{
    public class ListValidation
    {
        [Required]
        public List<string> RequiredListProperty { get; set; }

        public List<string> OptionalListProperty { get; set; }

        [CustomValidation(typeof(ListValidation), "CustomListPropertyValidator")]
        public List<string> CustomListProperty { get; set; }

        public static ValidationResult CustomListPropertyValidator(List<string> list, ValidationContext validationContext)
        {
            if(null == list || 0 >= list.Count)
            {
                return new ValidationResult(
                    String.Format("{0}: CustomListPropertyValidator FAILED. List must contain at least 1 element.", validationContext.MemberName)
                    ,
                    new List<string>() { validationContext.MemberName }
                    );  
            }
            return ValidationResult.Success;
        }
    }
}
