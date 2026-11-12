using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Newtonsoft.Json.Unity.Tests {

  public class UnityConverterRegistryTests {

    private sealed class DummyConverter : JsonConverter {
      public override bool CanConvert(Type objectType) => objectType == typeof(DummyConverter);

      public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
        return new DummyConverter();
      }

      public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
        writer.WriteNull();
      }
    }

    [SetUp]
    public void SetUp() => UnityConverterRegistry.Reset();

    [TearDown]
    public void TearDown() => UnityConverterRegistry.Reset();

    [Test]
    public void ConverterTypes_ContainsEveryConverterCompiledIntoAssembly() {
      var expected = typeof(UnityConverterRegistry).Assembly.GetTypes()
        .Where(type => !type.IsAbstract && typeof(JsonConverter).IsAssignableFrom(type))
        .ToArray();
      var actual = UnityConverterRegistry.ConverterTypes;

      CollectionAssert.AreEquivalent(expected, actual);
      CollectionAssert.AllItemsAreUnique(actual);
    }

    [Test]
    public void CreateConverters_ReturnsOneInstanceOfEveryRegisteredType() {
      var types = UnityConverterRegistry.ConverterTypes;
      var converters = UnityConverterRegistry.CreateConverters();

      Assert.AreEqual(types.Count, converters.Length);
      for (var i = 0; i < types.Count; i++) Assert.AreEqual(types[i], converters[i].GetType());
    }

    [Test]
    public void Register_Type_AddsCustomConverter() {
      UnityConverterRegistry.Register<DummyConverter>();

      CollectionAssert.Contains(UnityConverterRegistry.ConverterTypes, typeof(DummyConverter));
      Assert.AreEqual(1, UnityConverterRegistry.CreateConverters().Count(converter => converter is DummyConverter));
    }

    [Test]
    public void Register_Instance_IsReturnedByCreateConverters() {
      var instance = new DummyConverter();
      UnityConverterRegistry.Register(instance);

      Assert.AreSame(instance, UnityConverterRegistry.CreateConverters().Single(converter => converter is DummyConverter));
    }

    [Test]
    public void Register_ReplacesExistingTypeWithoutChangingCount() {
      var count = UnityConverterRegistry.ConverterTypes.Count;
      var instance = new Vector2Converter();
      UnityConverterRegistry.Register(instance);

      Assert.AreEqual(count, UnityConverterRegistry.ConverterTypes.Count);
      Assert.AreSame(instance, UnityConverterRegistry.CreateConverters().Single(converter => converter.GetType() == typeof(Vector2Converter)));
    }

    [Test]
    public void Unregister_RemovesConverter() {
      Assert.IsTrue(UnityConverterRegistry.Unregister<Vector2Converter>());
      CollectionAssert.DoesNotContain(UnityConverterRegistry.ConverterTypes, typeof(Vector2Converter));
      Assert.IsFalse(UnityConverterRegistry.Unregister<Vector2Converter>());
    }

    [Test]
    public void Reset_RestoresBuiltInConverters() {
      UnityConverterRegistry.Unregister<Vector2Converter>();
      UnityConverterRegistry.Register<DummyConverter>();
      UnityConverterRegistry.Reset();

      CollectionAssert.Contains(UnityConverterRegistry.ConverterTypes, typeof(Vector2Converter));
      CollectionAssert.DoesNotContain(UnityConverterRegistry.ConverterTypes, typeof(DummyConverter));
    }

    [Test]
    public void CreateSettings_ProvidesAllConvertersAndStaysMutable() {
      var settings = UnityConverterRegistry.CreateSettings();

      Assert.AreEqual(UnityConverterRegistry.ConverterTypes.Count, settings.Converters.Count);
      Assert.DoesNotThrow(() => settings.Converters.Add(new Vector2Converter()));
    }

    [Test]
    public void ApplyTo_AddsConvertersToExistingSettings() {
      var settings = new JsonSerializerSettings();
      settings.Converters.Add(new Vector2Converter());
      var existingCount = settings.Converters.Count;

      Assert.AreSame(settings, UnityConverterRegistry.ApplyTo(settings));
      Assert.AreEqual(existingCount + UnityConverterRegistry.ConverterTypes.Count, settings.Converters.Count);
    }

    [Test]
    public void Settings_SerializeUnityTypesWithRegistryConverters() {
      var settings = UnityConverterRegistry.CreateSettings();

      Assert.AreEqual("{\"x\":1.5,\"y\":-2.25}", JsonConvert.SerializeObject(new Vector2(1.5f, -2.25f), settings));
    }
  }

}
