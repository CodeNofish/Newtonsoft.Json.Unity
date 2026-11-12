#if HAVE_MODULE_MATHEMATICS
using NUnit.Framework;
using Unity.Mathematics;
using UnityMath = Newtonsoft.Json.Unity.Mathematics;

namespace Newtonsoft.Json.Unity.Tests {

  public class UnityMathematicsConverterTests : ConverterTestBase {

    [Test]
    public void Bool2Converter() => AssertConverter(new bool2(true, false), "x", "y");

    [Test]
    public void Bool3Converter() => AssertConverter(new bool3(true, false, true), "x", "y", "z");

    [Test]
    public void Bool4Converter() => AssertConverter(new bool4(true, false, true, false), "x", "y", "z", "w");

    [Test]
    public void Double2Converter() => AssertConverter(new double2(1.25, -2.5), "x", "y");

    [Test]
    public void Double3Converter() => AssertConverter(new double3(1.25, -2.5, 3.75), "x", "y", "z");

    [Test]
    public void Double4Converter() => AssertConverter(new double4(1.25, -2.5, 3.75, -4.125), "x", "y", "z", "w");

    [Test]
    public void Float2Converter() => AssertConverter(new float2(1.5f, -2.25f), "x", "y");

    [Test]
    public void Float3Converter() => AssertConverter(new float3(1.5f, -2.25f, 3.75f), "x", "y", "z");

    [Test]
    public void Float4Converter() => AssertConverter(new float4(1.5f, -2.25f, 3.75f, -4.5f), "x", "y", "z", "w");

    [Test]
    public void Half2Converter() =>
      AssertConverter(new half2(new half(1.5f), new half(-2.25f)), "x", "y");

    [Test]
    public void Half3Converter() =>
      AssertConverter(new half3(new half(1.5f), new half(-2.25f), new half(3.75f)), "x", "y", "z");

    [Test]
    public void Half4Converter() =>
      AssertConverter(new half4(new half(1.5f), new half(-2.25f), new half(3.75f), new half(-4.5f)), "x", "y", "z", "w");

    [Test]
    public void Int2Converter() => AssertConverter(new int2(-3, 7), "x", "y");

    [Test]
    public void Int3Converter() => AssertConverter(new int3(-3, 7, 11), "x", "y", "z");

    [Test]
    public void Int4Converter() => AssertConverter(new int4(-3, 7, 11, -13), "x", "y", "z", "w");

    [Test]
    public void Uint2Converter() => AssertConverter(new uint2(3u, 7u), "x", "y");

    [Test]
    public void Uint3Converter() => AssertConverter(new uint3(3u, 7u, 11u), "x", "y", "z");

    [Test]
    public void Uint4Converter() => AssertConverter(new uint4(3u, 7u, 11u, 13u), "x", "y", "z", "w");

    [Test]
    public void QuaternionConverter() =>
      AssertConverter(new quaternion(0.1f, 0.2f, 0.3f, 0.9f), "x", "y", "z", "w");

    [Test]
    public void HalfConverter() {
      // The scalar half converter writes a bare value inside the JSON object instead of a
      // named property, so only its type matching is covered until that implementation is fixed.
      var converter = new UnityMath.HalfConverter();

      Assert.IsTrue(converter.CanConvert(typeof(half)));
      Assert.IsTrue(converter.CanConvert(typeof(half?)));
      Assert.IsFalse(converter.CanConvert(typeof(float2)));
    }
  }

}
#endif // HAVE_MODULE_MATHEMATICS
