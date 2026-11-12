using System;
using System.Collections.Generic;
#if HAVE_MODULE_MATHEMATICS
using UnityMath = Newtonsoft.Json.Unity.Mathematics;
#endif

namespace Newtonsoft.Json.Unity {

  public static class UnityConverterRegistry {
    private sealed class Registration {
      public Type Type { get; }
      public Func<JsonConverter> Factory { get; }

      public Registration(Type type, Func<JsonConverter> factory) {
        Type = type;
        Factory = factory;
      }
    }

    private static readonly object Gate = new();
    private static readonly List<Registration> Registrations = CreateDefaultRegistrations();

    /// <summary>
    /// Types of every converter currently in the registry.
    /// </summary>
    public static IReadOnlyList<Type> ConverterTypes {
      get {
        lock (Gate) {
          var types = new Type[Registrations.Count];
          for (var i = 0; i < types.Length; i++) types[i] = Registrations[i].Type;
          return types;
        }
      }
    }

    /// <summary>
    /// Creates a fresh instance of every available converter.
    /// </summary>
    public static JsonConverter[] CreateConverters() {
      lock (Gate) {
        var converters = new JsonConverter[Registrations.Count];
        for (var i = 0; i < converters.Length; i++)
          converters[i] = Registrations[i].Factory();
        return converters;
      }
    }

    /// <summary>
    /// Creates settings pre-filled with every available converter.
    /// </summary>
    public static JsonSerializerSettings CreateSettings() {
      return ApplyTo(new JsonSerializerSettings());
    }

    /// <summary>
    /// Adds every available converter to existing settings.
    /// </summary>
    public static JsonSerializerSettings ApplyTo(JsonSerializerSettings settings) {
      foreach (var converter in CreateConverters())
        settings.Converters.Add(converter);

      return settings;
    }

    /// <summary>
    /// Registers a custom converter type, replacing any registration of the same type.
    /// </summary>
    public static void Register<T>() where T : JsonConverter, new() {
      Set(typeof(T), () => new T());
    }

    /// <summary>
    /// Registers a converter instance, replacing any registration of the same type.
    /// </summary>
    public static void Register(JsonConverter converter) {
      Set(converter.GetType(), () => converter);
    }

    /// <summary>
    /// Removes a converter from the registry. Returns false when it was not registered.
    /// </summary>
    public static bool Unregister<T>() where T : JsonConverter {
      return Unregister(typeof(T));
    }

    /// <summary>
    /// Removes a converter from the registry. Returns false when it was not registered.
    /// </summary>
    public static bool Unregister(Type converterType) {
      if (converterType == null) throw new ArgumentNullException(nameof(converterType));

      lock (Gate) {
        var index = Registrations.FindIndex(o => o.Type == converterType);
        if (index < 0) return false;

        Registrations.RemoveAt(index);
        return true;
      }
    }

    /// <summary>
    /// Restores the built-in converters and drops all custom registrations.
    /// </summary>
    public static void Reset() {
      lock (Gate) {
        Registrations.Clear();
        Registrations.AddRange(CreateDefaultRegistrations());
      }
    }

    private static void Set(Type type, Func<JsonConverter> factory) {
      lock (Gate) {
        var registration = new Registration(type, factory);
        var index = Registrations.FindIndex(o => o.Type == type);
        if (index < 0) Registrations.Add(registration);
        else Registrations[index] = registration;
      }
    }

    private static List<Registration> CreateDefaultRegistrations() {
      var list = new List<Registration>();

      // Unity core modules, always available.
      Add<BoundsConverter>(list);
      Add<BoundsIntConverter>(list);
      Add<PlaneConverter>(list);
      Add<RectConverter>(list);
      Add<RectIntConverter>(list);
      Add<RectOffsetConverter>(list);
      Add<Color32Converter>(list);
      Add<ColorConverter>(list);
      Add<Matrix4x4Converter>(list);
      Add<QuaternionConverter>(list);
      Add<SphericalHarmonicsL2Converter>(list);
      Add<Vector2Converter>(list);
      Add<Vector2IntConverter>(list);
      Add<Vector3Converter>(list);
      Add<Vector3IntConverter>(list);
      Add<Vector4Converter>(list);
      Add<CullingGroupEventConverter>(list);
      Add<Hash128Converter>(list);
      Add<ResolutionConverter>(list);
      Add<LayerMaskConverter>(list);
      Add<RangeIntConverter>(list);
      Add<RandomStateConverter>(list);
      Add<NativeArrayConverter>(list);

#if HAVE_MODULE_AI
      Add<NavMeshQueryFilterConverter>(list);
      Add<NavMeshTriangulationConverter>(list);
#endif
#if HAVE_MODULE_PHYSICS
      Add<JointDriveConverter>(list);
      Add<JointLimitsConverter>(list);
      Add<SoftJointLimitConverter>(list);
#endif
#if HAVE_MODULE_PHYSICS2D
      Add<ColliderDistance2DConverter>(list);
      Add<ContactFilter2DConverter>(list);
#endif
#if HAVE_MODULE_MATHEMATICS
      Add<UnityMath.Bool2Converter>(list);
      Add<UnityMath.Bool3Converter>(list);
      Add<UnityMath.Bool4Converter>(list);
      Add<UnityMath.Double2Converter>(list);
      Add<UnityMath.Double3Converter>(list);
      Add<UnityMath.Double4Converter>(list);
      Add<UnityMath.Float2Converter>(list);
      Add<UnityMath.Float3Converter>(list);
      Add<UnityMath.Float4Converter>(list);
      Add<UnityMath.HalfConverter>(list);
      Add<UnityMath.Half2Converter>(list);
      Add<UnityMath.Half3Converter>(list);
      Add<UnityMath.Half4Converter>(list);
      Add<UnityMath.Int2Converter>(list);
      Add<UnityMath.Int3Converter>(list);
      Add<UnityMath.Int4Converter>(list);
      Add<UnityMath.QuaternionConverter>(list);
      Add<UnityMath.Uint2Converter>(list);
      Add<UnityMath.Uint3Converter>(list);
      Add<UnityMath.Uint4Converter>(list);
#endif
      return list;
    }

    private static void Add<T>(List<Registration> list) where T : JsonConverter, new() {
      list.Add(new Registration(typeof(T), () => new T()));
    }
  }

}
