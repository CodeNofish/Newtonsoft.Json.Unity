#if HAVE_MODULE_MATHEMATICS
using Unity.Mathematics;

namespace Newtonsoft.Json.Unity.Mathematics {

  public class Float2Converter : PartialConverter<float2> {
    protected override void ReadValue(ref float2 value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case nameof(value.x):
        value.x = reader.ReadAsFloat() ?? 0f;
        break;
      case nameof(value.y):
        value.y = reader.ReadAsFloat() ?? 0f;
        break;
      }
    }

    protected override void WriteJsonProperties(JsonWriter writer, float2 value, JsonSerializer serializer) {
      writer.WritePropertyName(nameof(value.x));
      writer.WriteValue(value.x);
      writer.WritePropertyName(nameof(value.y));
      writer.WriteValue(value.y);
    }
  }
  
  public class Float3Converter : PartialConverter<float3> {
    protected override void ReadValue(ref float3 value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case nameof(value.x):
        value.x = reader.ReadAsFloat() ?? 0f;
        break;
      case nameof(value.y):
        value.y = reader.ReadAsFloat() ?? 0f;
        break;
      case nameof(value.z):
        value.z = reader.ReadAsFloat() ?? 0f;
        break;
      }
    }

    protected override void WriteJsonProperties(JsonWriter writer, float3 value, JsonSerializer serializer) {
      writer.WritePropertyName(nameof(value.x));
      writer.WriteValue(value.x);
      writer.WritePropertyName(nameof(value.y));
      writer.WriteValue(value.y);
      writer.WritePropertyName(nameof(value.z));
      writer.WriteValue(value.z);
    }
  }
  
  public class Float4Converter : PartialConverter<float4> {
    protected override void ReadValue(ref float4 value, string name, JsonReader reader, JsonSerializer serializer) {
      switch (name) {
      case nameof(value.x):
        value.x = reader.ReadAsFloat() ?? 0f;
        break;
      case nameof(value.y):
        value.y = reader.ReadAsFloat() ?? 0f;
        break;
      case nameof(value.z):
        value.z = reader.ReadAsFloat() ?? 0f;
        break;
      case nameof(value.w):
        value.w = reader.ReadAsFloat() ?? 0f;
        break;
      }
    }

    protected override void WriteJsonProperties(JsonWriter writer, float4 value, JsonSerializer serializer) {
      writer.WritePropertyName(nameof(value.x));
      writer.WriteValue(value.x);
      writer.WritePropertyName(nameof(value.y));
      writer.WriteValue(value.y);
      writer.WritePropertyName(nameof(value.z));
      writer.WriteValue(value.z);
      writer.WritePropertyName(nameof(value.w));
      writer.WriteValue(value.w);
    }
  }

}
#endif // HAVE_MODULE_MATHEMATICS
