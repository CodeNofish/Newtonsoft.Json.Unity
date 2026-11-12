#if HAVE_MODULE_PHYSICS
using NUnit.Framework;
using UnityEngine;

namespace Newtonsoft.Json.Unity.Tests {

  public class UnityPhysicsConverterTests : ConverterTestBase {

    [Test]
    public void JointDriveConverter() {
      var value = new JointDrive { positionSpring = 12.5f, positionDamper = 3.25f, maximumForce = 100f };

      AssertConverter(value, (expected, actual) => {
        Assert.AreEqual(expected.positionSpring, actual.positionSpring, 0.00001f);
        Assert.AreEqual(expected.positionDamper, actual.positionDamper, 0.00001f);
        Assert.AreEqual(expected.maximumForce, actual.maximumForce, 0.00001f);
      }, "positionSpring", "positionDamper", "maximumForce");
    }

    [Test]
    public void JointLimitsConverter() {
      var value = new JointLimits {
        min = -45f,
        max = 60f,
        bounciness = 0.25f,
        bounceMinVelocity = 1.5f,
        contactDistance = 0.125f,
      };

      AssertConverter(value, (expected, actual) => {
        Assert.AreEqual(expected.min, actual.min, 0.00001f);
        Assert.AreEqual(expected.max, actual.max, 0.00001f);
        Assert.AreEqual(expected.bounciness, actual.bounciness, 0.00001f);
        Assert.AreEqual(expected.bounceMinVelocity, actual.bounceMinVelocity, 0.00001f);
        Assert.AreEqual(expected.contactDistance, actual.contactDistance, 0.00001f);
      }, "min", "max", "bounciness", "bounceMinVelocity", "contactDistance");
    }

    [Test]
    public void SoftJointLimitConverter() {
      var value = new SoftJointLimit { limit = -30f, bounciness = 0.5f, contactDistance = 0.75f };

      AssertConverter(value, (expected, actual) => {
        Assert.AreEqual(expected.limit, actual.limit, 0.00001f);
        Assert.AreEqual(expected.bounciness, actual.bounciness, 0.00001f);
        Assert.AreEqual(expected.contactDistance, actual.contactDistance, 0.00001f);
      }, "limit", "bounciness", "contactDistance");
    }
  }

}
#endif // HAVE_MODULE_PHYSICS
