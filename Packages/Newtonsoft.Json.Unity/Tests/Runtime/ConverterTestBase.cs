using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Newtonsoft.Json.Unity.Tests {

  public abstract class ConverterTestBase {
    protected static JsonSerializerSettings Settings { get; } = UnityConverterRegistry.CreateSettings();

    [SetUp]
    public void ResetRegistryBeforeEachTest() => UnityConverterRegistry.Reset();

    protected static string ToJson<T>(T value) {
      return JsonConvert.SerializeObject(value, Settings);
    }

    protected static T FromJson<T>(string json) {
      return JsonConvert.DeserializeObject<T>(json, Settings)!;
    }

    protected static JObject ObjectOf<T>(T value) {
      return JObject.Parse(ToJson(value));
    }

    /// <summary>Asserts the serialized property names and that the value survives a round trip.</summary>
    protected static void AssertConverter<T>(T value, params string[] propertyNames) {
      AssertConverter(value, (expected, actual) => Assert.AreEqual(expected, actual), propertyNames);
    }

    /// <summary>Asserts the serialized property names and compares the round-tripped value.</summary>
    protected static void AssertConverter<T>(T value, Action<T, T> assertEqual, params string[] propertyNames) {
      var json = ObjectOf(value);
      CollectionAssert.AreEquivalent(propertyNames, PropertyNames(json), $"Unexpected JSON properties for {typeof(T).Name}");
      assertEqual(value, FromJson<T>(json.ToString(Formatting.None)));
    }

    private static List<string> PropertyNames(JObject json) {
      var names = new List<string>();
      foreach (var property in json.Properties()) names.Add(property.Name);
      return names;
    }
  }

}
