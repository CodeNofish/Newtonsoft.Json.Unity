#if HAVE_MODULE_PHYSICS2D
using NUnit.Framework;
using UnityEngine;

namespace Newtonsoft.Json.Unity.Tests {

  public class UnityPhysics2DConverterTests : ConverterTestBase {

    [Test]
    public void ColliderDistance2DConverter() {
      var value = new ColliderDistance2D {
        pointA = new Vector2(1.5f, -2.25f),
        pointB = new Vector2(3.5f, 4.75f),
        distance = 2.5f,
        isValid = true,
      };

      AssertConverter(value, (expected, actual) => {
        Assert.AreEqual(expected.pointA, actual.pointA);
        Assert.AreEqual(expected.pointB, actual.pointB);
        Assert.AreEqual(expected.normal, actual.normal);
        Assert.AreEqual(expected.distance, actual.distance, 0.00001f);
        Assert.AreEqual(expected.isValid, actual.isValid);
      }, "pointA", "pointB", "normal", "distance", "isValid");
    }

    [Test]
    public void ContactFilter2DConverter() {
      var value = new ContactFilter2D {
        useTriggers = true,
        useLayerMask = true,
        useDepth = true,
        useOutsideDepth = false,
        useNormalAngle = true,
        useOutsideNormalAngle = false,
        layerMask = new LayerMask { value = 1 << 3 },
        minDepth = -1.5f,
        maxDepth = 2.5f,
        minNormalAngle = 10f,
        maxNormalAngle = 80f,
      };

      AssertConverter(value, (expected, actual) => {
        Assert.AreEqual(expected.useTriggers, actual.useTriggers);
        Assert.AreEqual(expected.useLayerMask, actual.useLayerMask);
        Assert.AreEqual(expected.useDepth, actual.useDepth);
        Assert.AreEqual(expected.useOutsideDepth, actual.useOutsideDepth);
        Assert.AreEqual(expected.useNormalAngle, actual.useNormalAngle);
        Assert.AreEqual(expected.useOutsideNormalAngle, actual.useOutsideNormalAngle);
        Assert.AreEqual(expected.layerMask.value, actual.layerMask.value);
        Assert.AreEqual(expected.minDepth, actual.minDepth, 0.00001f);
        Assert.AreEqual(expected.maxDepth, actual.maxDepth, 0.00001f);
        Assert.AreEqual(expected.minNormalAngle, actual.minNormalAngle, 0.00001f);
        Assert.AreEqual(expected.maxNormalAngle, actual.maxNormalAngle, 0.00001f);
      }, "useTriggers", "useLayerMask", "useDepth", "useOutsideDepth", "useNormalAngle",
         "useOutsideNormalAngle", "layerMask", "minDepth", "maxDepth", "minNormalAngle", "maxNormalAngle");
    }
  }

}
#endif // HAVE_MODULE_PHYSICS2D
