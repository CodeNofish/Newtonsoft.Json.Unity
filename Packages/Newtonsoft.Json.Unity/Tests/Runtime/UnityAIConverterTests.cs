#if HAVE_MODULE_AI
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

namespace Newtonsoft.Json.Unity.Tests {

  public class UnityAIConverterTests : ConverterTestBase {

    [Test]
    public void NavMeshQueryFilterConverter() {
      var value = new NavMeshQueryFilter { areaMask = 7, agentTypeID = 2 };
      value.SetAreaCost(0, 2.5f);

      AssertConverter(value, (expected, actual) => {
        Assert.AreEqual(expected.areaMask, actual.areaMask);
        Assert.AreEqual(expected.agentTypeID, actual.agentTypeID);
        Assert.AreEqual(expected.GetAreaCost(0), actual.GetAreaCost(0), 0.00001f);
      }, "costs", "areaMask", "agentTypeId");

      Assert.AreEqual(32, ((JArray)ObjectOf(value)["costs"]!).Count);
    }

    [Test]
    public void NavMeshTriangulationConverter() {
      var value = new NavMeshTriangulation {
        vertices = new[] { new Vector3(1, 2, 3), new Vector3(4, 5, 6) },
        indices = new[] { 0, 1, 2 },
        areas = new[] { 0, 1 },
      };

      AssertConverter(value, (expected, actual) => {
        CollectionAssert.AreEqual(expected.vertices, actual.vertices);
        CollectionAssert.AreEqual(expected.indices, actual.indices);
        CollectionAssert.AreEqual(expected.areas, actual.areas);
      }, "vertices", "indices", "areas");
    }
  }

}
#endif // HAVE_MODULE_AI
