using System;
using System.Collections.Generic;
using ABAC.Attributes;
using ABAC.Attributes.Tests.AttributesForTest;
using FluentAssertions;
using Xunit;

namespace ABAC.Conditions.Tests
{
    public class TestConditionEvaluation
    {
        [Fact]
        public void Should_Evaluate_With_Given_Attribute_Values()
        {
            var attributes = new AttributesCollection();
            var condition = new Condition(() => Document.Revision == 1);

            attributes[() => Document.Revision] = 0;
            condition.Evaluate(attributes).Should().BeFalse();

            attributes[() => Document.Revision] = 1;
            condition.Evaluate(attributes).Should().BeTrue();

            condition = new Condition(() => 1 <= Document.Revision);

            attributes[() => Document.Revision] = 0;
            condition.Evaluate(attributes).Should().BeFalse();

            attributes[() => Document.Revision] = 1;
            condition.Evaluate(attributes).Should().BeTrue();

            attributes[() => Document.Revision] = 2;
            condition.Evaluate(attributes).Should().BeTrue();

            condition = new Condition(() => User.IsBlocked);

            attributes[() => User.IsBlocked] = true;
            condition.Evaluate(attributes).Should().BeTrue();

            attributes[() => User.IsBlocked] = false;
            condition.Evaluate(attributes).Should().BeFalse();
        }

        [Fact]
        public void Should_Evaluate_Complex_Conditions()
        {
            var attributes = new AttributesCollection
            {
                [() => User.Name] = "John",
                [() => User.IsBlocked] = false,
                [() => Document.Revision] = 1
            };

            var condition = new Condition(() => 
                User.Name == "John" &&
                !User.IsBlocked && 
                Document.Revision > 0);

            condition.Evaluate(attributes).Should().BeTrue();
        }

        [Fact]
        public void Should_Throw_Not_Supported_Exception_If_Given_Members_From_Not_Atrributes_Class()
        {
            var values = new Dictionary<string, object>
            {
                {"NotAttributes.Group", "Some"}
            };
            var condition = new Condition(() => NotAttributes.Group == "Some");
            Assert.Throws<NotSupportedException>(() => condition.Evaluate(values));
        }

        [Fact]
        public void When_Attribute_Not_Found_Should_Throw_AttributeMissingException()
        {
            var values = new AttributesCollection();
            var condition = new Condition(() => User.Name == "John");
            Assert.Throws<AttributeMissingException>(() => condition.Evaluate(values));
        }
    }
}
