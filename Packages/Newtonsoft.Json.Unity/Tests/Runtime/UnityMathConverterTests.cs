using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;

namespace Newtonsoft.Json.Unity.Tests {

  public class UnityMathConverterTests : ConverterTestBase {

    [Test]
    public void Vector2Converter() => AssertConverter(new Vector2(1.5f, -2.25f), "x", "y");

    [Test]
    public void Vector3Converter() => AssertConverter(new Vector3(1.5f, -2.25f, 3.75f), "x", "y", "z");

    [Test]
    public void Vector4Converter() => AssertConverter(new Vector4(1.5f, -2.25f, 3.75f, -4.5f), "x", "y", "z", "w");

    [Test]
    public void Vector2IntConverter() => AssertConverter(new Vector2Int(-3, 7), "x", "y");

    [Test]
    public void Vector3IntConverter() => AssertConverter(new Vector3Int(-3, 7, 11), "x", "y", "z");

    [Test]
    public void QuaternionConverter() => AssertConverter(new Quaternion(0.1f, 0.2f, 0.3f, 0.9f), "x", "y", "z", "w");

    [Test]
    public void ColorConverter() => AssertConverter(new Color(0.25f, 0.5f, 0.75f, 1f), "r", "g", "b", "a");

    [Test]
    public void Color32Converter() => AssertConverter(new Color32(12, 34, 56, 78), "r", "g", "b", "a");

    [Test]
    public void Matrix4x4Converter() {
      var value = Matrix4x4.identity;
      value[0] = 1.5f;
      value[5] = -2.25f;
      value[15] = 3.75f;

      AssertConverter(value, (expected, actual) => {
        for (var i = 0; i < 16; i++) Assert.AreEqual(expected[i], actual[i], 0.00001f, $"matrix index {i}");
      }, MatrixMemberNames());
    }

    [Test]
    public void SphericalHarmonicsL2Converter() {
      var value = new SphericalHarmonicsL2();
      value[0, 0] = 1.25f;
      value[1, 3] = -2.5f;
      value[2, 8] = 3.75f;

      AssertConverter(value, (expected, actual) => {
        for (var rgb = 0; rgb < 3; rgb++)
          for (var i = 0; i < 9; i++)
            Assert.AreEqual(expected[rgb, i], actual[rgb, i], 0.00001f, $"SH2 [{rgb}, {i}]");
      }, SphericalHarmonicsMemberNames());
    }

    private static string[] MatrixMemberNames() {
      var names = new List<string>();
      for (var row = 0; row < 4; row++)
        for (var column = 0; column < 4; column++)
          names.Add($"m{column}{row}");
      return names.ToArray();
    }

    private static string[] SphericalHarmonicsMemberNames() {
      var names = new List<string>();
      foreach (var channel in new[] { 'r', 'g', 'b' })
        for (var i = 0; i < 9; i++) names.Add(channel + i.ToString());
      return names.ToArray();
    }
  }

}
