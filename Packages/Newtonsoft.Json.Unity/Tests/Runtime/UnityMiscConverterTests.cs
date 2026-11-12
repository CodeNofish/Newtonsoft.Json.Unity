using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;

namespace Newtonsoft.Json.Unity.Tests {

  public class UnityMiscConverterTests : ConverterTestBase {

    [Test]
    public void CullingGroupEventConverter() {
      AssertConverter(new CullingGroupEvent(), (expected, actual) => {
        Assert.AreEqual(expected.index, actual.index);
        Assert.AreEqual(expected.isVisible, actual.isVisible);
        Assert.AreEqual(expected.wasVisible, actual.wasVisible);
        Assert.AreEqual(expected.currentDistance, actual.currentDistance);
        Assert.AreEqual(expected.previousDistance, actual.previousDistance);
      }, "index", "isVisible", "wasVisible", "currentDistance", "previousDistance");
    }

    [Test]
    public void ResolutionConverter() {
      var value = new Resolution { width = 1920, height = 1080 };

      AssertConverter(value, (expected, actual) => {
        Assert.AreEqual(expected.width, actual.width);
        Assert.AreEqual(expected.height, actual.height);
      }, "width", "height", "refreshRateRatio");
    }

    [Test]
    public void Hash128Converter() {
      var value = Hash128.Parse("0123456789abcdef0123456789abcdef");
      var json = ToJson(value);

      Assert.AreEqual(value.ToString(), JToken.Parse(json).ToObject<string>());
      Assert.AreEqual(value, FromJson<Hash128>(json));

      Hash128? nullable = null;
      Assert.AreEqual("null", ToJson(nullable));
      Assert.IsNull(FromJson<Hash128?>("null"));
    }

    [Test]
    public void LayerMaskConverter() {
      var value = new LayerMask { value = 1 << 5 };
      var json = ToJson(value);

      Assert.AreEqual("32", json);
      Assert.AreEqual(value.value, FromJson<LayerMask>(json).value);

      LayerMask? nullable = null;
      Assert.AreEqual("null", ToJson(nullable));
      Assert.IsNull(FromJson<LayerMask?>("null"));
    }

    [Test]
    public void RangeIntConverter() => AssertConverter(new RangeInt(3, 7), "start", "length");

    [Test]
    public void RandomStateConverter() =>
      AssertConverter(UnityEngine.Random.state,
        (expected, actual) => Assert.AreEqual(ToJson(expected), ToJson(actual)),
        "s0", "s1", "s2", "s3");

    [Test]
    public void NativeArrayConverter() {
      var converter = new NativeArrayConverter();
      Assert.IsTrue(converter.CanConvert(typeof(NativeArray<int>)));
      Assert.IsTrue(converter.CanConvert(typeof(NativeSlice<int>)));
      Assert.IsFalse(converter.CanConvert(typeof(int[])));

      using (var array = new NativeArray<int>(new[] { 1, 2, 3 }, Allocator.Temp)) {
        Assert.AreEqual("[1,2,3]", ToJson(array));
      }

      Assert.Throws<JsonSerializationException>(() => FromJson<NativeArray<int>>("[1,2,3]"));
    }
  }

}
