using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace biz.dfch.CS.Annotations.CustomValidation.Tests
{
    [TestClass]
    public class ValidateListTest
    {
        [Ignore]
        [TestMethod]
        public void ThisTestWillBeMarkedAsIgnored()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void RequiredEmptyListReturnsTrue()
        {
            var list = new ListValidation();
            list.CustomListProperty = new List<string>( new string[] { "arbitrary string" });

            // property under test
            list.RequiredListProperty = new List<string>();

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RequiredOneItemListReturnsTrue()
        {
            var list = new ListValidation();
            list.CustomListProperty = new List<string>(new string[] { "arbitrary string" });

            // property under test
            list.RequiredListProperty = new List<string>();
            list.RequiredListProperty.Add("arbitrary string");

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void RequiredNullListReturnsValidationError()
        {
            var list = new ListValidation();
            list.CustomListProperty = new List<string>(new string[] { "arbitrary string" });

            // property under test
            list.RequiredListProperty = null;

            var validationResults = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResults.Count());
            var validationResult = validationResults[0];
            Assert.AreEqual(1, validationResult.MemberNames.Count());
            var memberName = validationResult.MemberNames.First();
            Assert.AreEqual("RequiredListProperty", memberName);
            StringAssert.Contains(validationResult.ErrorMessage, "RequiredListProperty");
        }

        [TestMethod]
        public void OptionalNullListReturnsTrue()
        {
            var list = new ListValidation();
            list.RequiredListProperty = new List<string>();
            list.CustomListProperty = new List<string>(new string[] { "arbitrary string" });

            // property under test
            list.OptionalListProperty = null;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OptionalEmptyListReturnsTrue()
        {
            var list = new ListValidation();
            list.RequiredListProperty = new List<string>();
            list.CustomListProperty = new List<string>(new string[] { "arbitrary string" });

            // property under test
            list.OptionalListProperty = new List<string>();

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OptionalOneItemListReturnsTrue()
        {
            var list = new ListValidation();
            list.RequiredListProperty = new List<string>();
            list.CustomListProperty = new List<string>(new string[] { "arbitrary string" });

            // property under test
            list.OptionalListProperty = new List<string>();
            list.OptionalListProperty.Add("arbitrary string");

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CustomNullListReturnsValidationError()
        {
            var list = new ListValidation();
            list.RequiredListProperty = new List<string>();

            // property under test
            list.CustomListProperty = null;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CustomEmptyListReturnsValidationError()
        {
            var list = new ListValidation();
            list.RequiredListProperty = new List<string>();

            // property under test
            list.CustomListProperty = new List<string>();

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CustomOneItemListReturnsTrue()
        {
            var list = new ListValidation();
            list.RequiredListProperty = new List<string>();

            // property under test
            list.CustomListProperty = new List<string>();
            list.CustomListProperty.Add("arbitrary string");

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CustomThreeItemListReturnsTrue()
        {
            var list = new ListValidation();
            list.RequiredListProperty = new List<string>();

            // property under test
            list.CustomListProperty = new List<string>();
            list.CustomListProperty.Add("1 - arbitrary string");
            list.CustomListProperty.Add("2 - arbitrary string");
            list.CustomListProperty.Add("3 - arbitrary string");

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(list);
            var result = false;
            result = Validator.TryValidateObject(list, validationContext, validationResults, true);
            Assert.IsTrue(result);
        }
    }
}
