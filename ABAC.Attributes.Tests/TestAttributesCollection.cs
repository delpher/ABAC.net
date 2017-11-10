using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ABAC.Attributes.Tests.AttributesForTest;
using FluentAssertions;
using Xunit;

namespace ABAC.Attributes.Tests
{
    public class TestAttributesCollection
    {
        [Fact]
        public void Should_Get_Set_Attribute_Value_By_Expression_Indexer()
        {
            var attrs = new AttributesCollection
            {
                [() => User.Name] = "John"
            };

            attrs[() => User.Name].Should().Be("John");

            ((IReadOnlyDictionary<string, object>) attrs)["User.Name"].Should().Be("John");

            attrs.Count.Should().Be(1);

            attrs.Keys.ToArray().Should().BeEquivalentTo("User.Name");

            attrs.Values.Cast<string>().ToArray().Should().BeEquivalentTo("John");

            attrs.ContainsKey("User.Name").Should().BeTrue();
            attrs.ContainsKey("Document.Revision").Should().BeFalse();

            object value = null;
            attrs.TryGetValue("User.Name", out value).Should().BeTrue();
            ((string) value).Should().Be("John");

            attrs.ToArray()
                .Should().HaveCount(1);

            IEnumerable a = attrs;
            var enumer = a.GetEnumerator();
            enumer.MoveNext().Should().BeTrue();
            enumer.Current.Should().BeOfType<KeyValuePair<string, object>>();
            var kvp = (KeyValuePair<string, object>) enumer.Current;
            kvp.Key.Should().Be("User.Name");
            kvp.Value.Should().Be("John");
        }

        [Fact]
        public void Given_Index_From_Non_Attributes_Class_Throws()
        {
            var attrs = new AttributesCollection();
            Assert.Throws<NotSupportedException>(() => 
                attrs[() => NotAttributes.Group]);
            Assert.Throws<NotSupportedException>(() =>
                attrs[() => NotAttributes.Group] = 123);
        }

        [Fact]
        public void Given_Index_For_Non_String_Type_Should_Work()
        {
            var attrs = new AttributesCollection();
            attrs[() => Document.Revision] = 1;
            attrs[() => Document.Revision].Should().Be(1);
        }

        [Fact]
        public void Given_Non_Member_Access_Expression_Throws()
        {
            var attrs = new AttributesCollection();
            Assert.Throws<NotSupportedException>(() => attrs[() => User.Name + "123"]);
            Assert.Throws<NotSupportedException>(() => attrs[() => User.Name + "123"] = "123");
        }
    }
}
