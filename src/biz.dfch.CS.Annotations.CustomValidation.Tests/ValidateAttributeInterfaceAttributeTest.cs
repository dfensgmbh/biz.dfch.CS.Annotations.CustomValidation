using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation;

namespace biz.dfch.CS.Annotations.CustomValidation.Tests
{
    [TestClass]
    public class ValidateAttributeInterfaceAttributeTest
    {
        //[TestMethod]
        //public void ThisTestWillBeMarkedAsIgnored()
        //{
        //    Assert.Inconclusive();
        //}

        [TestMethod]
        public void UsingPersonWorksAsExpected()
        {
            var person = new Person();
            person.Age = 42;
            person.Name = "Edgar";

            Assert.AreEqual(42, person.Age);
            Assert.AreEqual("Edgar", person.Name);
            Assert.AreEqual(null, person.Description);
        }

        [TestMethod]
        public void UsingPersonWithDataAnnotationWorksAsExpected()
        {
            var person = new PersonWithDataAnnotation();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsTrue(result); 
            
            Assert.AreEqual(42, person.Age);
            Assert.AreEqual("Edgar", person.Name);
            Assert.AreEqual("some description", person.Description);            
        }
        [TestMethod]
        public void ValidatingNameOnPersonWithDataAnnotationReturnsValidationError()
        {
            var person = new PersonWithDataAnnotation();
            person.Name = "Edgar.Schnittenfittich";
            person.Description = "some description";
            person.Age = 42;

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResults.Count());
            var validationResult = validationResults[0];
            Assert.AreEqual(1, validationResult.MemberNames.Count());
            var memberName = validationResult.MemberNames.First();
            Assert.AreEqual("Name", memberName);
        }
        [TestMethod]
        public void ValidatingDescriptionOnPersonWithDataAnnotationReturnsValidationError()
        {
            var person = new PersonWithDataAnnotation();
            person.Name = "Edgar";
            person.Age = 42;

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResults.Count());
            var validationResult = validationResults[0];
            Assert.AreEqual(1, validationResult.MemberNames.Count());
            var memberName = validationResult.MemberNames.First();
            Assert.AreEqual("Description", memberName);
        }
        [TestMethod]
        public void ValidatingAgeOnPersonWithDataAnnotationReturnsValidationError()
        {
            var person = new PersonWithDataAnnotation();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 111;

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResults.Count());
            var validationResult = validationResults[0];
            Assert.AreEqual(1, validationResult.MemberNames.Count());
            var memberName = validationResult.MemberNames.First();
            Assert.AreEqual("Age", memberName);
        }
        [TestMethod]
        public void ValidatingCustomAnnotationOnPersonWithCustomAnnotationReturnsValidationError()
        {
            var person = new PersonWithCustomAnnotation();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;

            // property under test
            person.CustomAnnotationProperty = "tralala";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidatingCustomValidationPropertyFailOnPersonWithCustomValidationReturnsValidationError()
        {
            var person = new PersonWithCustomValidation();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;

            // property under test
            person.CustomValidationPropertyFail = "some contents that will fail";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResults.Count());
            var validationResult = validationResults[0];
            Assert.AreEqual(1, validationResult.MemberNames.Count());
            var memberName = validationResult.MemberNames.First();
            Assert.AreEqual("CustomValidationPropertyFail", memberName);
        }
        [TestMethod]
        public void ValidatingCustomValidationPropertyPassOnPersonWithCustomValidationReturnsTrue()
        {
            var person = new PersonWithCustomValidation();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;

            // property under test
            person.CustomValidationPropertyPass = "some contents that will pass";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void ValidatingAnnotationWithValidationInBaseClassOnPersonWithCustomValidationInBaseThrowsInvalidOperationException()
        {
            var person = new PersonWithCustomValidationInBase();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;

            // property under test
            person.AnnotationWithValidationInBaseClass = "some contents that will fail";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
        }
        [TestMethod]
        public void AnnotationWithValidationStubValidatorOnPersonWithCustomValidationInStubReturnsTrue()
        {
            var person = new PersonWithCustomValidationInStub();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;
            person.AnnotationWithValidationExternal = "en-us";

            // property under test
            person.AnnotationWithValidationStub = "en-us";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void AnnotationWithValidationStubValidatorOnPersonWithCustomValidationInStubReturnsValidationError()
        {
            var person = new PersonWithCustomValidationInStub();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;
            person.AnnotationWithValidationExternal = "en-us";

            // property under test
            person.AnnotationWithValidationStub = "non-iso3166-country-code-that-will-fail";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResults.Count());
            var validationResult = validationResults[0];
            Assert.AreEqual(1, validationResult.MemberNames.Count());
            var memberName = validationResult.MemberNames.First();
            Assert.AreEqual("AnnotationWithValidationStub", memberName);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("en-us"));
        }
        [TestMethod]
        public void AnnotationWithValidationExternalValidatorOnPersonWithCustomValidationInStubReturnsTrue()
        {
            var person = new PersonWithCustomValidationInStub();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;
            person.AnnotationWithValidationStub = "en-us";

            // property under test
            person.AnnotationWithValidationExternal = "en-us";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void AnnotationWithValidationExternalValidatorOnPersonWithCustomValidationInStubReturnsValidationError()
        {
            var person = new PersonWithCustomValidationInStub();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;
            person.AnnotationWithValidationStub = "en-us";

            // property under test
            person.AnnotationWithValidationExternal = "non-iso3166-country-code-that-will-fail";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResults.Count());
            var validationResult = validationResults[0];
            Assert.AreEqual(1, validationResult.MemberNames.Count());
            var memberName = validationResult.MemberNames.First();
            Assert.AreEqual("AnnotationWithValidationExternal", memberName);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("en-us"));
        }
        [TestMethod]
        public void ValidatingFluentValidationOnPersonWithFluentValidationReturnsValidationError()
        {
            var person = new PersonWithFluentValidation();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;
            person.CustomValidationPropertyFail = "some contents that will fail";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResults.Count());
            var validationResult = validationResults[0];
            Assert.AreEqual(1, validationResult.MemberNames.Count());
            var memberName = validationResult.MemberNames.First();
            Assert.AreEqual("CustomValidationPropertyFail", memberName);

            //DFTODO - implement test
            Assert.Inconclusive();
        }
        [TestMethod]
        public void ValidatingFluentValidationOnPersonWithFluentValidationReturnsOk()
        {
            var person = new PersonWithFluentValidation();
            person.Name = "Edgar";
            person.Description = "some description";
            person.Age = 42;
            person.CustomValidationPropertyPass = "some contents that will pass";

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(person);
            var result = false;
            result = Validator.TryValidateObject(person, validationContext, validationResults, true);
            Assert.IsTrue(result);

            //DFTODO - implement test
            Assert.Inconclusive();
        }
    }
}
