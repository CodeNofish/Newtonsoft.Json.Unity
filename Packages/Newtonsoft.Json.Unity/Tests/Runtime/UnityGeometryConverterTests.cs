using NUnit.Framework;
using UnityEngine;

namespace Newtonsoft.Json.Unity.Tests {

  public class UnityGeometryConverterTests : ConverterTestBase {

    [Test]
    public void BoundsConverter() =>
      AssertConverter(new Bounds(new Vector3(1, 2, 3), new Vector3(4, 5, 6)), "center", "size");

    [Test]
    public void BoundsIntConverter() =>
      AssertConverter(new BoundsInt(new Vector3Int(1, 2, 3), new Vector3Int(4, 5, 6)), "position", "size");

    [Test]
    public void PlaneConverter() =>
      AssertConverter(new Plane(new Vector3(0, 1, 0), 2.5f), "normal", "distance");

    [Test]
    public void RectConverter() =>
      AssertConverter(new Rect(1.5f, -2.25f, 3.5f, 4.75f), "x", "y", "width", "height");

    [Test]
    public void RectIntConverter() =>
      AssertConverter(new RectInt(-3, 7, 11, 13), "x", "y", "width", "height");

    [Test]
    public void RectOffsetConverter() {
      var value = new RectOffset(1, 2, 3, 4);

      AssertConverter(value, (expected, actual) => {
        Assert.AreEqual(expected.left, actual.left);
        Assert.AreEqual(expected.right, actual.right);
        Assert.AreEqual(expected.top, actual.top);
        Assert.AreEqual(expected.bottom, actual.bottom);
      }, "left", "right", "top", "bottom");
    }
  }

}
