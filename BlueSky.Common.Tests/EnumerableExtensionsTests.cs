using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSky.Common.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void ForEach_ListWithAction_ActionPerformedOnEachItemOfList()
        {
            // Arrange
            var toAdd = new Random().Next().ToString();
            var list = new List<string> { "some", "random", "string", "items", "for", "test" };
            var enumerable = list.AsEnumerable();

            // Act
            var final = new List<string>();
            enumerable.ForEach(x => final.Add(x + toAdd));

            // Assert
            list.ForEach(x => final.Should().Contain(x + toAdd));
        }
    }
}