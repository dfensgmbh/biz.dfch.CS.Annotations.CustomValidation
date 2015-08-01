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
using System.Globalization;
using System.Linq;

namespace biz.dfch.CS.Annotations.CustomValidation.Tests
{
    public class CustomValidationStubImplementation
    {
        public static ValidationResult AnnotationWithValidationInStubImplValidator(String name, ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(name) || !name.Equals("en", StringComparison.InvariantCultureIgnoreCase))
            {
                return new ValidationResult(
                    String.Format("CustomValidationPropertyFailValidator-ErrorMessage: '{0}'. Property must be set to 'en'.", name)
                    ,
                    new List<String>() { validationContext.MemberName }
                    );
            }
            return ValidationResult.Success;
        }

        public static ValidationResult Iso3166Validator(string name, ValidationContext validationContext)
        {
            var culture = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(c => c.TwoLetterISOLanguageName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .Select(c => c.TwoLetterISOLanguageName)
                .FirstOrDefault();
            if (null == culture)
            {
                return new ValidationResult(
                    String.Format("Iso3166Validator FAILED: '{0}' is not an ISO3166 country code.", name)
                    ,
                    new List<String>() { validationContext.MemberName }
                    );  
            }
            return ValidationResult.Success;
        }
    }
}
