using System;
using System.Collections.Generic;
using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSky.Common.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        private Random _random;
        private List<string> InvalidStrings = new List<string> { null, string.Empty, " ", "     " };

        [TestInitialize]
        public void TestInitialise()
        {
            _random = new Random();
        }

        [TestMethod]
        public void ToDateTime_StringIsNullEmptyOrWhitespaceOrNotDateTime_ReturnsDateTimeMinValue()
        {
            // Arrange
            var expected = DateTime.MinValue;
            new List<string>{ null, string.Empty, " ", "   ", "notDate", "14/14/1990", "01-01-"}
                .ForEach(input =>
                {
                    // Act
                    var actual = input.ToDateTime();

                    // Assert
                    actual.Should().Be(expected);
                });
        }

        [TestMethod]
        public void ToDateTime_StringIsProperDateTime_ReturnsDateTime()
        {
            // Arrange
            var expected = DateTime.Today.AddDays(4);
            var input = expected.ToString("D");

            // Act
            var actual = input.ToDateTime();

            // Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToDouble_InputNullEmptyOrWhiteSpace_ReturnsDefaultDouble()
        {
            // Arrange
            var expected = StringExtensions.DefaultDouble;

            InvalidStrings
                .ForEach(input =>
                {
                    // Act
                    var actual = input.ToDouble();

                    // Assert
                    actual.Should().Be(expected);
                });
        }

        [TestMethod]
        public void ToDouble_StringIsNotValidDouble_NoDefaultProvided_ReturnsZero()
        {
            // Arrange
            var expected = StringExtensions.DefaultDouble;
            var input = "zzzz";

            // Act
            var actual = input.ToDouble();

            // Assert
            actual.Should().Be(expected);
        } 

        [TestMethod]
        public void ToDouble_StringIsNotValidDouble_DefaultValueProvided_ReturnsDefault()
        {
            // Arrange
            double expected = _random.NextDouble();
            var input = "zzzz";

            // Act
            var actual = input.ToDouble(expected);

            // Assert
            actual.Should().Be(expected);
        } 

        [TestMethod]
        public void ToDouble_StringValidDouble_DoubleValueReturned()
        {
            // Arrange
            double expected = _random.NextDouble();
            var input = expected.ToString();

            // Act
            var actual = input.ToDouble();

            // Assert
            Assert.AreEqual(expected, actual, 0.00000001);
        }

        [TestMethod]
        public void ToEnum_StringNullEmptyOrWhiteSpace_ThrowsException()
        {
            // Arrange
            InvalidStrings.ForEach(input =>
            {
                // Act
                var actual = MsTestExtensions.AssertRaises<ArgumentNullException>(() => input.ToEnum<SampleEnum>());

                // Assert
                actual.Message.Should().Contain(StringExtensions.MessageValidStringNeededForParsingEnum);
            });
        }

        [TestMethod]
        public void ToEnum_TypeToConvertToNotEnum_ThrowsException()
        {
            // Arrange
            var input = "Un";

            // Act
            var actual = MsTestExtensions.AssertRaises<ArgumentException>(() => input.ToEnum<SampleStruct>());

            // Assert
            actual.Message.Should()
                .Contain(StringExtensions.FormatMessageEnumTypeNeeded.FormatWith(typeof (SampleStruct)));
        }

        [TestMethod]
        public void ToEnum_StringNotProperEnumValue_ThrowsException()
        {
            // Arrange
            var input = "Cinco";

            // Act
            var actual = MsTestExtensions.AssertRaises<ArgumentException>(() => input.ToEnum<SampleEnum>());

            // Assert
            actual.Message.Should()
                .Contain(StringExtensions.FormatMessageValueNotProperEnum.FormatWith(input, typeof (SampleEnum)));
        }

        [TestMethod]
        public void ToEnum_StringCaseNotForProperEnumValue_IgnoreCaseSetToFalse_ThrowsException()
        {
            // Arrange
            var input = SampleEnum.Tres.ToString().ToLower();

            // Act
            var actual =
                MsTestExtensions.AssertRaises<ArgumentException>(() => input.ToEnum<SampleEnum>(ignorecase: false));

            // Assert
            actual.Message.Should()
                .Contain(StringExtensions.FormatMessageValueNotProperEnum.FormatWith(input, typeof (SampleEnum)));
        }

        [TestMethod]
        public void ToEnum_ValidEnumTypeAndValue_ReturnsConvertedEnum()
        {
            // Arrange
            var expected = SampleEnum.Dos;
            var input = expected.ToString();

            // Act
            var actual = input.ToEnum<SampleEnum>();

            // Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToInt_InputNullEmptyOrWhiteSpace_ReturnsDefaultInt()
        {
            // Arrange
            var expected = StringExtensions.DefaultInt;

            InvalidStrings.ForEach(input =>
                {
                    // Act
                    var actual = input.ToInt();

                    // Assert
                    actual.Should().Be(expected);
                });
        }

        
        [TestMethod]
        public void ToInt_StringIsNotValidInt_NoDefaultProvided_ReturnsZero()
        {
            // Arrange
            var expected = StringExtensions.DefaultInt;
            var input = "zzzz";

            // Act
            var actual = input.ToInt();

            // Assert
            actual.Should().Be(expected);
        } 

        [TestMethod]
        public void ToInt_StringIsNotValidInt_DefaultValueProvided_ReturnsDefault()
        {
            // Arrange
            var expected = _random.Next();
            var input = "zzzz";

            // Act
            var actual = input.ToInt(expected);

            // Assert
            actual.Should().Be(expected);
        } 

        [TestMethod]
        public void ToInt_StringValidInt_IntValueReturned()
        {
            // Arrange
            var expected = _random.Next();
            var input = expected.ToString();

            // Act
            var actual = input.ToInt();

            // Assert
            actual.Should().Be(expected);
        }
    }

    public struct SampleStruct
    {
    }

    public enum SampleEnum
    {
        Un,
        Dos,
        Tres,
        Cuatro
    }
}